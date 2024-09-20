using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class ShootingSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _entityCommandBufferSystem;

    protected override void OnCreate()
    {
        _entityCommandBufferSystem = World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var ecb = _entityCommandBufferSystem.CreateCommandBuffer().AsParallelWriter();
        var deltaTime = SystemAPI.Time.DeltaTime;
        var projectilePrefab = SystemAPI.GetSingleton<ProjectileSpawnerData>().ProjectilePrefab;

        // Handle shooting
        Entities.ForEach((int entityInQueryIndex, in SpaceshipInput input, in LocalTransform transform) =>
        {
            if (input.ShootInput)
            {
                Debug.Log("Spawning projectile");
                var spawnPosition = transform.Position + new float3(0, 1, 0); // Spawn slightly above the player
                var instance = ecb.Instantiate(entityInQueryIndex, projectilePrefab);
                ecb.SetComponent(entityInQueryIndex, instance, LocalTransform.FromPosition(spawnPosition));
                ecb.SetComponent(entityInQueryIndex, instance, new ProjectileData
                {
                    Velocity = new float2(0, 1), // Upward velocity
                    Speed = 5f
                });
            }
        }).ScheduleParallel();

        // Move projectiles
        Entities.ForEach((ref ProjectileData projectile, ref LocalTransform transform) =>
        {
            var movement = new float3(projectile.Velocity.x, projectile.Velocity.y, 0) * projectile.Speed * deltaTime;
            transform.Position += movement;
        }).ScheduleParallel();

        _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}

public struct ProjectileSpawnerData : IComponentData
{
    public Entity ProjectilePrefab;
}