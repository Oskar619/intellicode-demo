using Scarred.Systems;
using Unity.Entities;

public class LowerAnimationSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref AnimationTriggerData animationData, ref LowerAnimationTag animationType, ref InputData inputData) =>
        {
            var isJumping = inputData.jump && !inputData.crouch;
            if (isJumping)
            {
                animationData.name = AnimationTriggerNames.LowerJump;
            }
            else if (inputData.movement.magnitude > 0)
            {
                if (inputData.crouch)
                {
                    animationData.name = AnimationTriggerNames.LowerCrouchWalk;
                }
                else if (inputData.run)
                {
                    animationData.name = AnimationTriggerNames.LowerRun;
                }
                else
                {
                    animationData.name = AnimationTriggerNames.LowerWalk;
                }
            }
            else
            {
                if (inputData.crouch)
                {
                    animationData.name = AnimationTriggerNames.LowerCrouch;
                }
                else
                {
                    animationData.name = AnimationTriggerNames.LowerIdle;
                }
            }
        });
    }
}
