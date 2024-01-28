using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObject))]
public class Enemy : MonoBehaviour
{
    public EnemyFactory OriginFactory { get; set; }

    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    private float _originalSpeed;
    private float _reward = 0f;
    private float _demage = 0f;

    private float DISTANCE_TO_SWITCH_POINT = 0.005f;

    private int _currentWayPoint = 0;
    private int _distanceToDestination = 0;

    public event Action<float> Dead;
    public event Action<float> AchievedBase;

    public int DistanceToDestination => _distanceToDestination;

    public void Initialize(float health, float speed, float reward, float damage)
    {
        _health = health;
        _speed = speed;
        _originalSpeed = _speed;
        _reward = reward;
        _demage = damage;
    }

    public void SpawnOn(GameTile tile)
    {
        transform.localPosition = tile.transform.localPosition;
    }

    public void SetWayPoints(List<Transform> pointsTransforms)
    {
        _wayPoints = pointsTransforms;
    }

    public void SetDistanceToDestionation(int distance)
    {
        _distanceToDestination = distance;
    }

    public bool GameUpdate()
    {
        if (_health <= 0f)
        {
            Dead?.Invoke(_reward);
            OriginFactory.Reclaim(this);
            return false;
        }
        if (Vector3.Distance(transform.position, _wayPoints[_currentWayPoint].position) < DISTANCE_TO_SWITCH_POINT)
        {
            _currentWayPoint += 1;
            _distanceToDestination -= 1;
        }

        if (_currentWayPoint >= _wayPoints.Count)
        {
            AchievedBase?.Invoke(_demage);
            OriginFactory.Reclaim(this);
            return false; 
        }

        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[_currentWayPoint].position, _speed * Time.deltaTime);
        return true;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public void ModifySpeed(float factor)
    {
        _speed = _originalSpeed * factor;
    }

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

}
