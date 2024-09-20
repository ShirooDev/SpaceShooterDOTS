using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class EnemySpawnerSystem : SystemBase
{
    private float _timeSinceLastSpawn = 0f;
    private const float SpawnInterval = .25f;
    private Unity.Mathematics.Random _random;

    protected override void OnCreate()
    {
        _random = new Unity.Mathematics.Random((uint)System.DateTime.Now.Ticks);
    }

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;
        _timeSinceLastSpawn += deltaTime;

        if (_timeSinceLastSpawn >= SpawnInterval)
        {
            _timeSinceLastSpawn = 0f;

            // Get the enemy prefab
            var enemyPrefab = SystemAPI.GetSingleton<EnemySpawnerData>().EnemyPrefab;

            // Spawn position 
            float3 spawnPosition = new float3(_random.NextFloat(-5f, 5f), 5f, 0f);

            // Instantiate the enemy
            var enemy = EntityManager.Instantiate(enemyPrefab);

            // Set the enemy's position
            EntityManager.SetComponentData(enemy, LocalTransform.FromPosition(spawnPosition));

            // Set the enemy's data
            EntityManager.SetComponentData(enemy, new EnemyData
            {
                Position = spawnPosition.xy,
                Velocity = new float2(0, -2f), // Downward velocity
                Speed = 2f // This is now used to calculate the magnitude of the velocity
            });
        }

        // Move existing enemies
        Entities.ForEach((ref EnemyData enemyData, ref LocalTransform transform) =>
        {
            // Update position based on velocity
            enemyData.Position += enemyData.Velocity * deltaTime;

            // Update Unity's transform component
            transform.Position = new float3(enemyData.Position.x, enemyData.Position.y, transform.Position.z);

        }).ScheduleParallel();
    }
}