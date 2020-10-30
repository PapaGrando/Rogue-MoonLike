using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] private Text _textArea;

    //private event ScoreEventHandler _updateScore;

    void Awake()
    {
        _textArea = _textArea ?? GetComponentInChildren<Text>();
        
        var gameController = FindObjectOfType<GameController>();
        if (gameController != null)
            FindObjectOfType<GameController>().UpdateScore += UpdateScore;
        else
        {
            Debug.LogError($"{this.GetType().Name} : gameController not found. Score doesnt work");
            _textArea.text = "ERR";
        }
    }

    private void UpdateScore(Game game) => _textArea.text = $"{game.Location} - {game.Wave}";
}