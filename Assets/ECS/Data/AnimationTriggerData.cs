using Unity.Entities;

namespace Scarred.Systems
{
    public struct AnimationTriggerData : IComponentData
    {
        public AnimationTriggerNames name;
    }

    public struct LowerAnimationTag : IComponentData
    {

    }

    public struct UpperAnimationTag : IComponentData
    {

    }
}

