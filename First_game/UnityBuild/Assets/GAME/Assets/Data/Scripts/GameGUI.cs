using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Game))]
public class GameGUI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _gameStatusNotice;

    private void OnEnable()
    {
        _game.Lose += OnGameLose;
    }

    private void OnDisable()
    {
        _game.Lose -= OnGameLose;
    }

    private void Update()
    {
        _money.text = "Money = " + _game.Money.ToString();
        _health.text = "Health = " + _game.Health.ToString();
    }

    private void OnGameLose()
    {
        _gameStatusNotice.text = "You lose!";
        _gameStatusNotice.gameObject.SetActive(true);

    }


}
