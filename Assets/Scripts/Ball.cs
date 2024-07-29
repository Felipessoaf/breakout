using UnityEngine;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Vector2 InitialImpulseMin, InitialImpulseMax;
        [SerializeField] private Rigidbody2D Rigidbody2D;

        [SerializeField] private Controller Controller;

        private bool _waitingForLaunch = true;
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;

            Controller.OnLevelComplete += ResetLaunch;
        }

        private void OnDestroy()
        {
            Controller.OnLevelComplete -= ResetLaunch;
        }

        private void Update()
        {
            if (_waitingForLaunch && Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }

        private void Launch()
        {
            _waitingForLaunch = false;

            float randomX = Random.Range(InitialImpulseMin.x, InitialImpulseMax.x);
            float randomY = Random.Range(InitialImpulseMin.y, InitialImpulseMax.y);

            randomX *= (Random.value > 0.5f) ? 1 : -1;

            Vector2 initialImpulse = new Vector2(randomX, randomY);
            Rigidbody2D.AddForce(initialImpulse, ForceMode2D.Impulse);
        }

        private void ResetLaunch()
        {
            _waitingForLaunch = true;
            Rigidbody2D.velocity = Vector3.zero;
            transform.position = _initialPosition;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("BottomBoundary"))
            {
                //TODO: maybe implement lifes/tries?
                ResetLaunch();
            }
        }   
    }
}