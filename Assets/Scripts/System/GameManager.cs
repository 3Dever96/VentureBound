using Unity.Netcode;
using Unity.Netcode.Transports;
using Unity.Netcode.Transports.SinglePlayer;
using UnityEngine;

namespace VentureBound.System
{
    public class GameManager : NetworkBehaviour
    {
        public static GameManager instance;

        public void Start()
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

            StartSoloSession();
        }

        public void StartSoloSession()
        {
            NetworkManager netObject = NetworkManager.Singleton.GetComponent<NetworkManager>();
            SinglePlayerTransport solo = netObject.GetComponent<SinglePlayerTransport>();

            netObject.NetworkConfig.NetworkTransport = solo;

            netObject.StartHost();
        }
    }
}
