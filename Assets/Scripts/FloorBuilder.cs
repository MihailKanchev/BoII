using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FloorBuilder : MonoBehaviour
{
    public int maxRooms;

    private int currentRooms;
    private Queue<Room> toVisit;
    private List<Room> rooms;

    // Start is called before the first frame update
    void Awake()
    {
        rooms = new List<Room>();
        toVisit = new Queue<Room>();

        Room room = new Room(new Vector2(0, 0));
        GenerateDoors(room);
        
        
    }

    //Generates all doors of the room (even the door from the direction it came from).
    //Then it pushes it to toVisit stack if there are more doors that are not visited.
    //Otherwise adds the room into the rooms List.
    public void GenerateDoors(Room room)
    {
        int allowedDoors = 4 - room.pathTail;
        //Lowers number of generated doors if it exceeds the maxRoom limit.
        if(allowedDoors > (maxRooms - currentRooms))
        {
            allowedDoors = maxRooms - currentRooms;
        }
        int doors = UnityEngine.Random.Range(1, allowedDoors);

        //Randomly generates a door in a direction where there is no door.
        while (doors > 0)
        {
            int direction = UnityEngine.Random.Range(0, 4);
            if(!IsInArray(room.path, direction))
            {
                room.path[room.pathTail] = direction;
                room.pathTail++;
                currentRooms++;
                doors--;
            }
        }

        //Adds it to rooms List if no paths are to visit. Pushes it to stack if more paths are to be visited.
        if(room.pathTail == room.isVisitedTail)
        {
            rooms.Add(room);
        }
        //If all paths of the room are visited, adds it to the rooms List.
        else
        {
            toVisit.Enqueue(room);
            GenerateRooms();
        }
    }
    //Generates all rooms in the toVisit stack
    public void GenerateRooms()
    {   if(toVisit.Count > 0)
        {
            Debug.Log(toVisit.Count);

            Room room = toVisit.Dequeue();
            int pathToVisit = UnityEngine.Random.Range(0, 4);
            while (IsInArray(room.GetAvailablePaths(), pathToVisit))
            {
                pathToVisit = UnityEngine.Random.Range(0, 4);
            }

            room.isVisited[room.isVisitedTail] = pathToVisit;
            room.isVisitedTail++;

            if (room.pathTail > room.isVisitedTail)
            {
                toVisit.Enqueue(room);
            }

            Room newRoom = new Room(new Vector2(0, 0));

            if (pathToVisit == 0)
            {
                newRoom = new Room(new Vector2(room.GetX(), room.GetY() + 1));
                newRoom.path[newRoom.pathTail] = 2;
                newRoom.isVisited[newRoom.isVisitedTail] = 2;
            }
            if (pathToVisit == 1)
            {
                newRoom = new Room(new Vector2(room.GetX() + 1, room.GetY()));
                newRoom.path[newRoom.pathTail] = 3;
                newRoom.isVisited[newRoom.isVisitedTail] = 3;
            }
            if (pathToVisit == 2)
            {
                newRoom = new Room(new Vector2(room.GetX(), room.GetY() - 1));
                newRoom.path[newRoom.pathTail] = 0;
                newRoom.isVisited[newRoom.isVisitedTail] = 0;
            }
            if (pathToVisit == 3)
            {
                newRoom = new Room(new Vector2(room.GetX() - 1, room.GetY()));
                newRoom.path[newRoom.pathTail] = 1;
                newRoom.isVisited[newRoom.isVisitedTail] = 1;
            }

            newRoom.pathTail++;
            newRoom.isVisitedTail++;

            GenerateDoors(newRoom);
        }
        else
        {
            GenerateFloorObjects();
        }
    }
    //Checks if the number exists inside the array
    public bool IsInArray(int[] array, int number)
    {
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == number)
            {
                return true;
            }
        }
        return false;
    }
    //Generates all room objects of the floor.
    public void GenerateFloorObjects()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            GameObject room = Instantiate(gameObject.transform.Find("Room").gameObject, 
                                          new Vector3(rooms[i].GetX() * 12, rooms[i].GetY() * 7, 0), 
                                          Quaternion.identity);
            room.SetActive(true);
            for (int n = 0; n < rooms[i].path.Length; n++)
            {
                if(rooms[i].path[n] == 0)
                {
                    room.transform.Find("FrontDoor").gameObject.SetActive(true);
                }
                if (rooms[i].path[n] == 1)
                {
                    room.transform.Find("RightDoor").gameObject.SetActive(true);
                }
                if (rooms[i].path[n] == 2)
                {
                    room.transform.Find("BottomDoor").gameObject.SetActive(true);
                }
                if (rooms[i].path[n] == 3)
                {
                    room.transform.Find("LeftDoor").gameObject.SetActive(true);
                }
            }
        }
    }

    public class Room
    {
        public Vector2 position;
        public int[] path;
        public int[] isVisited;
        public int pathTail, isVisitedTail;


        public Room(Vector2 _position)
        {
            isVisited = new int[4];
            path = new int[4];
            position = _position;
            pathTail = 0;
            isVisitedTail = 0;
        }

        public float GetX()
        {
            return position.x;
        }
        public float GetY()
        {
            return position.y;
        }

        //Returns an array of a Room with all paths that are not visited.
        public int[] GetAvailablePaths()
        {
            int[] availablePaths;
            if (pathTail != isVisitedTail && pathTail != 0 && isVisitedTail != 0)
            {
                availablePaths = new int[pathTail - isVisitedTail];
                int tail = 0;
                for (int i = 0; i < pathTail-1; i++)
                {
                    bool equals = true;
                    for (int n = 0; n < isVisitedTail-1; n++)
                    {
                        if (isVisited[n] != path[i])
                        {
                            equals = false;
                            break;
                        }
                    }
                    if (!equals)
                    {
                        availablePaths[tail] = path[i];
                        tail++;
                    }
                    equals = true;
                }
            }
            else
            {
                availablePaths = new int[0];
            }

            return availablePaths;
        }
    }
}

