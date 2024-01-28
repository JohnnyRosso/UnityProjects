using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SelectionBase]
public class GameTile : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private GameTileType _type;
    [SerializeField] protected int _rotationIndex = 0;
    public int RoationIndex => _rotationIndex;
    public virtual void Rotate()
    {
        _rotationIndex = (_rotationIndex + 1) % Rotation.rotations.Length;
    }

    private bool _show = false;
    public GameTileType Type => _type;

    public void Show()
    {
        _show = true;
    }

    private void OnDrawGizmos()
    {
        if (_show == true)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
        }
    }
}
    public enum GameTileType
{
    Empty,
    Ground,
    Path,
    TowerPlace,
    SpawnPoint,
    Destination
}
public class Rotation
{
    static public Quaternion[] rotations =
    {
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(90f, 90f, 0f),
        Quaternion.Euler(90f, 180f, 0f),
        Quaternion.Euler(90f, 270f, 0f),
    };
}