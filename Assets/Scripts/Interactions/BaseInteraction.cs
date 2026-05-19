using UnityEngine;

namespace Interactions
{
    public abstract class BaseInteraction : MonoBehaviour
    {
        [SerializeField] protected LayerMask interactLayers;

        public virtual void OnInteract()
        {

        }
    }
}
