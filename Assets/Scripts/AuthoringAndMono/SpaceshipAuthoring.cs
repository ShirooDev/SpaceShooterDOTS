using Unity.Entities;
using UnityEngine;

public class SpaceshipAuthoring : MonoBehaviour
{
    public float moveSpeed = 5f;

    class Baker : Baker<SpaceshipAuthoring>
    {
        public override void Bake(SpaceshipAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpaceshipData { 
                MoveSpeed = authoring.moveSpeed 
            });
            AddComponent<SpaceshipInput>(entity);
        }
    }
}
public struct SpaceshipData : IComponentData
{
    public float MoveSpeed;
}