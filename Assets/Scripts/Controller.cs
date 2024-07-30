using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Breakout
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Brick BrickPrefab;
        [SerializeField] private RectTransform BrickContainer;

        [SerializeField] private List<Level> levels;

        public event Action OnLevelComplete;
        public event Action<int> OnScoreChanged;

        private int _bricksDestroyed;
        private int _bricksTarget;
        private int _currentLevel;
        private int _currentScore = 0;

        //TODO: implement time limit?

        private void Start() => LoadLevel(0);

        private void LoadLevel(int levelIndex)
        {
            _currentLevel = levelIndex;
            _bricksDestroyed = 0;
            _bricksTarget = 0;

            SpawnBricks(levels[_currentLevel]);
        }

        private void SpawnBricks(Level level)
        {
            float brickWidth = BrickPrefab.GetComponent<RectTransform>().rect.width;
            float brickheight = BrickPrefab.GetComponent<RectTransform>().rect.height;
            float yPadding = brickheight * 2;

            for(int lineIndex = 0; lineIndex < level.Lines.Count; lineIndex++)
            {
                Level.Line line = level.Lines[lineIndex];
                int bricksPerline = line.AmountOfBricks;
                for (int brickIndex = 0; brickIndex < bricksPerline; brickIndex++)
                {
                    Vector3 containerPosition = BrickContainer.transform.position;
                    float leftXPosition = containerPosition.x - ((bricksPerline * brickWidth) / 2);
                    float topYPosition = containerPosition.y + BrickContainer.rect.height / 2;

                    Vector3 startingPosition = new Vector3(leftXPosition + brickWidth / 2, topYPosition - (brickheight * lineIndex));
                    startingPosition.y -= yPadding;
                    Vector3 newPosition = startingPosition + Vector3.right * brickIndex * brickWidth;

                    Brick brick = Instantiate(BrickPrefab, newPosition, BrickPrefab.transform.rotation, BrickContainer);
                    brick.Setup(line.BrickColor, line.PointsToReward, AddPoints);

                    _bricksTarget++;
                }
            }
        }

        private void AddPoints(int points)
        {
            _currentScore += points;
            _bricksDestroyed++;

            OnScoreChanged?.Invoke(_currentScore);
            CheckLevelComplete();
        }

        private void CheckLevelComplete()
        {
            if (_bricksDestroyed >= _bricksTarget)
            {
                OnLevelComplete?.Invoke();
                _currentLevel++;

                if(_currentLevel < levels.Count)
                {
                    LoadLevel(_currentLevel);
                }
            }
        }
    }
}