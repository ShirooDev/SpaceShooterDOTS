using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public struct Position : IComponentData
{
    public float2 Value;
}

public partial class SpaceshipControllerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        Entities.ForEach((ref LocalTransform transform, in SpaceshipData data, in SpaceshipInput input) =>
        {
            float3 movement = new float3(input.HorizontalInput * data.MoveSpeed * deltaTime, 0, 0);
            transform.Position += movement;

        }).Schedule();
    }
}
