using System.Collections.Generic;
using UnityEngine;

namespace VentureBound.Dungeon
{
    public class RoomObject : MonoBehaviour
    {
        public Dictionary<Vector2Int, FloorTile> floorTiles = new Dictionary<Vector2Int, FloorTile>();
        public List<FloorTile> available = new List<FloorTile>();

        private void Start()
        {
            FloorTile[] tiles = GetComponentsInChildren<FloorTile>();

            foreach (FloorTile tile in tiles)
            {
                Vector2Int position = new Vector2Int(Mathf.RoundToInt((tile.transform.position.x + 8) / 4), Mathf.RoundToInt((tile.transform.position.z + 8) / 4));
                tile.gridPos = position;
                tile.myRoom = this;
                floorTiles.Add(position, tile);

                if (!tile.isLocked)
                {
                    available.Add(tile);
                }
            }

            int platforms = 0;

            while (platforms < 3)
            {
                int index = Random.Range(0, available.Count);

                FloorTile tile = available[index];

                tile.SetScale(0, 4);

                available.RemoveAt(index);

                platforms++;
            }
        }
    }
}
