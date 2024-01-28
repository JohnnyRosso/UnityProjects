using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    public static Action _onSave;

    private Storage _storage;
    private SettingsData _settingsData;

    public Slider _musicSlider, _audioEffectsSlider;
    [SerializeField] private DifficultManager _difficultManager;
    [SerializeField] private VideoManager _videoManagerRes;
    [SerializeField] private VideoManager _videoManagerQual;

    private void Start()
    {
        _storage = new Storage();
        Load();
    }

    public void Save()
    {
        _settingsData._music = _musicSlider.value;
        _settingsData._effects = _audioEffectsSlider.value;
        _settingsData._difficultyIndex = _difficultManager.CurrentDifficulty;
        _settingsData._resolutionIndex = _videoManagerRes.CurrentResolution;
        _settingsData._qualityIndex = _videoManagerQual.CurrentQuality; 

        _storage.Save(_settingsData);

        Debug.Log("Settings Saved!");
        Debug.Log("MusicValue = " + _settingsData._music);
        Debug.Log("EffctsValue = " + _settingsData._effects);
        Debug.Log("DifIndex = " + _settingsData._difficultyIndex);
        Debug.Log("ResIndex = " + _settingsData._resolutionIndex);
        Debug.Log("QualIndex = " + _settingsData._qualityIndex);

        _onSave?.Invoke();
    }

    public void Load()
    {
        _settingsData = (SettingsData)_storage.Load(new SettingsData());

        //Norm Costil
        if (_settingsData._resolutionIndex < 0)
            _settingsData._resolutionIndex = Screen.resolutions.Length - 1;

        _musicSlider.value = _settingsData._music;
        _audioEffectsSlider.value = _settingsData._effects;
        _difficultManager.CurrentDifficulty = _settingsData._difficultyIndex;
        _videoManagerRes.CurrentResolution = _settingsData._resolutionIndex;
        _videoManagerQual.CurrentQuality = _settingsData._qualityIndex;

        Debug.Log("Settings Loaded!");
        Debug.Log("MusicValue = " + _settingsData._music);
        Debug.Log("EffctsValue = " + _settingsData._effects);
        Debug.Log("DifIndex = " + _settingsData._difficultyIndex);
        Debug.Log("ResIndex = " + _settingsData._resolutionIndex);
        Debug.Log("QualIndex = " + _settingsData._qualityIndex);

        _onSave?.Invoke();
    }
}
