using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameBoard _board;

    [SerializeField] private Camera _camera;

    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private TowerFactory _towerFactory;

    [SerializeField] private float _money = 100f;
    public float Money => _money;
    [SerializeField] private float _baseHealth = 100f;
    public float Health => _baseHealth;

    [SerializeField, Range(0.1f, 10f)]
    private float _spawnSpeed;
    private float _spawnProgress;

    private EnemyCollection _enemies = new EnemyCollection();

    private Ray TouchRay => _camera.ScreenPointToRay(Input.mousePosition);
    private TowerType _towerType = TowerType.Default;

    public event Action Lose;


    private void Awake()
    {
        if (_board == null)
        {
            GameBoard board = FindObjectOfType<GameBoard>();
            if (board != null)
            {
                _board = board;
                _board.name = "From Game Scene BOARD!!!!";
                _board.GrowthPath();
            }
        }
    }

    private void OnEnable()
    {
        _enemies.EnemyDead += OnEnemyDead;
        _enemies.EnemyAchivedBase += OnAchievedBase;
    }

    private void OnDisable()
    {
        _enemies.EnemyDead -= OnEnemyDead;
        _enemies.EnemyAchivedBase -= OnAchievedBase;
    }

    private void Start()
    {
        if (_board != null)
        {
            _board.GrowthPath();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _towerType = TowerType.Default;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _towerType = TowerType.Freeze;
        }

        if (Input.GetMouseButtonDown(0))
        {
            HandelTouch();
        }

        _spawnProgress += _spawnSpeed * Time.deltaTime;
        while(_spawnProgress >= 1f)
        {
            _spawnProgress -= 1f;
            SpawnEnemy();
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    _enemies.Clear();
        //    StartCoroutine(ChangeScene());
        //}

        _enemies.GameUpdate();

    }

    //В песочницy
    public void ToRedactor()
    {
        _enemies.Clear();
        StartCoroutine(ChangeScene());
    }

    public void HandelTouch()
    {
        GameTile tile = _board.GetTile(TouchRay);
        if (tile != null && tile.Type == GameTileType.TowerPlace)
        {
            TowerPlaceTile towerTile = tile.GetComponent<TowerPlaceTile>();
            float towerCost = _towerFactory.GetCost(_towerType);
            if (!towerTile.isTowerPlaced && _money >= towerCost)
            {
                Tower tower;
                switch (_towerType)
                {
                    case TowerType.Default:
                        tower = _towerFactory.GetDefault();
                        break;
                    case TowerType.Freeze:
                        tower = _towerFactory.GetFreeze();
                        break;
                    default:
                        tower = _towerFactory.GetDefault();
                        break;
                }
                 _board.PlaceTower(towerTile, tower);
                 _money -= towerCost;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (_board.SpawnPointCount > 0)
        {
            SpawnPointTile spawnPoint = _board.GetSpawnPoint(UnityEngine.Random.Range(0, _board.SpawnPointCount));
            Enemy enemy = _enemyFactory.Get((EnemyType)UnityEngine.Random.Range(0,3));
            enemy.SpawnOn(spawnPoint);
            enemy.SetWayPoints(spawnPoint.WayPoints);
            enemy.SetDistanceToDestionation(spawnPoint.DistanceToDestination);
            _enemies.Add(enemy);
            
        }
    }

    public void SetDefaultTower()
    {
        _towerType = TowerType.Default;
    }
    public void SetFreezeTower()
    {
        _towerType = TowerType.Freeze;
    }

    private void OnEnemyDead(float reward)
    {
        _money += reward;
    }

    private void OnAchievedBase(float damage)
    {
        _baseHealth -= damage;
        if (_baseHealth < 0)
        {
            Lose?.Invoke();
        }
    }

    IEnumerator ChangeScene()
    {
        SceneManager.LoadScene("EditorGame", LoadSceneMode.Additive);
        Scene EditorScene = SceneManager.GetSceneByName("EditorGame");
        Scene GameScene = SceneManager.GetSceneByName("Game");
        Scene EnemyScene = SceneManager.GetSceneByName("EnemyFactory");
        Scene TowerScene = SceneManager.GetSceneByName("TowerFactory");
        SceneManager.MoveGameObjectToScene(_board.gameObject, EditorScene);
        yield return null;
        SceneManager.SetActiveScene(EditorScene);
        SceneManager.UnloadSceneAsync(GameScene);
        SceneManager.UnloadSceneAsync(TowerScene);
        SceneManager.UnloadSceneAsync(EnemyScene);
        yield return null;
    }


}
