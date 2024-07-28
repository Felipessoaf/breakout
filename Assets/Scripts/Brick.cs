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
        private System.Action<int> _addPointsAction;

        private void Start() =>
            BoxCollider2D.size = (transform as RectTransform).rect.size;

        public void Setup(Color color, int points, System.Action<int> addPointsAction)
        {
            Image.color = color;
            _pointsToReward = points;
            _addPointsAction = addPointsAction;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Ball"))
            {
                _addPointsAction?.Invoke(_pointsToReward);
                Destroy(gameObject);
            }
        }
    }
}