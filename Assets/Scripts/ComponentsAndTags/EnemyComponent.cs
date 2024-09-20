using Unity.Entities;
using Unity.Mathematics;

public struct EnemyData : IComponentData
{
    public float2 Position;
    public float2 Velocity;
    public float Speed;
}