using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorGame : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameTileFactory _tileFactory;
    [SerializeField] private GameObject _menuSounds;


    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private GameBoard _board;
    private Plane _gameBoardPlane;
    private GameTile _showTile;

    private void Awake()
    {
        Initialization();
    }
    private void OnEnable()
    {
        if (_board == null)
        {
            _board = FindObjectOfType<GameBoard>();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           _board.PlaceTile(TouchRay,_showTile);
        }
        ShowTile();
    }

    private void Initialization()
    {
        _gameBoardPlane = new Plane(Vector3.up, Vector3.zero);
        if (_tileFactory == null)
        {
            _tileFactory = Resources.Load<GameTileFactory>("Resources/Setup/TileFactory");
        }
    }

    private void ShowTile()
    {
        if (_gameBoardPlane.Raycast(TouchRay, out float position))
        {
            Vector3 worldPosition = TouchRay.GetPoint(position);
            if (_showTile != null)
            {
                GameTile targetTile = _board.GetTile(TouchRay);
                if (targetTile != null)
                {
                    _showTile.transform.position = targetTile.gameObject.transform.position;
                }
                else
                {
                    _showTile.transform.position = worldPosition;
                }
                
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                switch (_showTile.Type)
                {
                    case GameTileType.SpawnPoint:
                        SpawnPointTile spawnPoint = _showTile.GetComponent<SpawnPointTile>();
                        spawnPoint.Rotate();
                        break;
                    case GameTileType.Path:
                        PathTile pathTile = _showTile.GetComponent<PathTile>();
                        pathTile.Rotate();
                        break;
                }
            }
        }
    }

    public void ToggleShowTile(int type)
    {
        if (_showTile != null) Destroy(_showTile.gameObject);
        switch (type)
        {
            case 0: 
                _showTile = Instantiate(_tileFactory.Get(GameTileType.Empty));
                break;
            case 1:
                _showTile = Instantiate(_tileFactory.Get(GameTileType.Ground));
                break;
            case 2:
                _showTile = Instantiate(_tileFactory.Get(GameTileType.Path));
                break;
            case 3:
                _showTile = Instantiate(_tileFactory.Get(GameTileType.TowerPlace));
                break;
            case 4:
                _showTile = Instantiate(_tileFactory.Get(GameTileType.SpawnPoint));
                break;
            case 5:
                _showTile = Instantiate(_tileFactory.Get(GameTileType.Destination));
                break;
        }
    }
    public void SetGameBoard(GameBoard board)
    {
        _board = board;
    }

    public void HandelTouch()
    {
        GameTile tile = _board.GetTile(TouchRay);
    }

    public void StartGame()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneAt(1);
        SceneManager.MoveGameObjectToScene(_board.gameObject, scene);
        SceneManager.MoveGameObjectToScene(_menuSounds, scene);
        yield return null;
        SceneManager.UnloadSceneAsync(1);
    }
}
