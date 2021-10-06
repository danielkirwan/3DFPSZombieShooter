using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public enum EnemyState
    {
        Idle, Chase, Attack
    }

    private Transform _target;
    [SerializeField] private float _speed;
    private CharacterController _controller;
    private Vector3 _velocity;
    private float _gravity = 20;
    private int _damage = 25;
    private Health _playerHealth;
    private float _attackCooldown = 1.5f;
    private float _nextAttack = -1f;

    [SerializeField] private EnemyState _currentState = EnemyState.Chase;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _target.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Movement();
                break;
        }
        
    }

    public void CanAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void Chase()
    {
        _currentState = EnemyState.Chase;
    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if(_playerHealth != null)
            {
                _playerHealth.TakeDamage(_damage);
                _nextAttack = Time.time + _attackCooldown;
            }
        }
    }

    private void Movement()
    {
        if (_controller.isGrounded)
        {
            Vector3 direction = _target.position - transform.position;
            direction.Normalize();
            //stops tilting
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;
        }

        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

}
