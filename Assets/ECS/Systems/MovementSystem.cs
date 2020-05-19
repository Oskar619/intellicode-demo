using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace Scarred.Systems
{
    public class MovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var delta = Time.DeltaTime;
            Entities.ForEach((ref Translation pos, ref MoveData move) =>
            {
                float3 normalizedDir = math.normalizesafe(move.direction);
                pos.Value += normalizedDir * move.speed * delta;
            });
        }
    }
}
