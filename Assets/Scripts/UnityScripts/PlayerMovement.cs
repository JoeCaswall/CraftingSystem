using UnityEngine;

namespace UnityScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 5f;
        private Rigidbody2D _rb;
        private Vector2 _movement;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Get input from keyboard
            _movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
            _movement.y = Input.GetAxisRaw("Vertical"); // W/S or Up/Down
        }

        void FixedUpdate()
        {
            // Apply movement
            _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}