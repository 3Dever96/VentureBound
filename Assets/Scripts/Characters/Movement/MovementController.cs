using System.Collections.Generic;
using UnityEngine;

namespace VentureBound.CharacterMovement
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        public CharacterController Controller {  get; private set; }

        public MoveState CurrentState { get; private set; }
        public Dictionary<string, MoveState> States = new Dictionary<string, MoveState>();

        public float CurrentSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        public Vector3 Direction { get; set; }

        private void Start()
        {
            Direction = transform.forward;

            Controller = GetComponent<CharacterController>();

            InitializeStateMachine();
        }

        private void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.UpdateState(this);
                CurrentState.ChangeState(this);
            }
        }

        public void FaceDirection(Vector3 direction, float turnSpeed = 500f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
        }

        public void ApplyMovement(Vector3 direction)
        {
            Vector3 velocity = direction * CurrentSpeed;
            velocity.y = VerticalSpeed;

            Controller.Move(velocity * Time.deltaTime);
        }

        private void InitializeStateMachine()
        {
            MoveState[] states = GetComponents<MoveState>();

            for (var i = 0; i < states.Length; i++)
            {
                States.Add(states[i].StateName, states[i]);
            }

            SetState(states[0]);
        }

        public void SetState(MoveState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState(this);
            }

            CurrentState = newState;

            if (CurrentState != null)
            {
                CurrentState.StartState(this);
            }
        }
    }
}
