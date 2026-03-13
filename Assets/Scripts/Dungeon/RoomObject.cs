using UnityEngine;

namespace VentureBound.Dungeon
{
    public class RoomObject : MonoBehaviour
    {
        [SerializeField] Vector2 roomSize;

        [SerializeField] SpriteRenderer[] doors;
        [SerializeField] SpriteRenderer roomType;

        [SerializeField] Color[] doorColors;
        [SerializeField] Color[] roomTypeColors;

        public void InitializeRoom(RoomData room)
        {
            if (room == null)
            {
                return;
            }

            transform.position = new Vector3(room.gridPos.x * roomSize.x, 0f, room.gridPos.y * roomSize.y);

            Vector2Int[] dirs = { Vector2Int.up, Vector2Int.right, Vector2Int.left, Vector2Int.down };

            for (var i = 0; i < dirs.Length; i++)
            {
                if (room.connections.Contains(room.gridPos + dirs[i]))
                {
                    doors[i].gameObject.SetActive(true);
                }
                else
                {
                    doors[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
