using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class SpawnPointTile : GameTile
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private int _nextOnPathIndex;

    private List<Transform> _wayPoints = new List<Transform>();
    private int _distanceToDestination = 0;

    public List<Transform> WayPoints => _wayPoints;
    public int DistanceToDestination => _distanceToDestination;

    public override void Rotate()
    {
        _rotationIndex = (_rotationIndex + 1) % Rotation.rotations.Length;
        _arrow.transform.rotation = Rotation.rotations[_rotationIndex];
    }
    public void SetNextOnPathIndex(int index)
    {
        _nextOnPathIndex = index;
    }

    public void GrowthPath(GameTile[] tiles)
    {
        _wayPoints.Clear();
        PathTile nextOnPath = tiles[_nextOnPathIndex].GetComponent<PathTile>();
        if (nextOnPath != null)
        {
            _distanceToDestination = nextOnPath.GrowthPath(_wayPoints, tiles);
            if (_distanceToDestination > 0) _wayPoints.Add(nextOnPath.transform);
            _wayPoints.Reverse();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_wayPoints.Count > 0)
        {
            foreach (Transform point in _wayPoints)
            {
                Gizmos.DrawCube(point.position + Vector3.up * 0.25f, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
    }

}

