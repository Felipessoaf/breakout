using UnityEngine;
using UnityEngine.UI;

namespace Breakout
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D BoxCollider2D;
        [SerializeField] private Animator Animator;
        [SerializeField] private Image Image;

        private int _pointsToReward;

        private void Start() =>
            BoxCollider2D.size = (transform as RectTransform).rect.size;

        public void Setup(Color color, int points)
        {
            Image.color = color;
            _pointsToReward = points;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Ball"))
            {
                //TODO: reward points
                Destroy(gameObject);
            }
        }
    }
}