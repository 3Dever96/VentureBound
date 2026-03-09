using UnityEngine;

namespace VentureBound
{
    public class CameraTarget : MonoBehaviour
    {
        Transform player;

        void Update()
        {
            if (player != null)
            {
                transform.position = player.position;
            }
        }

        public void SetTarget(Transform newPlayer)
        {
            if (newPlayer == null)
            {
                return;
            }

            player = newPlayer;
        }
    }
}
