using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace VentureBound.Player
{
    [RequireComponent(typeof(NetworkObject)), RequireComponent(typeof(NetworkTransform)), RequireComponent(typeof(PlayerInput)), RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour
    {
        // Component References
        PlayerInput input;
        CharacterController controller;

        // Movement Variables
        [SerializeField] float moveSpeed;
        [SerializeField] float turnSpeed;
        [SerializeField] float gravity;

        float currentSpeed;
        float verticalSpeed;

        Vector3 lookDirection;
        Vector3 velocity;

        // Input
        Vector2 move;

        void FixedUpdate()
        {
            if (IsOwner)
            {
                // Get input direction
                Vector3 direction = Vector3.right * move.x + Vector3.forward * move.y;
                direction.y = 0f;
                direction = direction.normalized;

                // Set Ground Speed
                if (move != Vector2.zero)
                {
                    currentSpeed = moveSpeed;
                    lookDirection = direction;
                }
                else
                {
                    currentSpeed = 0f;
                }

                // Rotate to face direction
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), turnSpeed * Time.deltaTime);

                // Apply Gravity
                if (!controller.isGrounded)
                {
                    verticalSpeed += gravity * Time.deltaTime;
                }
                else
                {
                    verticalSpeed = 0f;
                }

                // Set Velocity and move the character
                velocity = currentSpeed * lookDirection;
                velocity.y = verticalSpeed;

                controller.Move(velocity * Time.deltaTime);
            }
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                input = GetComponent<PlayerInput>();
                controller = GetComponent<CharacterController>();

                input.onActionTriggered += OnAction;
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsOwner)
            {
                input.onActionTriggered -= OnAction;
            }
        }

        void OnAction(InputAction.CallbackContext context)
        {
            if (IsOwner)
            {
                switch (context.action.name)
                {
                    case "Move":
                        move = context.ReadValue<Vector2>();
                        break;
                }
            }
        }
    }
}
