using UnityEngine;

namespace Assets.Scripts
{
    /*This class acts as Player Character Animation Controller - it sets Isaac's head and body animation variables.
      It does two actions on Update:
      1. Sets the speed variable on head and on body. (Head should stop the animation earlier than the body as in the OG - Original Game)
      !!!The "Speed" variable is set after a check of the player.velocity.magnitude is made!!!
      2. Takes in player input and sets animation variables accordingly, as well as flips sprites on the X axis if needed.*/


    class AnimationController : MonoBehaviour
    {
        public Animator head, body; //Animator, used for its variables
        public GameObject spriteBody, spriteHead; //Sprites used to flip the animation
        private Rigidbody2D player;

        private void Start()
        {
            this.player = gameObject.GetComponent<Rigidbody2D>();
        }
        // Update is called once per frame
        private void Update()
        {
            SetAnimatorSpeedVariable();
            Animate();
        }
        //Changes animator variables of head and body accordingly
        void Animate()
        {   
            //Head animation handler
            if(Input.GetAxisRaw("Horizontal") > 0)
            {
                //Head variables
                head.SetBool("SideHead", true);
                head.SetBool("FrontHead", false);
                head.SetBool("BackHead", false);
                spriteHead.GetComponent<SpriteRenderer>().flipX = false;
                //Body variables
                body.SetBool("isVertical", false);
                spriteBody.GetComponent<SpriteRenderer>().flipX = false;
                
            }
            if(Input.GetAxisRaw("Horizontal") < 0)
            {
                //Head variables
                head.SetBool("SideHead", true);
                head.SetBool("FrontHead", false);
                head.SetBool("BackHead", false);
                spriteHead.GetComponent<SpriteRenderer>().flipX = true;
                //Body variables
                body.SetBool("isVertical", false);
                spriteBody.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(Input.GetAxisRaw("Vertical") > 0)
            {
                //Head variables
                head.SetBool("SideHead", false);
                head.SetBool("FrontHead", false);
                head.SetBool("BackHead", true);
                //Body variables
                body.SetBool("isVertical", true);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                //Head variables
                head.SetBool("SideHead", false);
                head.SetBool("FrontHead", true);
                head.SetBool("BackHead", false);
                //Body variables
                body.SetBool("isVertical", true);
            }
        }
        //Sets the Speed variable on body Animator.
        void SetAnimatorSpeedVariable()
        {
            if(player.velocity.magnitude < 1.9)
            {
                head.SetBool("SideHead", false);
                head.SetBool("FrontHead", true);
                head.SetBool("BackHead", false);
            }
            if (player.velocity.magnitude > 0.2)
            {
                body.SetFloat("Speed", 2);
            }
            else
            {
                body.SetFloat("Speed", 0);
            }
        }
    }
}
