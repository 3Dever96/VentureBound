using UnityEngine;
using VentureBound.Managers;

namespace Interactions
{
    public class InputInteraction : BaseInteraction
    {
        public bool canInteract; // Is the condition for the interaction available
        public bool didInteract; // Has the interaction already been used

        private void OnTriggerEnter(Collider other)
        {
            if ((interactLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                InteractManager input = other.GetComponent<InteractManager>();

                if (input != null)
                {
                   if (input.myInteraction == null)
                    {
                        input.myInteraction = this;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if ((interactLayers.value & (1 << other.gameObject.layer)) != 0)
            {
                InteractManager input = other.GetComponent<InteractManager>();

                if (input != null)
                {
                   if (input.myInteraction == this)
                    {
                        input.myInteraction = null;
                    }
                }
            }
        }
    }
}
