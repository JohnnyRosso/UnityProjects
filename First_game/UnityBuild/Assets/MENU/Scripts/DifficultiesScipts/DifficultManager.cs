using TMPro;
using UnityEngine;
using System;

public class DifficultManager : MonoBehaviour
{
    public static Action<string> _onDifficultChangedConsole;
    public static Action _onDifficultChanged;

    private TextMeshProUGUI _difficultTXT;
    private string[] _difficultsTXT = { "Легко", "Средне", "Сложно" };
    private int _currentDifficulty;

    public int CurrentDifficulty
    {
        get
        {
            return _currentDifficulty;
        }

        set
        {
            if (value >= 0 && value <= 2)
            {
                _currentDifficulty = value;
            }
        }
    }

    private void OnEnable()
    {
        _difficultTXT = GetComponentInChildren<TextMeshProUGUI>();
        _difficultTXT.text = _difficultsTXT[_currentDifficulty];
    }

    public void IncreaseDifficult()
    {
        if (_currentDifficulty < 2)
        {
            _currentDifficulty++;
            _difficultTXT.text = _difficultsTXT[_currentDifficulty];

            _onDifficultChangedConsole?.Invoke("Difficult");
            _onDifficultChanged?.Invoke();
        }
    }

    public void DecreaseDifficult()
    {
        if (_currentDifficulty > 0)
        {
            _currentDifficulty--;
            _difficultTXT.text = _difficultsTXT[_currentDifficulty];

            _onDifficultChangedConsole?.Invoke("Difficult");
            _onDifficultChanged?.Invoke();
        }
    }
}
