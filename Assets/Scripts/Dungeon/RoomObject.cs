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

            transform.position = new Vector3(roomSize.x * room.gridPos.x, 0f, roomSize.y * room.gridPos.y);

            for (var i = 0; i < doors.Length; i++)
            {
                doors[i].gameObject.SetActive(room.hasDoor[i]);
                doors[i].color = doorColors[room.doorType[i]];
            }
        }
    }
}
