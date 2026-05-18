using UnityEngine;
using VentureBound.Managers;

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

        public override void StartState(MovementController move)
        {
            
        }

        public override void UpdateState(MovementController move)
        {
            Vector3 direction = Camera.main.transform.right * InputManager.Instance.Move.x + Camera.main.transform.forward * InputManager.Instance.Move.y;
            direction.y = 0f;
            direction = direction.normalized;

            moveSpeed = InputManager.Instance.Sprint ? sprintSpeed : runSpeed;

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
                        move.CurrentSpeed += accel * Time.deltaTime;
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

            move.ApplyMovement(move.Direction);
        }

        public override void ChangeState(MovementController move)
        {
            
        }

        public override void ExitState(MovementController move)
        {
            
        }
    }
}
