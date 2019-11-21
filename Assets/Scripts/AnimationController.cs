using UnityEngine;

namespace Assets.Scripts
{
    class AnimationController : MonoBehaviour
    {
        public Animator head, body; //Animator, used for its variables
        public GameObject spriteBody, spriteHead; //Sprites used to flip the animation

        // Update is called once per frame
        private void Update()
        {
            Animate();
        }

        //Changes animator variables of head and body accordingly
        void Animate()
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                SetAnimatorVriables("isFace", false);
                SetAnimatorVriables("isVertical", true);
                body.SetFloat("Speed", 2);
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                SetAnimatorVriables("isFace", true);
                SetAnimatorVriables("isVertical", true);
                body.SetFloat("Speed", 2);
            }
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SetAnimatorVriables("isVertical", false);
                SetAnimatorVriables("flipX", true);
                body.SetFloat("Speed", 2);
            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SetAnimatorVriables("isVertical", false);
                SetAnimatorVriables("flipX", false);
                body.SetFloat("Speed", 2);
            }
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                body.SetFloat("Speed", 0);
            }
        }
        //Grouped up methods to change animator variables, first is the action, then the boolean.
        //ex. SetAnimatorVariables("someAction", true)
        //See actions in the switch cases
        void SetAnimatorVriables(string variable, bool setter)
        {
            switch (variable)
            {
                case "isVertical":
                    if (head.GetBool("isVertical") != setter && body.GetBool("isVertical") != setter)
                    {
                        head.SetBool("isVertical", setter);
                        body.SetBool("isVertical", setter);
                    }
                    break;
                case "isFace":
                    if (head.GetBool("isFace") != setter)
                    {
                        head.SetBool("isFace", setter);
                    }
                    break;
                case "flipX":
                    if (spriteHead.GetComponent<SpriteRenderer>().flipX != setter && spriteBody.GetComponent<SpriteRenderer>().flipX != setter)
                    {
                        spriteBody.GetComponent<SpriteRenderer>().flipX = setter;
                        spriteHead.GetComponent<SpriteRenderer>().flipX = setter;
                    }
                    break;
            }
        }
    }
}
