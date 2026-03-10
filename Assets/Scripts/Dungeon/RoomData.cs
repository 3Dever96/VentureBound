using UnityEngine;

namespace VentureBound.Dungeon
{
    public class RoomData
    {
        public Vector2Int gridPos;
        public bool[] hasDoor = new bool[4];
        public int[] doorType = new int[4];
        public int[] roomType = new int[4];

        public RoomData(Vector2Int pos)
        {
            gridPos = pos;
        }
    }
}
