using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBoard : MonoBehaviour
{
    [SerializeField] private Transform _ground;
    [SerializeField] private Vector2Int _size;
    public Vector2Int Size => _size;

    [SerializeField] private GameTile[] _tiles;
    private GameTileFactory _tileFactory;

    [SerializeField] private List<SpawnPointTile> _spawnPoints = new List<SpawnPointTile>();
    public int SpawnPointCount => _spawnPoints.Count;

    public void Initialize(Vector2Int size, GameTileFactory contentFactory)
    {
        _size = size;
        _ground.localScale = new Vector3(size.x, size.y, 1f);

        Vector2 offset = new Vector2((size.x - 1) * 0.5f, (size.y - 1) * 0.5f);

        _tiles = new GameTile[size.x * size.y];
        _tileFactory = contentFactory;
        for (int i = 0, y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++, i++)
            {
                GameTile tile = _tiles[i] = Instantiate(_tileFactory.Get(GameTileType.Empty));
                tile.transform.SetParent(transform, false);
                tile.transform.localPosition = new Vector3(x - offset.x, 0f, y - offset.y);
            }
        }
        _spawnPoints.Clear();
    }

    public GameTile GetTile(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue,1))
        {
            if ((hit.point.x + _size.x * 0.5f) < 0 || (hit.point.z + _size.y * 0.5f) < 0) return null;
            int x = (int)(hit.point.x + _size.x * 0.5f);
            int y = (int)(hit.point.z + _size.y * 0.5f);
            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return _tiles[x + y * _size.x];
            }
        }
        return null;
    }

    public int GetTileIndex(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            int x = (int)(hit.point.x + _size.x * 0.5f);
            int y = (int)(hit.point.z + _size.y * 0.5f);
            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                return x + y * _size.x;
            }
        }
        return -1;
    }

    public void PlaceTile(Ray ray, GameTile tile)
    {
        int tileIndex = GetTileIndex(ray);
        if (tileIndex < 0) return;

        GameTile targetTile = _tiles[tileIndex];
        if (targetTile.Type == tile.Type) return;
        GameTile newTile = Instantiate(tile);
        newTile.transform.SetParent(transform, false);
        newTile.transform.localPosition = targetTile.transform.localPosition;
        switch (targetTile.Type)
        {
            case GameTileType.SpawnPoint:
                _spawnPoints.Remove(targetTile.GetComponent<SpawnPointTile>());
                break;
        }
        DestroyImmediate(targetTile.gameObject);
        switch (newTile.Type)
        {
            case GameTileType.SpawnPoint:
                SpawnPointTile spawnPoint = newTile.GetComponent<SpawnPointTile>();
                _spawnPoints.Add(spawnPoint);
                spawnPoint.SetNextOnPathIndex(NextOnPathIndex(spawnPoint.RoationIndex, tileIndex));
                break;
            case GameTileType.Path:
                PathTile pathTile = newTile.GetComponent<PathTile>();
                pathTile.SetNextOnPathIndex(NextOnPathIndex(pathTile.RoationIndex, tileIndex));
                break;

        }
        _tiles[tileIndex] = newTile;
    }
    private int NextOnPathIndex(int rotationIndex, int currentIndex)
    {
        int nextOnPathIndex = currentIndex;
        switch (rotationIndex)
        {
            case 0:
                nextOnPathIndex += 1;
                if (nextOnPathIndex >= _size.x * _size.y || nextOnPathIndex < 0) break;
                return nextOnPathIndex;

            case 1:
                nextOnPathIndex -= _size.x;
                if (nextOnPathIndex >= _size.x * _size.y || nextOnPathIndex < 0) break;
                return nextOnPathIndex;
                
            case 2:
                nextOnPathIndex -= 1;
                if (nextOnPathIndex >= _size.x * _size.y || nextOnPathIndex < 0) break;
                return nextOnPathIndex;
                
            case 3:
                nextOnPathIndex += _size.x;
                if (nextOnPathIndex >= _size.x * _size.y || nextOnPathIndex < 0) break;
                return nextOnPathIndex;
                
        }
        return nextOnPathIndex;
    }
    public SpawnPointTile GetSpawnPoint(int index)
    {
        return _spawnPoints[index];
    }
    public void GrowthPath()
    {
        if (_spawnPoints.Count > 0)
        {
            foreach (SpawnPointTile spawnPoint in _spawnPoints)
            {
                spawnPoint.GrowthPath(_tiles);
            }
        }
    }

    public void PlaceTower(TowerPlaceTile tile,Tower tower)
    {
        tile.PlaceTower(tower);
    }
}
