using Unity.Entities;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour
{
    public float initialSpeed = 2f;

    class Baker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyData
            {
                Speed = authoring.initialSpeed,
                Position = new Vector2(authoring.transform.position.x, authoring.transform.position.y)
            });
        }
    }
}