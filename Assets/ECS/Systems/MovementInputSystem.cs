using Unity.Entities;
using Scarred.Systems;
using Unity.Mathematics;

public class InputToMovementSystem : ComponentSystem
{
    private const float CrouchSpeed = 0.5f;
    private const float RunningSpeed = 2f;
    private const float WalkSpeed = 1f;

    protected override void OnUpdate()
    {
        Entities.ForEach((ref InputData inputData, ref MoveData moveData, ref FaceTowardsData faceTowards) =>
        {
            if(inputData.movement.magnitude > 0)
            {
                moveData.direction = new float3(inputData.movement.x, inputData.movement.y, moveData.direction.z);
                if (inputData.crouch)
                {
                    moveData.speed = CrouchSpeed;
                }

                if (inputData.run)
                {
                    moveData.speed = RunningSpeed;
                }

                moveData.speed = WalkSpeed;
            }
            else
            {
                moveData.speed = 0;
                moveData.direction = float3.zero;
            }
        });
    }
}
