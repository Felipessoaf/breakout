using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D BoxCollider2D;
        [SerializeField] private Rigidbody2D Rigidbody2D;
        [SerializeField] private int Speed;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = (transform as RectTransform);
            BoxCollider2D.size = _rectTransform.rect.size;
        }

        private void Update()
        {
            UpdateMovement();
        }

        private void UpdateMovement()
        {
            Vector2 velocity = Vector2.zero;

            velocity += GetMovementVelocity(true);
            velocity += GetMovementVelocity(false);
            velocity.Normalize();

            float halfWidth = _rectTransform.rect.size.x / 2;
            float nextMoveRightLimit = velocity.x + transform.position.x + halfWidth;
            float nextMoveLeftLimit = velocity.x + transform.position.x - halfWidth;

            if(CheckBounds(true, nextMoveRightLimit, velocity.x) || CheckBounds(false, nextMoveLeftLimit, velocity.x))
            {
                velocity.x = 0;
            }

            Rigidbody2D.velocity = velocity * Speed;
        }

        private bool GetMovementInput(bool right) => 
            Input.GetKey(right ? KeyCode.RightArrow : KeyCode.LeftArrow);

        private Vector2 GetMovementVelocity(bool right) => GetMovementInput(right) ? GetMovementVector(right) : Vector2.zero;
        private Vector2 GetMovementVector(bool right) => right ? Vector2.right : Vector2.left;
        private bool CheckBounds(bool right, float nextMove, float xVelocity) => 
            right ? nextMove > Screen.width && xVelocity > 0 :
            nextMove < 0 && xVelocity < 0;
    }
}