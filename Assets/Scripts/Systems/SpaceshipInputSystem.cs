using Unity.Entities;
using UnityEngine;

public partial class SpaceshipInputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        bool shootInput = Input.GetMouseButtonDown(0);

        Entities.ForEach((ref SpaceshipInput input) =>
        {
            input.HorizontalInput = horizontalInput;
            input.ShootInput = shootInput;
        }).Schedule();
    }
}