using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoting : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _levelModifier;

    private float _curAttackSpeed = 0;
    private int _curDamage;

    private void Update()
    {
        if (_curAttackSpeed > 0)
        {
            _curAttackSpeed -= Time.deltaTime;
            return;
        }

        if (!_controller.IsActive) return;
        Shoot();
    }

    private void Shoot()
    {
        _curDamage = Mathf.RoundToInt(_damage + (_levelModifier * _controller.Level));
        _curAttackSpeed = _attackSpeed - (_levelModifier * _controller.Level);

        Projectile proj = Instantiate(_projectilePrefab, _shotPoint.position, Quaternion.identity);
        proj.Init(_curDamage);
    }

}
