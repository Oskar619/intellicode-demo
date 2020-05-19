using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Scarred.Systems
{
    public class FaceTowardsSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var delta = Time.DeltaTime;
            Entities.ForEach((ref Translation pos, ref Rotation rot, ref FaceTowardsData faceTowards) =>
            {
                var angle = Mathf.Atan2(faceTowards.faceTowards.y, faceTowards.faceTowards.x) * Mathf.Rad2Deg;
                rot.Value = Quaternion.Euler(0, 0, angle - 180);
            });
        } 
    }
}
