using UnityEngine;
using VentureBound.Managers;

namespace Interactions
{
    public class InteractManager : MonoBehaviour
    {
        public InputInteraction myInteraction;

        public bool canInteract;

        private void Update()
        {
            if (myInteraction != null)
            {
                if (InputManager.Instance.Interact && canInteract)
                {
                    myInteraction.OnInteract();
                    myInteraction = null;
                    canInteract = false;
                }

                if (!InputManager.Instance.Interact && !canInteract)
                {
                    canInteract = true;
                }
            }
        }
    }
}
