using Unity.Entities;
using UnityEngine;
using TMPro;

public class ECSBootstrap : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        var world = World.DefaultGameObjectInjectionWorld;

        var simulationSystemGroup = world.GetExistingSystemManaged<SimulationSystemGroup>();
        var spaceshipControllerSystem = world.CreateSystemManaged<SpaceshipControllerSystem>();
        var spaceshipInputSystem = world.CreateSystemManaged<SpaceshipInputSystem>();
        var enemySpawnerSystem = world.CreateSystemManaged<EnemySpawnerSystem>();
        var shootingSystem = world.CreateSystemManaged<ShootingSystem>();
        var collisionSystem = world.CreateSystemManaged<CollisionSystem>();

        simulationSystemGroup.AddSystemToUpdateList(spaceshipControllerSystem);
        simulationSystemGroup.AddSystemToUpdateList(spaceshipInputSystem);
        simulationSystemGroup.AddSystemToUpdateList(enemySpawnerSystem);
        simulationSystemGroup.AddSystemToUpdateList(shootingSystem);
        simulationSystemGroup.AddSystemToUpdateList(collisionSystem);

        simulationSystemGroup.SortSystems();

        var entityManager = world.EntityManager;
    }
}