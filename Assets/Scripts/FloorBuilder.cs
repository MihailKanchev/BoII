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

    void Awake()
    {
        rooms = new List<Room>();
        toVisit = new Queue<Room>();

        Room room = new Room(new Vector2(0, 0));
        GenerateDoors(room);
    }
    void FixedUpdate()
    {
        
    }
    //Generates all doors of the room (even the door from the direction it came from).
    //Then it pushes it to toVisit stack if there are more doors that are not visited.
    //Otherwise adds the room into the rooms List.
    public void GenerateDoors(Room room)
    {
        System.Random rnd = new System.Random();
        int allowedDoors = 5 - room.pathTail;
        //Lowers number of generated doors if it exceeds the maxRoom limit.
        if (allowedDoors > (maxRooms - currentRooms))
            allowedDoors = maxRooms - currentRooms;

        int doors = 0; ;
        if (allowedDoors > 1)
        {
            doors = rnd.Next(0, allowedDoors);
        }
        else
        {
            doors = rnd.Next(0, 2);
        }
        //GOES BOOM HERE
        //Randomly generates a door in a direction where there is no door.
        while (doors != 0)
        {
            int direction = rnd.Next(0, 4);
            int[] test = room.GetAvailablePaths();
            bool test1 = IsInArray(test, direction);
            if (test1)
            {
                room.path[room.pathTail-1] = direction;
                room.pathTail++;
                currentRooms++;
                doors--;
            }
        }
        Debug.Log(0);
        //If all paths of the room are visited, add it to the rooms List.
        if (room.GetAvailablePaths().Length == 0)
        {
            rooms.Add(room);
            if(toVisit.Count == 0)
            {
                GenerateFloorObjects();
            }
        }

        //Adds it to rooms List if no paths are to visit. Pushes it to stack if more paths are to be visited.
        else
        {
            toVisit.Enqueue(room);
            StartCoroutine(GenerateRooms());
        }
       
    }
    //Generates all rooms in the toVisit stack
    public IEnumerator GenerateRooms()
    {
        
        Room room = toVisit.Dequeue();
        int pathToVisit;
        bool b = true;
        System.Random rnd = new System.Random();
        do
        {
            yield return new WaitForSeconds(.1f);
            pathToVisit = rnd.Next(0, 4);
            if (IsInArray(room.GetAvailablePaths(), pathToVisit))
            {
                if (!IsRoom(room.position, pathToVisit))
                    b = false;
            }
        }
        while (b);

        room.isVisited[room.isVisitedTail-1] = pathToVisit;
        room.isVisitedTail++;

        if (room.pathTail > room.isVisitedTail)
        {
            toVisit.Enqueue(room);
        }

        Room newRoom;

        switch (pathToVisit)
        {
            case 0:
                newRoom = new Room(new Vector2(room.GetX(), room.GetY() + 1));
                newRoom.path[newRoom.pathTail - 1] = 2;
                newRoom.isVisited[newRoom.isVisitedTail - 1] = 2;
                break;
            case 1:
                newRoom = new Room(new Vector2(room.GetX() + 1, room.GetY()));
                newRoom.path[newRoom.pathTail - 1] = 3;
                newRoom.isVisited[newRoom.isVisitedTail - 1] = 3;
                break;
            case 2:
                newRoom = new Room(new Vector2(room.GetX(), room.GetY() - 1));
                newRoom.path[newRoom.pathTail - 1] = 0;
                newRoom.isVisited[newRoom.isVisitedTail - 1] = 0;
                break;
            case 3:
                newRoom = new Room(new Vector2(room.GetX() - 1, room.GetY()));
                newRoom.path[newRoom.pathTail - 1] = 1;
                newRoom.isVisited[newRoom.isVisitedTail - 1] = 1;
                break;
        }

        newRoom.pathTail++;
        newRoom.isVisitedTail++;

        GenerateDoors(newRoom);
    }
    //Checks if the number exists inside the array
    public bool IsInArray(int[] array, int number)
    {
        if (array.Length > 0)
        {
            for (int i = 0; i < array.Length-1; i++)
            {
                if (array[i] == number)
                {
                    return true;
                }
            }
        }
        return false;
    }
    //Generates all room objects of the floor.
    public IEnumerator GenerateFloorObjects()
    {
        for (int i = 0; i < rooms.Count-1; i++)
        {
            yield return new WaitForSeconds(.1f);
            GameObject room = Instantiate(gameObject.transform.Find("Room").gameObject,
                                          new Vector3(rooms[i].GetX() * 12, rooms[i].GetY() * 7, 0),
                                          Quaternion.identity);
            room.SetActive(true);
            for (int n = 0; n < rooms[i].pathTail-1; n++)
            {
                switch (rooms[i].path[n])
                {
                    case 0:
                        room.transform.Find("FrontDoor").gameObject.SetActive(true);
                        break;
                    case 1:
                        room.transform.Find("RightDoor").gameObject.SetActive(true);
                        break;
                    case 2:
                        room.transform.Find("BottomDoor").gameObject.SetActive(true);
                        break;
                    case 3:
                        room.transform.Find("LeftDoor").gameObject.SetActive(true);
                        break;
                }
            }
        }
    }
    //Checks if the location has a room already existing there
    public bool IsRoom(Vector2 position, int number)
    {
        switch (number)
        {
            case 0:
                position.y++;
                break;
            case 1:
                position.x++;
                break;
            case 2:
                position.y--;
                break;
            case 3:
                position.x--;
                break;
        }
        for (int i = 0; i < rooms.Count-1; i++)
        {
            if (position.Equals(rooms[i].position))
            {
                return true;
            }
        }
        return false;
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

        //THIS IS INCONSISTANT MAYBE?
        //Returns an array of a Room with all paths that are not visited.
        public int[] GetAvailablePaths()
        {
            int[] availablePaths;
            if(pathTail != isVisitedTail)
            {
                availablePaths = new int[4];
                int tail = 0;
                bool equals = false;
                for (int i = 0; i < pathTail - 1; i++)
                {
                    for (int n = 0; n < isVisitedTail - 1; n++)
                    {
                        if (isVisited[n] == path[i])
                        {
                            equals = true;

                        }
                    }
                    if (!equals)
                    {
                        availablePaths[tail] = path[i];
                        tail++;
                    }
                    equals = false;
                }
                int[] returnArray = new int[tail - 1];
                for (int i = 0; i < returnArray.Length - 1; i++)
                {
                    returnArray[i] = availablePaths[i];
                }
                availablePaths = returnArray;
            }
            else
            {
                availablePaths = new int[0];
            }

            return availablePaths;
        }
    }
}

