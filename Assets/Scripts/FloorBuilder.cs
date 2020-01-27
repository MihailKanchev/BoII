using UnityEngine;

public class FloorBuilder : MonoBehaviour
{
    public int maxRooms;
    public int xAxis;
    public int yAxis;
    private int[,] grid;
    private int currentRooms;
    private bool generated;
    
    void Start() 
    {
        generated = false;
        currentRooms = 0;
        grid = new int[xAxis, yAxis];
        //Rougly estimate the center of the grid.
        int centerX = xAxis / 2;
        int centerY = yAxis / 2;
        grid[centerX, centerY] = 1;
        currentRooms++;
        gameObject.transform.Find("Isaac").position = new Vector3(centerX*12, centerY*7, 0);
    }
    void FixedUpdate()
    {
        if (currentRooms < maxRooms)
        {
            System.Random rnd = new System.Random();
            int x = rnd.Next(0, xAxis);
            int y = rnd.Next(0, yAxis);
            int neighbors = 0;
            if (x + 1 < xAxis && grid[x + 1, y] > 0)
            {
                neighbors++;
            }
            if (x - 1 >= 0 && grid[x - 1, y] > 0)
            {
                neighbors++;
            }
            if (y + 1 < yAxis && grid[x, y + 1] > 0)
            {
                neighbors++;
            }
            if (y - 1 >= 0 && grid[x, y - 1] > 0)
            {
                neighbors++;
            }
            if (neighbors <= 1 && neighbors > 0 && grid[x,y] == 0)
            {
                Debug.Log(x + "," + y);
                grid[x, y] = 2;
                currentRooms++;
            }
        }
        else if(generated == false)
        {
            GenerateFloorObjects();
            generated = true;
        }
    }
    //Generates all room objects of the floor.
    public void GenerateFloorObjects()
    {
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int n = 0; n < grid.GetLength(1); n++)
            {
                if(grid[i, n] > 0)
                {
                    GameObject room = Instantiate(gameObject.transform.Find("Room").gameObject,
                                      new Vector3(i * 12, n * 7, 0),
                                      Quaternion.identity);
                    room.SetActive(true);
                    if (i + 1 < xAxis && grid[i + 1, n] > 0)
                    {
                        room.transform.Find("RightDoor").gameObject.SetActive(true);
                    }
                    if (i - 1 >= 0 && grid[i - 1, n] > 0)
                    {
                        room.transform.Find("LeftDoor").gameObject.SetActive(true);
                    }
                    if (n + 1 < yAxis && grid[i, n + 1] > 0)
                    {
                        room.transform.Find("FrontDoor").gameObject.SetActive(true);
                    }
                    if (n - 1 >= 0 && grid[i, n - 1] > 0)
                    {
                        room.transform.Find("BottomDoor").gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}

