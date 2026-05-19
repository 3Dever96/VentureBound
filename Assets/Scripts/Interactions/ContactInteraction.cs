using UnityEngine;

namespace Interactions
{
    public class ContactInteraction : BaseInteraction
    {
        [SerializeField] protected ContactType contactType;

        private void OnTriggerEnter(Collider other)
        {
            if (contactType == ContactType.Enter)
            {
                OnInteract();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (contactType == ContactType.Stay)
            {
                OnInteract();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (contactType == ContactType.Exit)
            {
                OnInteract();
            }
        }
    }

    public enum ContactType
    {
        Enter,
        Stay,
        Exit
    }
}
