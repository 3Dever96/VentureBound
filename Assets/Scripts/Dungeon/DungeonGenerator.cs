using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VentureBound.Dungeon
{
    public class DungeonGenerator : MonoBehaviour
    {
        [Header("Settings")]
        public int totalRoomCount = 64;
        [Range(0f, 1f)] public float branchChance = 0.7f;
        [Range(0f, 1f)] public float loopChance = 0.2f;
        public int testSeed;

        Dictionary<Vector2Int, RoomData> dungeon = new Dictionary<Vector2Int, RoomData>();
        Queue<Vector2Int> growthQueue = new Queue<Vector2Int>();

        public GameObject roomObject;

        private void Start()
        {
            GenerateLayout(testSeed);
        }

        public void GenerateLayout(int seed)
        {
            System.Random rng = new System.Random(seed);

            dungeon.Clear();
            growthQueue.Clear();

            Vector2Int startPos = Vector2Int.zero;
            RoomData root = new RoomData(startPos);
            dungeon.Add(startPos, root);
            growthQueue.Enqueue(startPos);

            while (growthQueue.Count > 0 && dungeon.Count < totalRoomCount)
            {
                Vector2Int currentPos = growthQueue.Dequeue();

                Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
                Shuffle(dirs, rng);

                foreach (Vector2Int dir in dirs)
                {
                    Vector2Int neighborPos = currentPos + dir;

                    if (!dungeon.ContainsKey(neighborPos))
                    {
                        if (rng.NextDouble() < branchChance)
                        {
                            RoomData newRoom = new RoomData(neighborPos);

                            newRoom.connections.Add(currentPos);
                            dungeon[currentPos].connections.Add(neighborPos);

                            dungeon.Add(neighborPos, newRoom);
                            growthQueue.Enqueue(neighborPos);
                        }
                   }
                }
            }

            AddLoops(rng);

            AssignSpecialRooms(rng);

            RenderDungeon();
        }

        void AddLoops(System.Random rng)
        {
            List<Vector2Int> positions = dungeon.Keys.ToList();

            foreach (Vector2Int pos in positions)
            {
                Vector2Int[] dirs = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

                foreach (Vector2Int dir  in dirs)
                {
                    Vector2Int neighborPos = pos + dir;

                    if (dungeon.ContainsKey(neighborPos) && !dungeon[pos].connections.Contains(neighborPos))
                    {
                        if (rng.NextDouble() < loopChance)
                        {
                            dungeon[pos].connections.Add(neighborPos);
                            dungeon[neighborPos].connections.Add(pos);
                        }
                    }
                }
            }
        }

        void AssignSpecialRooms(System.Random rng)
        {
            List<Vector2Int> availablePositions = dungeon.Keys.ToList();

            Vector2Int startPos = availablePositions[rng.Next(availablePositions.Count) - 1];
            dungeon[startPos].type = RoomType.Start;
            availablePositions.Remove(startPos);

            Vector2Int miniBossPos = availablePositions[rng.Next(availablePositions.Count) - 1];
            dungeon[miniBossPos].type = RoomType.MiniBoss;
            availablePositions.Remove(miniBossPos);

            Vector2Int bossPos = availablePositions[rng.Next(availablePositions.Count) - 1];
            dungeon[bossPos].type = RoomType.Boss;
            availablePositions.Remove(bossPos);
        }

        void RenderDungeon()
        {
            foreach (var pair in dungeon)
            {
                RoomData room = pair.Value;

                RoomObject newRoom = Instantiate(roomObject, transform).GetComponent<RoomObject>();
                newRoom.InitializeRoom(room);
            }
        }

        void Shuffle(Vector2Int[] array, System.Random rng)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Vector2Int temp = array[i];
                int randomIndex = rng.Next(array.Length);
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }
    }
}
