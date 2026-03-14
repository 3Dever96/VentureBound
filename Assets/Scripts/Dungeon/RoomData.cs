using System.Collections.Generic;
using UnityEngine;

namespace VentureBound.Dungeon
{
    public enum RoomType
    {
        Normal,
        Start,
        MiniBoss,
        Boss
    }

    public class RoomData
    {
        public Vector2Int gridPos;
        public RoomType type = RoomType.Normal;
        public Dictionary<Vector2Int, int> connections = new Dictionary<Vector2Int, int>();

        public RoomData(Vector2Int pos)
        {
            gridPos = pos;
        }
    }
}
