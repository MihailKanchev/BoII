using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Inspector Variables
    public float playerSpeed; //speed player moves
    public Animator head;
    public Animator body;
    public GameObject spriteBody;

    // Update is called once per frame
    void Update()
    {
        Move(); // Player Movement 
    }

    void Move()
    {

        if (Input.GetKey("right") || Input.GetKey("left") || Input.GetKey("up") || Input.GetKey("down"))
        {
            
            if (Input.GetKey("up"))
            {
                SetAnimatorVriables("isFace", false);
                SetAnimatorVriables("isVertical", true);
                SetAnimatorVriables("isRight", false);
                spriteBody.GetComponent<SpriteRenderer>().flipX = false;
                if (transform.position.y > 2.64)
                {
                    body.SetFloat("Speed", 0);
                    transform.Translate(0, 0, 0);
                }
                else
                {
                    body.SetFloat("Speed", 2);
                    transform.Translate(0, playerSpeed * Time.deltaTime, 0);
                }
            }
            if (Input.GetKey("down"))
            {
                SetAnimatorVriables("isFace", true);
                SetAnimatorVriables("isVertical", true);
                SetAnimatorVriables("isRight", false);
                spriteBody.GetComponent<SpriteRenderer>().flipX = false;
                if (transform.position.y < -2.24)
                {
                    body.SetFloat("Speed", 0);
                    transform.Translate(0, 0, 0);
                }
                else
                {
                    body.SetFloat("Speed", 2);
                    transform.Translate(0, -playerSpeed * Time.deltaTime, 0);
                }
            }
            if (Input.GetKey("left"))
            {
                SetAnimatorVriables("isFace", false);
                SetAnimatorVriables("isVertical", false);
                SetAnimatorVriables("isRight", false);
                spriteBody.GetComponent<SpriteRenderer>().flipX = true;
                if (transform.position.x < -4.3)
                {
                    body.SetFloat("Speed", 0);
                    transform.Translate(0, 0, 0);
                }
                else
                {
                    body.SetFloat("Speed", 2);
                    transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
                }
            }
            if (Input.GetKey("right"))
            {
                SetAnimatorVriables("isFace", false);
                SetAnimatorVriables("isVertical", false);
                SetAnimatorVriables("isRight", true);
                spriteBody.GetComponent<SpriteRenderer>().flipX = false;
                if (transform.position.x > 4.3)
                {
                    body.SetFloat("Speed", 0);
                    transform.Translate(0, 0, 0);
                }
                else
                {
                    body.SetFloat("Speed", 2);
                    transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
                }
            }
        }
        else
        {
            body.SetFloat("Speed", 0);
        }
    }

    void SetAnimatorVriables(string variable, bool setter)
    {
        switch (variable)
        {
            case "isVertical": if (head.GetBool("isVertical").Equals(setter) && body.GetBool("isVertical").Equals(setter)){
                                   break;
                               }
                               else{
                                   head.SetBool("isVertical", setter);
                                   body.SetBool("isVertical", setter);
                               }
                               break;

            case "isRight": if (head.GetBool("isRight").Equals(setter) && body.GetBool("isRight").Equals(setter)){
                                break;
                            }
                            else{
                                head.SetBool("isRight", setter);
                                body.SetBool("isRight", setter);
                            }
                            break;

            case "isFace": if (head.GetBool("isFace").Equals(setter)){
                               break;
                           }
                           else{
                               head.SetBool("isFace", setter);
                           }
                           break;
        }
    }
}
