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
        public List<Vector2Int> connections = new List<Vector2Int>();

        public RoomData(Vector2Int pos)
        {
            gridPos = pos;
        }
    }
}
