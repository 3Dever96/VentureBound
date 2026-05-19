using UnityEngine;
using VentureBound.Managers;

namespace Interactions
{
    public class TreasureChest : InputInteraction
    {
        public override void OnInteract()
        {
            if (canInteract)
            {
                if (!didInteract)
                {
                    Inventory.Instance.AddGold(100);
                    didInteract = true;
                    print("You found 100 gold.");
                }
                else
                {
                    print("Someone's already taken this treasure...  Oh, wait.  That was me.");
                }
            }
            else
            {
                print("This chest is locked.");
            }
        }
    }
}
