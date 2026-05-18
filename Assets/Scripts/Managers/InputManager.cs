using UnityEngine;
using UnityEngine.InputSystem;

namespace VentureBound.Managers
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public Vector2 Move { get { return move; } }
        public Vector2 Look { get { return look; } }
        public bool Jump {  get { return jump; } }
        public bool Attack {  get { return attack; } }
        public bool Interact { get { return interact; } }
        public bool Defend {  get { return defend; } }
        public bool Sprint {  get { return sprint; } }
        public bool LockOn {  get { return lockOn; } }
        public bool Pause {  get { return pause; } }

        public static InputManager Instance;

        private PlayerInput input;

        private Vector2 move;
        private Vector2 look;
        private bool jump;
        private bool attack;
        private bool interact;
        private bool defend;
        private bool sprint;
        private bool lockOn;
        private bool pause;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnEnable()
        {
            if (input == null)
            {
                input = GetComponent<PlayerInput>();
            }

            input.onActionTriggered += OnAction;
        }

        private void OnDisable()
        {
            input.onActionTriggered -= OnAction;
        }

        public void OnAction(InputAction.CallbackContext context)
        {
            switch (context.action.name)
            {
                case "Move":
                    move = context.ReadValue<Vector2>();
                    break;
                case "Look":
                    look = context.ReadValue<Vector2>();
                    break;
                case "Jump":
                    SetBoolValue(ref jump, context);
                    break;
                case "Attack":
                    SetBoolValue(ref attack, context);
                    break;
                case "Interact":
                    SetBoolValue(ref interact, context);
                    break;
                case "Defend":
                    SetBoolValue(ref defend, context);
                    break;
                case "Sprint":
                    SetBoolValue(ref sprint, context);
                    break;
                case "LockOn":
                    SetBoolValue(ref lockOn, context);
                    break;
                case "Pause":
                    SetBoolValue(ref pause, context);
                    break;
            }
        }

        private void SetBoolValue(ref bool value, InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                value = true;
            }

            if (context.canceled)
            {
                value = false;
            }
        }
    }
}
