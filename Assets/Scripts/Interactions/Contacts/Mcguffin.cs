using UnityEngine;
using VentureBound.Managers;

namespace Interactions
{
    public class Mcguffin : ContactInteraction
    {
        public override void OnInteract()
        {
            Inventory.Instance.AddMcguffin();
            Destroy(gameObject);
        }
    }
}
