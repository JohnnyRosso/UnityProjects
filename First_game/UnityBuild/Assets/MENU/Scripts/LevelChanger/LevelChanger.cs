using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;
    //private const string SCENE_0 = "MainMenu";
    //private const string SCENE_1 = "Game1";
    private bool _isLoading;
    private static LevelChanger _instance;

    //private void Awake()
    //{
    //    if (_instance != null)
    //    {
    //        Destroy(gameObject);
    //        return;
    //    }

    //    _instance = this;
    //    DontDestroyOnLoad(gameObject);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha0))
    //        LoadLevel(SCENE_0);

    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //        LoadLevel(SCENE_1);
    //}



    public void LoadLevel()
    {
        if (_isLoading)
            return;

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == _sceneToLoad)
            throw new Exception("Ne nado tak!");

        StartCoroutine(LoadSceneCoroutine(_sceneToLoad));
    }

    private IEnumerator LoadSceneCoroutine(int _sceneToLoad)
    {
        Time.timeScale = 1f;
        _isLoading = true;

        var _waitFading = true;
        Fader.instance.FadeIn(() => _waitFading = false);

        while (_waitFading)
            yield return null;

        ChangeScene();

        _waitFading = true;
        Fader.instance.FadeOut(() => _waitFading = false);

        while (_waitFading)
            yield return null;
            

        _isLoading = false;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
