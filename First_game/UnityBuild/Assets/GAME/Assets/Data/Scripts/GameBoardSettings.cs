using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBoardSettings : MonoBehaviour
{
    [SerializeField] private GameBoard _boardPrefab;
    [SerializeField] private Vector2Int _minBoardSize = new Vector2Int(2,2);
    [SerializeField] private Vector2Int _maxBoardSize = new Vector2Int(12, 12);

    [SerializeField] private GameTileFactory _tileFactory;


    [SerializeField] private Slider _sliderSizeX;
    [SerializeField] private Slider _sliderSizeY;


    [SerializeField] private TextMeshProUGUI _textSizeX;
    [SerializeField] private TextMeshProUGUI _textSizeY;


    private Vector2Int _boardSize;
    private GameBoard _board;
    private EditorGame _editorGame;

    private void Awake()
    {
        if (_tileFactory == null)
        {
            _tileFactory = Resources.Load<GameTileFactory>("Resources/Setup/TileFactory");
        }
        Initialization();
    }

    private void SyncBaord()
    {
        _board = FindObjectOfType<GameBoard>();
        if (_board != null)
        {
            _boardSize = _board.Size;
            _sliderSizeX.value = _boardSize.x;
            _sliderSizeY.value = _boardSize.y;
        }
    }

    public void CreateBoard()
    {
        if (_board != null)
        {
            Destroy(_board.gameObject);
            _board = Instantiate(_boardPrefab);
            _board.name = "Game Board";
            _board.Initialize(_boardSize, _tileFactory);
            _editorGame.SetGameBoard(_board);

        }
        else
        {
            _board = Instantiate(_boardPrefab);
            _board.name = "Game Board";
            _board.Initialize(_boardSize, _tileFactory);
            _editorGame.SetGameBoard(_board);
        }
    }

    private void Initialization()
    {
        SyncBaord();
        if (_editorGame == null)
        {
            _editorGame = FindObjectOfType<EditorGame>();
        }

        _sliderSizeX.minValue = _minBoardSize.x;
        _sliderSizeY.minValue = _minBoardSize.y;
        _sliderSizeX.maxValue = _maxBoardSize.x;
        _sliderSizeY.maxValue = _maxBoardSize.y;

        if (_board == null)
        {
            _sliderSizeX.value = _sliderSizeX.minValue;
            _sliderSizeY.value = _sliderSizeY.minValue;
            _boardSize.x = (int)_sliderSizeX.minValue;
            _boardSize.y = (int)_sliderSizeY.minValue;
            CreateBoard();
        }

        _textSizeX.text = "X = " + _boardSize.x.ToString();
        _textSizeY.text = "Y = " + _boardSize.y.ToString();
    }

    public void UpdateBoardSizeX()
    {
        _boardSize.x = (int)_sliderSizeX.value;
        _textSizeX.text = "X = " + _boardSize.x.ToString();
    }

    public void UpdateBoardSizeY()
    {
        _boardSize.y = (int)_sliderSizeY.value;
        _textSizeY.text = "Y = " + _boardSize.y.ToString();
    }
}
