using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private EnemySpawnerController _enemySpawner;
    [SerializeField] private CanvasManager _canvasManager;

    [SerializeField] private int _score = 0;
    private List<EnemyController> _enemyList = new List<EnemyController>();

    private GameState _gameStarted;

    public static GameManager Instance { get; private set; }
    public GameState GameStarted => _gameStarted;



    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        _gameStarted = GameState.STARTING;
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _gameStarted = GameState.PLAYING;
        Time.timeScale = 1;
        _canvasManager.StartGame();
        HandleEnemies();
    }


    public void RegisterEnemyScene(EnemyController enemy)
    {
        _enemyList.Add(enemy);
    }

    public void EnemyKill(EnemyController enemy)
    {
        var value = (int)enemy.Type * enemy.MaxHealth;
        _score += value;
        _player.AddXp(value);

        _canvasManager.UpdateScore(_score);

        _enemyList.Remove(enemy);
        if (!enemy.HasChildren) HandleEnemies();
    }

    public void PlayerLevelUP()
    {
        _canvasManager.UpdateLevel(_player.Level);

        HandleEnemies();
    }

    public void PlayerDeath()
    {
        _gameStarted = GameState.FINISH;
        _canvasManager.LevelFinish(_score);
        Time.timeScale = 0;
    }

    public void HandleEnemies()
    {
        if (_enemyList.Count > 0 && _enemyList.Count >= Mathf.Round(_player.Level / 2)) return;

        int healthValue = (_player.Level * 2) * 2;
        _enemySpawner.SpawnEnemy((EnemyType)Mathf.Clamp(_player.Level,1,3), healthValue);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
