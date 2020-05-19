using UnityEngine;
using PlayerInput = Scarred.Inputs.PlayerInput;
using Unity.Entities;
using UnityEngine.InputSystem;

namespace Scarred.Systems
{
    [AlwaysUpdateSystem]
    public class PlayerInputSystem : ComponentSystem
    {
        PlayerInput input;

        protected override void OnCreate()
        {
            base.OnCreate();
            input = new PlayerInput();
            input.Enable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            input.Disable();
        }

        protected override void OnUpdate()
        {
            InputSystem.Update();

            var frameInput = new InputData();
            frameInput.look = input.Player.LOOK.ReadValue<Vector2>();
            frameInput.movement = input.Player.MOVE.ReadValue<Vector2>();
            frameInput.crouch = input.Player.CROUCH.phase == InputActionPhase.Started;
            frameInput.interact = input.Player.INTERACT.phase == InputActionPhase.Started;
            frameInput.run = input.Player.RUN.phase == InputActionPhase.Started;
            frameInput.jump = input.Player.JUMP.phase == InputActionPhase.Started;
            Debug.Log(frameInput.jump);

            Entities.ForEach((ref InputData inputData) =>
            {
                inputData.crouch = frameInput.crouch;
                inputData.look = frameInput.look;
                inputData.movement = frameInput.movement;
                inputData.run = frameInput.run;
                inputData.interact = frameInput.interact;
                inputData.jump = frameInput.jump;
            });
        }
    }
}