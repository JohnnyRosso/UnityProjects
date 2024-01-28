using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileFactory : ScriptableObject
{
    [SerializeField] private GameTile _emptyPrefab;
    [SerializeField] private GameTile _groundPrefab;
    [SerializeField] private GameTile _towerPlacePrefab;
    [SerializeField] private GameTile _spawnPointTile;
    [SerializeField] private GameTile _pathTile;
    [SerializeField] private GameTile _destinationTile;

    public GameTile Get(GameTileType type)
    {
        switch (type)
        {
            case GameTileType.Empty:
                return _emptyPrefab;
            case GameTileType.Ground:
                return _groundPrefab;
            case GameTileType.TowerPlace:
                return _towerPlacePrefab;
            case GameTileType.SpawnPoint:
                return _spawnPointTile;
            case GameTileType.Path:
                return _pathTile;
            case GameTileType.Destination:
                return _destinationTile;

        }
        return null;
    }
}
