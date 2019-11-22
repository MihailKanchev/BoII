using UnityEngine;

namespace Assets.Scripts
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        public float playerSpeed; //speed player moves
        private Vector2 direction; //direction of movement
        private Rigidbody2D player;

        private void Start()
        {
            player = gameObject.GetComponent<Rigidbody2D>();
        }
        // Update is used for inputs.
        void Update()
        {
            ReceiveInput();
        }
        //Fixed update is mainly used for physics checks.
        private void FixedUpdate()
        {
            Move();
        }
        //Normalizes the "direction" and adds force to the rigidbody.
        void Move()
        {
            player.AddForce(direction.normalized * playerSpeed * Time.deltaTime, ForceMode2D.Force);
        }
        //Listens for Horizontal or Vertical input and saves it into a Vectoe2.
        void ReceiveInput()
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}

