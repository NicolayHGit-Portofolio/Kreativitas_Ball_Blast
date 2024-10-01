using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Transform _startPanel;
    [SerializeField] Transform _gameFinishPanel;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _scoreFinish;
    [SerializeField] Button _restartButton;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartLevel);
    }

    private void Start()
    {
        _gameFinishPanel.gameObject.SetActive(false);
    }

    private void RestartLevel()
    {
        GameManager.Instance.RestartScene();
    }

    public void StartGame()
    {
        _startPanel.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLevel(int level)
    {
        _levelText.text = "Level " + level;
    }

    public void LevelFinish(int score)
    {
        _gameFinishPanel.gameObject.SetActive(true);
        _scoreFinish.text = "Your Score: " + score;
    }

}
