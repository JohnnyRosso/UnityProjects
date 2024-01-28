using UnityEngine;
using UnityEngine.UI;

public class CheckForChanges : MonoBehaviour
{
    [SerializeField] private GameObject _notice;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Button _closeSettingsButton;

    private bool _isChanged = false;
    private bool _isSaved = false;

    private void OnEnable()
    {
        VolumeManager._onVolumeChangedConsole += ConsoleMessage;
        DifficultManager._onDifficultChangedConsole += ConsoleMessage;
        VideoManager._onVideoChangedConsole += ConsoleMessage;

        SaveLoad._onSave += SettingsSaved;
        VolumeManager._onVolumeChanged += SettingsChanged;
        DifficultManager._onDifficultChanged += SettingsChanged;
        VideoManager._onVideoChanged += SettingsChanged;
    }

    private void OnDisable()
    {
        VolumeManager._onVolumeChangedConsole -= ConsoleMessage;
        DifficultManager._onDifficultChangedConsole -= ConsoleMessage;
        VideoManager._onVideoChangedConsole += ConsoleMessage;

        SaveLoad._onSave -= SettingsSaved;
        VolumeManager._onVolumeChanged -= SettingsChanged;
        DifficultManager._onDifficultChanged -= SettingsChanged;
        VideoManager._onVideoChanged -= SettingsChanged;
    }

    public void ConsoleMessage(string _changed)
    {
        if (_changed == "Music")
            Debug.Log("MusicChanged!");

        if (_changed == "Effects")
            Debug.Log("EffectsChanged!");

        if (_changed == "Difficult")
            Debug.Log("DifficultChanged!");

        if (_changed == "Resolution")
            Debug.Log($"{_changed}Changed");

        if (_changed == "Quality")
            Debug.Log($"{_changed}Changed");
    }

    public void SettingsSaved()
    {
        _isSaved = true;
    }

    public void SettingsChanged()
    {
        _isChanged = true;
        _isSaved = false;
    }

    public void SetNoticeTrue()
    {
        _notice.gameObject.SetActive(true);
        _isChanged = false;
        _isSaved = false;
    }

    public void SetMenuButtonsFalse()
    {
        _menuButtons.gameObject.SetActive(false);
    }

    public void SetSettingsMenuFalse()
    {
        _settingsMenu.gameObject.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        if (_isChanged && !_isSaved)
        {
            SetNoticeTrue();
            SetMenuButtonsFalse();
        }

        SetSettingsMenuFalse();
    }
}
