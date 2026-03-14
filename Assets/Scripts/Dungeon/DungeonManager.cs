using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace VentureBound.Dungeon
{
    public class DungeonManager : NetworkBehaviour
    {
        public static DungeonManager instance;
        public NetworkVariable<int> dungeonSeed = new NetworkVariable<int>();

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                if (instance != this)
                {
                    Destroy(gameObject);
                }
            }

            DontDestroyOnLoad(gameObject);
        }

        public void SetDungeonSeed()
        {
            if (IsServer)
            {
                int seed = (int)(System.DateTime.Now.Ticks % int.MaxValue);
                dungeonSeed.Value = seed;
            }
        }
    }
}
