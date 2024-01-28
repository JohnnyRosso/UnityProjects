using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PathTile : GameTile
{
    [SerializeField] private Transform _arrow;
    [SerializeField] private int _nextOnPathIndex;
    
    public override void Rotate()
    {
        _rotationIndex = (_rotationIndex + 1) % Rotation.rotations.Length;
        _arrow.transform.rotation = Rotation.rotations[_rotationIndex];
    }
    public void SetNextOnPathIndex(int index)
    {
        _nextOnPathIndex = index;
    }
    public int GrowthPath(List<Transform> wayPoints,GameTile[] tiles)
    {
        GameTile nextOnPath = tiles[_nextOnPathIndex];
        if (nextOnPath == null) return -1;
        if (nextOnPath?.GetComponent<DestinationTile>())
        {
            wayPoints.Add(nextOnPath.transform);
            return 1;
        }
        else
        {
            PathTile nextOnPathPath = nextOnPath.GetComponent<PathTile>();
            if (nextOnPath != null)
            {
                int distance = nextOnPathPath.GrowthPath(wayPoints,tiles);
                if (distance < 0) return -1;
                else
                {
                    wayPoints.Add(nextOnPath.transform);
                    return distance + 1;
                }
            }
            else
            {
                return -1;
            }
        }
        
    }
}
