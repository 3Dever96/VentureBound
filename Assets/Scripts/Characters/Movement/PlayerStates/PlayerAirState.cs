using UnityEngine;
using VentureBound.Managers;

namespace VentureBound.CharacterMovement
{
    public class PlayerAirState : MoveState
    {
        [Header("Vertical Movement")]
        [SerializeField] private float fallSpeed;
        private float gravity;

        [Header("Double Jump")]
        [SerializeField] private bool useDoubleJump;
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float moveSpeed;

        private bool canJump;
        private bool jumpCheck;

        public override void StartState(MovementController move)
        {
            move.Controller.stepOffset = 0f;
            move.Controller.slopeLimit = 0f;

            canJump = false;
            jumpCheck = false;
        }

        public override void UpdateState(MovementController move)
        {
            if (useDoubleJump)
            {
                if (InputManager.Instance.Jump && canJump && !jumpCheck)
                {
                    Vector3 direction = Camera.main.transform.right * InputManager.Instance.Move.x + Camera.main.transform.forward * InputManager.Instance.Move.y;
                    direction.y = 0f;
                    direction = direction.normalized;

                    if (direction != Vector3.zero)
                    {
                        move.Direction = direction;
                    }

                    move.CurrentSpeed = Mathf.Max(moveSpeed, move.CurrentSpeed);

                    move.VerticalSpeed = jumpSpeed;
                    canJump = false;
                    jumpCheck = true;
                }

                if (!InputManager.Instance.Jump && !canJump)
                {
                    canJump = true;
                }
            }

            if (move.VerticalSpeed < 0f || !InputManager.Instance.Jump || Physics.CheckSphere(transform.position + Vector3.up * move.Controller.height, move.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
            {
                gravity = Physics.gravity.y * 3f;
            }
            else
            {
                gravity = Physics.gravity.y;
            }

            if (move.VerticalSpeed > fallSpeed)
            {
                move.VerticalSpeed += gravity * Time.deltaTime;
            }

            move.FaceDirection(move.Direction);

            move.ApplyMovement(move.Direction);
        }

        public override void ChangeState(MovementController move)
        {
            if (move.States.ContainsKey("VentureBound.CharacterMovement.PlayerGroundState"))
            {
                if (move.VerticalSpeed <= 0f && Physics.CheckSphere(transform.position + Vector3.up * 0.375f, move.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
                {
                    move.SetState(move.States["VentureBound.CharacterMovement.PlayerGroundState"]);
                }
            }
        }

        public override void ExitState(MovementController move)
        {
            move.Controller.stepOffset = 0.3f;
            move.Controller.slopeLimit = 45f;
        }
    }
}
