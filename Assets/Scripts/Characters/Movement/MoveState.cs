using System;
using UnityEngine;

namespace VentureBound.CharacterMovement
{
    public abstract class MoveState : MonoBehaviour
    {
        public string StateName { get { return GetType().ToString(); } }
        public abstract void StartState(MovementController move);
        public abstract void UpdateState(MovementController move);
        public abstract void ChangeState(MovementController move);
        public abstract void ExitState(MovementController move);
    }
}
