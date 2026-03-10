using System.Collections.Generic;
using UnityEngine;

namespace VentureBound.Dungeon
{
    public class DungeonGenerator : MonoBehaviour
    {
        // Prefab of the room object to populate the scene with.
        [SerializeField] GameObject roomObject;

        // The maximum number of rooms the dungeon can have.
        [SerializeField] int maxRooms;
        // The probability of a room leading to another.
        [SerializeField] float roomChance;

        // The room data of the dungeon.
        [System.NonSerialized] Dictionary<Vector2Int, RoomData> dungeonLayout = new Dictionary<Vector2Int, RoomData>();
        // The queue used to create the dungeon.
        [System.NonSerialized] Queue<Vector2Int> roomQueue = new Queue<Vector2Int>();

        void Start()
        {
            GenerateLayout(0);
        }

        void GenerateLayout(int seed)
        {
            // Initialize the dungeon seed and set the starting position
            System.Random rng = new System.Random(seed);
            Vector2Int startPos = Vector2Int.zero;

            // Add the starting room to the dungeon layout and to the queue.
            dungeonLayout.Add(startPos, new RoomData(startPos));
            roomQueue.Enqueue(startPos);

            // Increase the number of rooms created.
            int roomsCreated = 1;

            // Iterate through the queue until the dungeon is proper size.
            while (roomQueue.Count > 0 && roomsCreated < maxRooms)
            {
                // Get the next queued position for a room.
                Vector2Int currentPos = roomQueue.Dequeue();

                // Get the current room data of the queued position.
                RoomData currentRoom = dungeonLayout[currentPos];

                // Sync the doors of the room to its neighbors or set the possibility of new rooms.
                SyncRoomDoors(currentRoom, rng);

                // Array of vectors to check for new possible rooms and positions.
                Vector2Int[] dir = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.left, Vector2Int.down };

                for (var i = 0; i < dir.Length; i++)
                {
                    // Find the current neighbor cell position
                    Vector2Int neighborPos = currentPos + dir[i];

                    // Check if a new room can be created in the cell position
                    if (!dungeonLayout.ContainsKey(neighborPos) && currentRoom.hasDoor[i])
                    {
                        // Create a new room data
                        RoomData newRoom = new RoomData(neighborPos);
                        dungeonLayout.Add(neighborPos, newRoom);
                        roomQueue.Enqueue(neighborPos);
                        
                        // Increase the number of rooms in the dungeon and check if the dungeon is proper size yet.
                        roomsCreated++;

                        if (roomsCreated >= maxRooms)
                        {
                            break;
                        }
                    }
                }
            }

            // Get a list of all the positions in the dungeon layout.
            List<Vector2Int> finalList = new List<Vector2Int>(dungeonLayout.Keys);

            // Iterate through all the positions and create a new room object at those positions.
            for (var i = 0; i < finalList.Count; i++)
            {
                RoomObject newRoom = Instantiate(roomObject, transform).GetComponent<RoomObject>();
                // Pass in the corresponding room data to get information about the room.
                newRoom.InitializeRoom(dungeonLayout[finalList[i]]);
            }
        }

        void SyncRoomDoors(RoomData room, System.Random rng)
        {
            // A list of vectors to check for neighbors.
            List<Vector2Int> dir = new List<Vector2Int>() { Vector2Int.up, Vector2Int.right, Vector2Int.left, Vector2Int.down };

            for (var i = 0; i < dir.Count; i++)
            {
                if (dungeonLayout.ContainsKey(room.gridPos + dir[i]))
                {
                    // Sync the doors between the room and its neighbor.
                    /*
                     * 0 = Up
                     * 1 = Right
                     * 2 = Left
                     * 3 = Down
                     * 
                     * Think of the directional vectors as a D6.
                     * Just as opposite ends of a D6 add up to 7, the opposite directions add up to 3.
                     * To get the corresponding opposite direction in the neighbor room, simply subtract i from 3.
                     * 
                     */
                    RoomData nRoom = dungeonLayout[room.gridPos + dir[i]];
                    room.hasDoor[i] = nRoom.hasDoor[3 - i];
                    room.doorType[i] = nRoom.doorType[3 - i];
                }
                else
                {
                    // Set the doors randomly using the seed.
                    room.hasDoor[i] = rng.NextDouble() > roomChance;
                    room.doorType[i] = rng.Next(5);
                }
            }
        }
    }
}
