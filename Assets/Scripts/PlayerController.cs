using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed; //speed player moves

        // Update is called once per frame
        void Update()
        {
            Move();
            //Debug.Log("I'm attached to " + gameObject.name);
        }

        //Sets playerSpeed appropriately
        void Move()
        {
            Vector3 v3 = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"), 0.0F);
            //Translate takes in a vector
            transform.Translate(playerSpeed * v3.normalized * Time.deltaTime);
        }
    }
}

