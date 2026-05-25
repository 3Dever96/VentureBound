using UnityEngine;
using VentureBound.Managers;
using VentureBound.CharacterData;

namespace VentureBound.CharacterMovement
{
    public class PlayerGroundState : MoveState
    {
        [Header("Momentum based movement")]
        [SerializeField] private float runSpeed;
        [SerializeField] private float sprintSpeed;
        [SerializeField] private float accel;
        [SerializeField] private float decel;
        [SerializeField] private float fric;
        [SerializeField] private float maxTurnAngle;

        private float moveSpeed;

        private bool canSprint;
        private bool hasSprintPower;

        [Header("Vertical Movement")]
        [SerializeField] private float jumpSpeed;
        [SerializeField] private float stickForce;
        [SerializeField] private bool useJump;

        private bool canJump;

        public override void StartState(MovementController move)
        {
            move.VerticalSpeed = stickForce;

            canJump = false;
        }

        public override void UpdateState(MovementController move)
        {
            Vector3 direction = Camera.main.transform.right * InputManager.Instance.Move.x + Camera.main.transform.forward * InputManager.Instance.Move.y;
            direction.y = 0f;
            direction = direction.normalized;

            hasSprintPower = move.Stats.currentSP > 0;

            if (!canSprint)
            {
                canSprint = move.Stats.currentSP == move.Stats.maxSP;
            }

            if (move.Stats.skills.ContainsKey("Sprint"))
            {
                moveSpeed = InputManager.Instance.Sprint && hasSprintPower && canSprint ? sprintSpeed : runSpeed;
            }
            else
            {
                moveSpeed = runSpeed;
            }

            if (InputManager.Instance.Move != Vector2.zero)
            {
                if (Vector3.Angle(direction, move.Direction) > maxTurnAngle)
                {
                    move.CurrentSpeed -= decel * Time.deltaTime;

                    if (move.CurrentSpeed <= 0f)
                    {
                        move.CurrentSpeed = 0f;
                        move.Direction = direction;
                    }
                }
                else
                {
                    float maxSpeed = moveSpeed == sprintSpeed ? sprintSpeed : moveSpeed * InputManager.Instance.Move.magnitude;

                    if (move.CurrentSpeed < maxSpeed)
                    {
                        if (maxSpeed != sprintSpeed)
                        {
                            move.CurrentSpeed += accel * Time.deltaTime;
                        }
                        else
                        {
                            move.CurrentSpeed = sprintSpeed;
                            move.Stats.currentSP -= 3f * Time.deltaTime;

                            if (move.Stats.currentSP <= 0f)
                            {
                                canSprint = false;
                            }
                        }
                    }
                    else if (move.CurrentSpeed > maxSpeed + 0.1f)
                    {
                        move.CurrentSpeed -= accel * Time.deltaTime;
                    }
                    else
                    {
                        move.CurrentSpeed = maxSpeed;
                    }

                    move.Direction = direction;
                }
            }
            else
            {
                move.CurrentSpeed -= Mathf.Min(move.CurrentSpeed, fric * Time.deltaTime);
            }

            move.FaceDirection(move.Direction);

            if (move.Stats.skills.ContainsKey("Jump"))
            {
                if (useJump)
                {
                    if (InputManager.Instance.Jump && canJump)
                    {
                        move.VerticalSpeed = jumpSpeed;
                    }

                    if (!InputManager.Instance.Jump && !canJump)
                    {
                        canJump = true;
                    }
                }
            }

            move.ApplyMovement(move.Direction);
        }

        public override void ChangeState(MovementController move)
        {
            if (move.States.ContainsKey("VentureBound.CharacterMovement.PlayerAirState"))
            {
                if (move.VerticalSpeed > 0f || !Physics.CheckSphere(transform.position, move.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
                {
                    move.SetState(move.States["VentureBound.CharacterMovement.PlayerAirState"]);
                }
            }
        }

        public override void ExitState(MovementController move)
        {
            
        }
    }
}
