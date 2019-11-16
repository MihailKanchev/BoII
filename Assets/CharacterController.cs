using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //Inspector Variables
    public float playerSpeed; //speed player moves

    // Update is called once per frame
    void Update()
    {
        MoveForward(); // Player Movement 
    }

    void MoveForward()
    {
        if (Input.GetKey("up"))//Press up arrow key to move forward on the Y AXIS
        {
            if (transform.position.y > 3f)
            {
                transform.Translate(0, 0, 0);
            }
            else
            {
                transform.Translate(0, playerSpeed * Time.deltaTime, 0);
            }
            
            

        }
        if (Input.GetKey("down"))
        {

            if (transform.position.y < -1.8f)
            {
                transform.Translate(0, 0, 0);
            }
            else
            {
                transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
            }

        }
        if (Input.GetKey("left"))
        {
            if (transform.position.x < -4.3f)
            {
                transform.Translate(0, 0, 0);
            }
            else
            {
                transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey("right"))
        {
            if (transform.position.x > 4.3f)
            {
                transform.Translate(0, 0, 0);
            }
            else
            {
                transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            }
        }
        
        
    }
}
