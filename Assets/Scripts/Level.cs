using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "SO/Level")]
public class Level : ScriptableObject
{
    [field:SerializeField]
    public List<Line> Lines {  get; private set; }

    [Serializable]
    public class Line
    {
        [field: SerializeField]
        public int AmountOfBricks { get; private set; }

        [field: SerializeField]
        public int PointsToReward { get; private set; }

        [field: SerializeField]
        public Color BrickColor { get; private set; }
    }
}
