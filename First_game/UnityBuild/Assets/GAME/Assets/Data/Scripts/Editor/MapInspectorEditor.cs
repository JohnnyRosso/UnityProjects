using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapCreator))]
public class MapInspectorEditor : Editor
{
    private Event _guiEvent => Event.current;
    private MapCreator _mapCreator;
    private GameBoard _gameBoard => _mapCreator.GameBoard;
    private Ray _mouseRay => HandleUtility.GUIPointToWorldRay(_guiEvent.mousePosition);
    private GameTile _shownTile => _mapCreator.shownTile;
    private Plane _gameBoardPlane;
    private void OnEnable()
    {
        Initialization();
    }
    private void Initialization()
    {
        _mapCreator = SceneAsset.FindObjectOfType<MapCreator>();
        _gameBoardPlane = new Plane(Vector3.up, Vector3.zero);
    }
    public void OnSceneGUI()
    {
      
        if (_gameBoardPlane.Raycast(_mouseRay, out float position))
        {
            Vector3 worldPosition = _mouseRay.GetPoint(position);
            if (_shownTile != null) _shownTile.transform.position = worldPosition;
        }

        if (_mapCreator.TileType == GameTileType.SpawnPoint || _mapCreator.TileType == GameTileType.Path)
        {
            if (_guiEvent.control && _guiEvent.type == EventType.KeyDown && _guiEvent.keyCode == KeyCode.R)
            {
                switch (_mapCreator.TileType)
                {
                    case GameTileType.SpawnPoint:
                        SpawnPointTile spawnPoint = _shownTile.GetComponent<SpawnPointTile>();
                        spawnPoint.Rotate();
                        break;
                    case GameTileType.Path:
                        PathTile pathTile = _shownTile.GetComponent<PathTile>();
                        pathTile.Rotate();
                        break;
                }
            }
        }

        if ((_guiEvent.type == EventType.MouseDrag || _guiEvent.type == EventType.MouseDown) && _guiEvent.button == 0)
        {
            _gameBoard.PlaceTile(_mouseRay,_shownTile);
        }
        if (_guiEvent.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        }
    }
}
