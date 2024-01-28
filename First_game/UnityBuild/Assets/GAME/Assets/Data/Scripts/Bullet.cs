using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _transform;
    private Transform _targetTransform;
    private float _deltaTime;

    public void Awake()
    {
        _transform = GetComponent<Transform>();
        _deltaTime = Time.deltaTime;
    }

    public void SetTarget(Transform enemyTransform)
    {
        _targetTransform = enemyTransform;
    }
    public void Move()
    {
        if (_targetTransform == null)
        {
            Destroy(transform.gameObject);
            return;
        }
        float distance = Vector3.Distance(_transform.position, _targetTransform.position);
        if (distance <= 0.05f)
        {
            Destroy(transform.gameObject);
            return;
        }
        float moveDistance = _speed * _deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _targetTransform.position, moveDistance);
        transform.LookAt(_targetTransform);
    }
    public void FixedUpdate()
    {
        Move();
    }
}
