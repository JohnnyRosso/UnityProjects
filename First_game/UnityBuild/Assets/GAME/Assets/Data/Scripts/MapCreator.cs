using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    private GameTileType _tileType = GameTileType.TowerPlace;
    private GameBoard _gameBoard;
    private GameTile _shownTile;
    public GameTileType TileType => _tileType;
    public GameBoard GameBoard => _gameBoard;
    public GameTileFactory gameTileFactory;
    public GameTile shownTile => _shownTile;

    public void SetTileType(GameTileType value) //?
    {
        if (_tileType != value)
        {
            _tileType = value;
            if (_shownTile != null)
            {
                DestroyImmediate(_shownTile.gameObject);
            }
            _shownTile = Instantiate(gameTileFactory.Get(value));
        }
    }

    public void SetGameBoard(GameBoard board)
    {
        _gameBoard = board;
    }

    public void Destroy()
    {
        if (_shownTile != null)
        {
            DestroyImmediate(_shownTile.gameObject);
        }
    }
}
