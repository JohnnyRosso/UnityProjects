using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu, _settingsMenu, _editorMenu;
    private GameObject _canvas;
    private bool _isGameMenu = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !_settingsMenu.activeSelf)
        {
            if (!_isGameMenu)
                OpenMenu();

            else
                CloseMenu();
        }

        //Costil
        if (GameObject.Find("Canvas") != null)
            _canvas = GameObject.Find("Canvas");
    }

    public void OpenMenu()
    {
        if (_canvas != null)
            _canvas.SetActive(false);

        if (_editorMenu != null)
            _editorMenu.SetActive(false);

        _gameMenu.SetActive(true);
        _isGameMenu = true;
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (_canvas != null)
            _canvas.SetActive(true);

        if (_editorMenu != null)
            _editorMenu.SetActive(true);

        _gameMenu.SetActive(false);
        _isGameMenu = false;
        Time.timeScale = 1f;
    }
}
