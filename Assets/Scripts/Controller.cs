using UnityEngine;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private RectTransform BrickContainer;

        [SerializeField] private int AmountToSpawn;

        //TODO: implement time limit?

        private void Start() => SpawnBricks(AmountToSpawn);

        public void SpawnBricks(int amount)
        {
            float brickWidth = BrickPrefab.GetComponent<RectTransform>().rect.width;

            for (int i = 0; i < amount; i++)
            {
                //TODO: spawn and setup following a color/point system
                Brick brick = Instantiate(BrickPrefab, Vector3.right * i * brickWidth, BrickPrefab.transform.rotation, BrickContainer);
                brick.Setup(Color.blue, 10, AddPoints);
            }
        }

        private void AddPoints(int points)
        {
            Debug.Log("Adding points: " + points);
        }
    }
}