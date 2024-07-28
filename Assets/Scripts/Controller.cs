using UnityEngine;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private RectTransform BrickContainer;

        [SerializeField] private int AmountToSpawn;

        private void Start() => SpawnBricks(AmountToSpawn);

        public void SpawnBricks(int amount)
        {
            float brickWidth = BrickPrefab.GetComponent<RectTransform>().rect.width;

            for (int i = 0; i < amount; i++)
            {
                Brick brick = Instantiate(BrickPrefab, Vector3.right * i * brickWidth, BrickPrefab.transform.rotation, BrickContainer);
                brick.Setup(Color.blue, 10);
            }
        }
    }
}