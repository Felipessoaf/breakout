using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private RectTransform BrickContainer;

        [SerializeField] private Level[] levels;

        //TODO: implement time limit?

        private void Start() => SpawnBricks(levels[0]);

        public void SpawnBricks(Level level)
        {
            float brickWidth = BrickPrefab.GetComponent<RectTransform>().rect.width;
            float brickheight = BrickPrefab.GetComponent<RectTransform>().rect.height;
            float yPadding = brickheight * 2;

            for(int lineIndex = 0; lineIndex < level.Lines.Count; lineIndex++)
            {
                int bricksPerline = level.Lines[lineIndex].AmountOfBricks;
                for (int brickIndex = 0; brickIndex < bricksPerline; brickIndex++)
                {
                    //TODO: spawn and setup following a color/point system
                    Vector3 containerPosition = BrickContainer.transform.position;
                    float leftXPosition = containerPosition.x - ((bricksPerline * brickWidth) / 2);
                    float topYPosition = containerPosition.y + BrickContainer.rect.height / 2;

                    Vector3 startingPosition = new Vector3(leftXPosition + brickWidth / 2, topYPosition - (brickheight * lineIndex));
                    startingPosition.y -= yPadding;
                    Vector3 newPosition = startingPosition + Vector3.right * brickIndex * brickWidth;

                    Brick brick = Instantiate(BrickPrefab, newPosition, BrickPrefab.transform.rotation, BrickContainer);
                    brick.Setup(Color.blue, 10, AddPoints);
                }
            }
        }

        private void AddPoints(int points)
        {
            Debug.Log("Adding points: " + points);
        }
    }
}