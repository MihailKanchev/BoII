using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed; //speed player moves
        private Vector3 direction;

        // Update is used for inputs.
        void Update()
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0);
        }

        //Fixed update is mainly used for physics checks.
        private void FixedUpdate()
        {
            Move();
        }

        //Sets playerSpeed appropriately
        void Move()
        {
            Vector3 newPosition = transform.position + (direction.normalized * playerSpeed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody>().MovePosition(newPosition);
        }
    }
}

