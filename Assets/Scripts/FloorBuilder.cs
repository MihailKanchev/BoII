using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FloorBuilder : MonoBehaviour
{
    public int maxRooms;

    private System.Random rnd;
    private bool rightDoor, leftDoor, frontDoor, bottomDoor;
    private Floor mesh;

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();
        mesh = new Floor(maxRooms);
        SetAdjacentRoomDoors();
        DrawDoors();
    }
    //Sets the doors for adjacent rooms
    private void SetAdjacentRoomDoors()
    {
        Vector2 position;
        position = new Vector2(mesh.GetRoom(0).x + 1, mesh.GetRoom(0).y);
        if (mesh.IsRoom(position))
        {
            rightDoor = true;
        }
        else
        {
            rightDoor = false;
        }
        position = new Vector2(mesh.GetRoom(0).x - 1, mesh.GetRoom(0).y);
        if (mesh.IsRoom(position))
        {
            leftDoor = true;
        }
        else
        {
            leftDoor = false;
        }
        position = new Vector2(mesh.GetRoom(0).x, mesh.GetRoom(0).y + 1);
        if (mesh.IsRoom(position))
        {
            frontDoor = true;
        }
        else
        {
            frontDoor = false;
        }
        position = new Vector2(mesh.GetRoom(0).x, mesh.GetRoom(0).y - 1);
        if (mesh.IsRoom(position))
        {
            bottomDoor = true;
        }
        else
        {
            bottomDoor = false;
        }
    }
    //Randomly asssigns(or does not assign) a door to a position towards which there is no room.
    private void GenerateRandomRooms()
    {
        if (!rightDoor)
        {
            if (rnd.Next(0, 3) == 0 && mesh.Size() <= mesh.maxRooms)
            {
                rightDoor = true;
            }
        }
        if (!leftDoor)
        {
            if (rnd.Next(0, 3) == 0 && mesh.Size() <= mesh.maxRooms)
            {
                leftDoor = true;
            }
        }
        if (!frontDoor)
        {
            if (rnd.Next(0, 3) == 0 && mesh.Size() <= mesh.maxRooms)
            {
                frontDoor = true;
            }
        }
        if (!bottomDoor)
        {
            if (rnd.Next(0, 3) == 0 && mesh.Size() <= mesh.maxRooms)
            {
                bottomDoor = true;
            }
        }
        if(!rightDoor && !leftDoor && !frontDoor && !bottomDoor)
        {
            GenerateRandomRooms();
        }
    }
    //Sets active door gameobjects if their boolean is true
    private void DrawDoors()
    {
        GenerateRandomRooms();
        if (rightDoor)
        {
            gameObject.transform.Find("RightDoor").gameObject.SetActive(true);
        }
        if (leftDoor)
        {
            gameObject.transform.Find("LeftDoor").gameObject.SetActive(true);
        }
        if (frontDoor)
        {
            gameObject.transform.Find("FrontDoor").gameObject.SetActive(true);
        }
        if (bottomDoor)
        {
            gameObject.transform.Find("BottomDoor").gameObject.SetActive(true);
        }
    }
}
public class Floor
{
    public int tail, maxRooms;
    private Vector2[] generatedRooms;

    //Sets up the head to the center of the mesh.
    public Floor(int _maxRooms)
    {
        maxRooms = _maxRooms;
        generatedRooms = new Vector2[maxRooms];
        generatedRooms[0] = new Vector2(50, 50);
        tail = 0;
    }
    //Checks if the Vector2 position that is given is a room.
    public bool IsRoom(Vector2 position)
    {
        for (int i = 0; i < generatedRooms.Length; i++)
        {
            if (position.Equals(generatedRooms[i]))
            {
                return true;
            }
        }
        return false;
    }
    //Checks if that position is of a room, if not, adds a room to that position
    public bool AddRoom(Vector2 position)
    {
        if (!IsRoom(position))
        {
            tail++;
            generatedRooms[tail] = position;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Size()
    {
        return generatedRooms.Length;
    }

    public Vector2 GetRoom(int index)
    {
        return generatedRooms[index];
    }
}
