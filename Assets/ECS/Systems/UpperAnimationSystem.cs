using Unity.Entities;

namespace Scarred.Systems
{
    public class UpperAnimationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref AnimationTriggerData animationData, ref UpperAnimationTag animationType, ref InputData inputData) =>
            {
                var isJumping = inputData.jump && !inputData.crouch;
                if (isJumping)
                {
                    animationData.name = AnimationTriggerNames.UpperJump;
                }
                else if (inputData.movement.magnitude > 0)
                {
                    if (inputData.crouch)
                    {
                        animationData.name = AnimationTriggerNames.UpperCrouchWalk;
                    }
                    else if (inputData.run)
                    {
                        animationData.name = AnimationTriggerNames.UpperRun;
                    }
                    else
                    {
                        animationData.name = AnimationTriggerNames.UpperWalk;
                    }
                }
                else
                {
                    if (inputData.crouch)
                    {
                        animationData.name = AnimationTriggerNames.UpperCrouch;
                    }
                    else
                    {
                        animationData.name = AnimationTriggerNames.UpperIdle;
                    }
                }
            });
        }
    }
}

