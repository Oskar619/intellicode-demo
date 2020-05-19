using Unity.Entities;
using Unity.Mathematics;

namespace Scarred.Systems
{
    public struct FaceTowardsData : IComponentData
    {
        public float3 faceTowards;
    }
}
