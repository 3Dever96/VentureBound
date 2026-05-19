using UnityEngine;

namespace VentureBound.Managers
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        public int McGuffin;
        public int Gold;

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

            DontDestroyOnLoad(gameObject);
        }

        public void AddMcguffin()
        {
            McGuffin++;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }
    }
}
