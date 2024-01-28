using System;
using UnityEngine;

[SelectionBase]
public class TowerDefault : Tower
{
    [SerializeField] private Transform _towerTop;
    [SerializeField] private Transform _gunPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _damage;
    [SerializeField, Range(0.1f, 10f)] private float _fireRate;
    [SerializeField, Range(1.5f, 5f)] private float _scanRadius = 1.5f;
    [SerializeField, Range(60f, 240f)] private float _rotationSpeed;

    [SerializeField, Range(0, 1)] private int _aimMode = 0;

    private float _shootProgress;

    private TargetPoint _target;

    private const int ENEMY_LAYER_MASK = 1 << 9;   // 00000000 00000001 => 00000010 00000000
    private const float AIM_ANGLE = 2f;

    private bool _isAimed;

    public void FixedUpdate()
    {
        GameUpdate();
    }

    public void GameUpdate()
    {
        _shootProgress += _fireRate * Time.deltaTime;
        if (_shootProgress > 1) _shootProgress = 1f;
        switch (_aimMode)
        {
            case 0:
                if (IsTargetTracked() || IsAcquireTarget())
                {
                    Rotation();
                    if (_isAimed)
                    {
                        Shoot();
                    }
                }
                break;
            case 1:
                if (IsAcquireTarget() || IsTargetTracked())
                {
                    Rotation();
                    if (_isAimed)
                    {
                        Shoot();
                    }
                }
                break;

        }
        /*        if (IsTargetTracked() || IsAcquireTarget())
                    *//*if (IsAcquireTarget() || IsTargetTracked())*//*
                    {
                        Rotation();
                        if (_isAimed)
                        {
                            Shoot();
                        }
                    }*/
    }

    private void Rotation()
    {
        Vector3 direction = (_target.transform.position - _towerTop.position);
        direction.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(direction);
        _towerTop.rotation = Quaternion.RotateTowards(_towerTop.rotation, rotation, _rotationSpeed * Time.deltaTime);

        if (AngleToTarget(_target.transform) <= 2f) _isAimed = true;
        else _isAimed = false;

    }
    private bool IsAcquireTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.localPosition, _scanRadius, ENEMY_LAYER_MASK);
        if (targets.Length > 0)
        {

            switch (_aimMode)
            {
                case 0:
                    _target = FindTargetClosestToDestination(targets);
                    break;
                case 1:
                    _target = FindClosestToTowerRotation(targets);
                    break;
            }
            return true;
        }
        _target = null;
        _isAimed = false;
        return false;
    }

    public TargetPoint FindTargetClosestToDestination(Collider[] targets)
    {
        TargetPoint target = targets[0].GetComponent<TargetPoint>();
        int minDistance = target.Enemy.DistanceToDestination;
        for (int i = 1; i < targets.Length; i++)
        {
            TargetPoint currentTarget = targets[i].GetComponent<TargetPoint>();
            if (currentTarget.Enemy.DistanceToDestination < minDistance)
            {
                target = currentTarget;
                minDistance = currentTarget.Enemy.DistanceToDestination;
            }
        }
        return target;
    }

    public TargetPoint FindClosestToTowerRotation(Collider[] targets)
    {
        TargetPoint target = targets[0].GetComponent<TargetPoint>();
        float minAngle = AngleToTarget(target.transform);
        for (int i = 1; i < targets.Length; i++)
        {
            TargetPoint currentTarget = targets[i].GetComponent<TargetPoint>();
            float currentAngle = AngleToTarget(currentTarget.transform);
            /*Debug.Log($"Current Angle = {currentAngle}");*/
            if (currentAngle < minAngle)
            {
                target = currentTarget;
                minAngle = currentAngle;
            }
        }
        return target;
    }

    private bool IsTargetTracked()
    {
        if (_target == null)
        {
            return false;
        }
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = _target.Position;
        if (Vector3.Distance(myPosition, targetPosition) > _scanRadius + _target.ColliderSize)
        {
            _target = null;
            return false;
        }
        return true;
    }

    private float AngleToTarget(Transform target)
    {
        Vector3 direction = (target.transform.position - _towerTop.position);
        direction.y = 0f;
        Vector3 currentDirection = (_gunPoint.position - _towerTop.position);
        currentDirection.y = 0f;
        return Vector3.Angle(direction, currentDirection);
    }

     

    private void Shoot()
    {
        if (_shootProgress == 1f)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _gunPoint.transform.position, Quaternion.identity);
            bullet.SetTarget(_target.transform);
            _target.Enemy.TakeDamage(_damage);
            _shootProgress -= 1f;
        }
 
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position, _scanRadius);
        if (_target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, _target.Position);
        }

    }
}
