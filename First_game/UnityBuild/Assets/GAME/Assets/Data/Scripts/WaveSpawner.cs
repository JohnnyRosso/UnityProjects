using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _wayPoints;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _waveSize;
    [SerializeField] private float _coolDown;
    private float _coolDownTimer;
    private float _deltaTime;
    private int _enemyCount; 
    

    public void Awake()
    {
        _deltaTime = Time.deltaTime;
    }

    public void Spawn()
    {
        if (_enemyCount > _waveSize || _coolDownTimer < _coolDown) return;
        GameObject enemyObject = GameObject.Instantiate(_enemyPrefab, _spawnPoint.position, Quaternion.identity);
        _coolDownTimer = 0;
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetWayPoints(_wayPoints);
        _enemyCount++;
    }

    public void SetWayPoints(List<Transform> wayPoints)
    {
        _wayPoints = wayPoints;
    }
    public void FixedUpdate()
    {
        _coolDownTimer += _deltaTime;
        Spawn();
    }
    public void OnDrawGizmos()
    {
/*        foreach (Transform transform in _wayPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, new Vector3(1f, 1f, 1f));
        }*/

    }



}
