using Breakout;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] private Controller Controller;
    [SerializeField] private UIDocument UIDocument;

    private TextElement _scoreText;

    private void Start()
    {
        Controller.OnScoreChanged += ScoreChanged;
    }

    private void OnDestroy()
    {
        Controller.OnScoreChanged -= ScoreChanged;
    }

    private void OnEnable()
    {
        _scoreText = UIDocument.rootVisualElement.Q("ScoreText") as TextElement;
    }

    private void ScoreChanged(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }
}
