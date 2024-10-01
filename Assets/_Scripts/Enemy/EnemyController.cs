using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private int _maxHealth;
    [SerializeField] private TextMeshProUGUI _enemyText;
    [SerializeField] private EnemyController _enemyChild;
    [SerializeField] private Vector2 _enemyOffset;

    private Rigidbody2D _rigidBody;
    private int _curHealth;

    public EnemyType Type => _type;
    public int MaxHealth => _maxHealth;
    public bool HasChildren => (_enemyChild && _enemyChild != null);

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _curHealth = _maxHealth;
        UpdateHealth();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out var player))
        {
            GameManager.Instance.PlayerDeath();
        }
    }

    private void UpdateHealth() 
    {
        _enemyText.text = _curHealth.ToString();
    }

    public void Spawn(int health, Vector2 force)
    {
        _curHealth = _maxHealth = health;
        UpdateHealth();

        _rigidBody.AddForce(force, ForceMode2D.Impulse);
    }


    public void Hit(int damage)
    {
        _curHealth -= damage;

        if (_curHealth > 0) UpdateHealth();
        else EnemyDie();
    }

    public void EnemyDie()
    {
        bool hasChildren = (_enemyChild && _enemyChild != null);
        
        GameManager.Instance.EnemyKill(this);

        if (hasChildren)
            SpawnChildren();

        Destroy(gameObject);
    }

    public void SpawnChildren()
    {
        Vector2 secondOffset = new Vector2(-1, 1);
        int childHealth = (int)Mathf.Ceil(_maxHealth / 2);
        childHealth = (childHealth <= 0) ? 1 : childHealth;

        Vector2 force = new(-1, 1);

        var enemy1 = Instantiate(_enemyChild, (Vector2)transform.position + _enemyOffset, Quaternion.identity);
        enemy1.Spawn(childHealth, force.normalized * 2);

        var enemy2 = Instantiate(_enemyChild, (Vector2)transform.position + (_enemyOffset * secondOffset), Quaternion.identity);
        enemy2.Spawn(childHealth, (force.normalized * secondOffset) * 2);
    }
}
