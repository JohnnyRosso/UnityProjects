using UnityEngine;
using UnityEditor;


public class MapInspectorEditorWindow : EditorWindow
{
    private Rect _menuSection;
    private GameTileType _tileType = GameTileType.Empty;
    private Vector2Int _size;
    private GameBoard _boardPrefab;
    private MapCreator _mapCreatorPrefab;
    private MapCreator _mapCreator;
    private GameBoard _board;
    private GameTileFactory _gameTileFactory;
    [MenuItem("Window/Map Editor")]
    static void OpenWindow()
    {
        MapInspectorEditorWindow window = (MapInspectorEditorWindow)GetWindow(typeof(MapInspectorEditorWindow));
        window.minSize = new Vector2(600, 300);
        window.maxSize = new Vector2(1280, 920);
        window.Show();
    }

    

    private void OnEnable()
    {
        Iinit();
    }

    private void OnDestroy()
    {
        if(_mapCreator != null)
        {
            _mapCreator.Destroy();
            DestroyImmediate(_mapCreator.gameObject);
        }
    }

    private void Iinit()
    {
        _boardPrefab = Resources.Load<GameBoard>("Prefabs/Levels/EmptyGameBoard");
        _mapCreatorPrefab = Resources.Load<MapCreator>("Prefabs/Levels/MapCreator");
        _gameTileFactory = Resources.Load<GameTileFactory>("Setup/TileFactory");
        if (SceneAsset.FindObjectOfType<MapCreator>() == null)
        {
            _mapCreator = Instantiate(_mapCreatorPrefab);
            _mapCreator.name = "Map Creator";
        }
        else
        {
            _mapCreator = SceneAsset.FindObjectOfType<MapCreator>();
        }
        _mapCreator.gameTileFactory = _gameTileFactory;
    }

    private void OnGUI()
    {
        SyncBoard();
        DrawLayouts();
        DrawHeader();
        DrawBITCH();
    }

    private void DrawLayouts()
    {
        _menuSection.x = 0;
        _menuSection.y = 0;
        _menuSection.width = Screen.width;
        _menuSection.height = 100;


        _tileType = (GameTileType)EditorGUILayout.EnumPopup(_tileType, GUILayout.Height(40));
        _size = (Vector2Int)EditorGUILayout.Vector2IntField("Board Size", _size, GUILayout.Height(40));


        _mapCreator.SetTileType(_tileType);


        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            CreateBoard();
        }
        if (GUILayout.Button("GrowthPath!", GUILayout.Height(40)) && _board != null)
        {
            _board.GrowthPath();
        }

    }

    private void DrawHeader()
    {

    }

    private void DrawBITCH()
    {

    }

    private void SyncBoard()
    {
        if (_board == null)
        {
            if (SceneAsset.FindObjectOfType<GameBoard>() != null)
            {
                _board = SceneAsset.FindObjectOfType<GameBoard>();
                _mapCreator.SetGameBoard(_board);
                _size = _board.Size;
            }
        }
        else 
        {
            _mapCreator.SetGameBoard(_board);
        }
    }

    private void CreateBoard()
    {
        if (_board != null)
        {

            DestroyImmediate(_board.gameObject);
            _board = Instantiate(_boardPrefab);
            _board.name = "Game Board";
            _board.Initialize(_size, _gameTileFactory);
            _mapCreator.SetGameBoard(_board);
        }
        else
        {
            _board = Instantiate(_boardPrefab);
            _board.name = "Game Board";
            _board.Initialize(_size, _gameTileFactory);
        }
    }
}
