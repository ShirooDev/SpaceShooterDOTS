using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public partial class CollisionSystem : SystemBase
{
    private EndSimulationEntityCommandBufferSystem _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _endSimulationEcbSystem.CreateCommandBuffer().AsParallelWriter();

        var enemyPositions = new NativeList<float3>(Allocator.TempJob);
        var enemyEntities = new NativeList<Entity>(Allocator.TempJob);

        Entities
            .WithAll<EnemyData>()
            .ForEach((Entity enemyEntity, in LocalTransform enemyTransform) => {
                enemyPositions.Add(enemyTransform.Position);
                enemyEntities.Add(enemyEntity);
            }).Run();

        // Collision threshold as a local variable
        float collisionThreshold = .5f;

        // Check collisions
        Entities
            .WithAll<ProjectileData>()
            .ForEach((Entity projectileEntity, int entityInQueryIndex, in LocalTransform projectileTransform) => {
                for (int i = 0; i < enemyPositions.Length; i++)
                {
                    if (CheckCollision(projectileTransform.Position, enemyPositions[i], collisionThreshold))
                    {
                        ecb.DestroyEntity(entityInQueryIndex, enemyEntities[i]);
                        ecb.DestroyEntity(entityInQueryIndex, projectileEntity);
                        break; // Projectile can only hit one enemy
                    }
                }
            }).Schedule();

        _endSimulationEcbSystem.AddJobHandleForProducer(Dependency);

        
        Dependency = enemyPositions.Dispose(Dependency);
        Dependency = enemyEntities.Dispose(Dependency);
    }

    private static bool CheckCollision(float3 projectilePosition, float3 enemyPosition, float collisionThreshold)
    {
        return math.distance(projectilePosition, enemyPosition) < collisionThreshold;
    }
}