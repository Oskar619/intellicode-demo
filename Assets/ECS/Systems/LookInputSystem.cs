using Unity.Entities;
using Scarred.Systems;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

public class LookInputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref InputData input, ref FaceTowardsData faceTowards, ref Translation translation) =>
        {
            float3 look = new float3(input.look.x, input.look.y, 0);
            var screenPoint = Camera.main.WorldToScreenPoint(translation.Value);
            var offset = new float3(look.x - screenPoint.x, look.y - screenPoint.y, 0);
            faceTowards.faceTowards = offset;
        });
    }
}
