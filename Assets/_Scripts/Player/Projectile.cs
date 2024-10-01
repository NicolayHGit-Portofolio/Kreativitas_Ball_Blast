using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeDuration;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile")) return;
        if (collision.gameObject.TryGetComponent<EnemyController>(out var enemy))
        {
            enemy.Hit(_damage);
        }
        Destroy(gameObject);
    }

    public void Init(int damage)
    {
        _damage = damage;
        StartCoroutine(ProjectileLifeDuration());
    }

    private IEnumerator ProjectileLifeDuration()
    {
        yield return new WaitForSeconds(_lifeDuration);

        Destroy(gameObject);
    }
}
