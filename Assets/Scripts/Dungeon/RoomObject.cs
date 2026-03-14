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
                if (room.connections.ContainsKey(room.gridPos + dirs[i]))
                {
                    doors[i].gameObject.SetActive(true);
                    doors[i].color = doorColors[room.connections[room.gridPos + dirs[i]]];
                }
                else
                {
                    doors[i].gameObject.SetActive(false);
                }
            }

            switch (room.type)
            {
                case RoomType.Normal:
                    break;
                case RoomType.Start:
                    roomType.color = roomTypeColors[1];
                    break;
                case RoomType.MiniBoss:
                    roomType.color = roomTypeColors[2];
                    break;
                case RoomType.Boss:
                    roomType.color = roomTypeColors[3];
                    break;
            }
        }
    }
}
