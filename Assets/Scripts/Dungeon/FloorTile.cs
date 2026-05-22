using UnityEngine;

namespace VentureBound
{
    public class FloorTile : MonoBehaviour
    {
        public bool isLocked;
        public Vector2Int gridPos;
        public RoomObject myRoom;

        [SerializeField] private Color[] colors;

        public void SetScale(int min, int max)
        {
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };

            int scale = Random.Range(min, max);

            Renderer myRenderer = GetComponentInChildren<Renderer>();
            myRenderer.material.color = colors[scale];

            if (scale != 0)
            {
                transform.localScale = new Vector3(1, scale * 2f, 1);
            }

            if (scale == 3)
            {
                int index = Random.Range(0, 4);

                if (myRoom.floorTiles.ContainsKey(gridPos + directions[index]))
                {
                    FloorTile neighbor = myRoom.floorTiles[gridPos + directions[index]];

                    if (!neighbor.isLocked && neighbor.transform.localScale.y < 1f)
                    {
                        neighbor.SetScale(1, max - 1);
                    }
                }
            }
        }
    }
}
