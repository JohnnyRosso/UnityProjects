using System;
using TMPro;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
    public static Action<string> _onVideoChangedConsole;
    public static Action _onVideoChanged;

    private TextMeshProUGUI _resolutionTXT;
    private TextMeshProUGUI _qualityTXT;
    private string[] _qualitiesTXT = {"Низкое", "Среднее", "Высокое"};
    private int _currentResolutionIndex;
    private int _currentQualityIndex;
    private Resolution[] _resolutions;
    private Resolution _resolution;

    public int CurrentResolution
    {
        get
        { 
            return _currentResolutionIndex; 
        }

        set
        {
            if (value >= 0 && value <= Screen.resolutions.Length)
                _currentResolutionIndex = value;
        }
    }

    public int CurrentQuality
    {
        get
        {
            return _currentQualityIndex;
        }

        set
        {
            if (value >= 0 && value <= 2)
                _currentQualityIndex = value;
        }
    }

    private void OnEnable()
    {
        if (gameObject.name == "Quality")
        {
            _qualityTXT = GetComponentInChildren<TextMeshProUGUI>();
            _qualityTXT.text = _qualitiesTXT[_currentQualityIndex];
        }

        if (gameObject.name == "Resolution")
        {
            _resolutions = Screen.resolutions;
            _resolutionTXT = GetComponentInChildren<TextMeshProUGUI>();
            _resolutionTXT.text = _resolutions[_currentResolutionIndex].width + "x" + _resolutions[_currentResolutionIndex].height + " " + _resolutions[_currentResolutionIndex].refreshRate + "hz";
        }  
    }

    public void IncreaseResolutionIndex()
    {
        if (_currentResolutionIndex < _resolutions.Length - 1)
        {
            _currentResolutionIndex++;
            _resolutionTXT.text = _resolutions[_currentResolutionIndex].width + "x" + _resolutions[_currentResolutionIndex].height + " " + _resolutions[_currentResolutionIndex].refreshRate + "hz";
            _onVideoChangedConsole?.Invoke("Resolution");
            _onVideoChanged?.Invoke();
        }
    }

    public void DecreaseResolutionIndex()
    {
        if (_currentResolutionIndex > 0)
        {
            _currentResolutionIndex--;
            _resolutionTXT.text = _resolutions[_currentResolutionIndex].width + "x" + _resolutions[_currentResolutionIndex].height + " " + _resolutions[_currentResolutionIndex].refreshRate + "hz";
            _onVideoChangedConsole?.Invoke("Resolution");
            _onVideoChanged?.Invoke();
        }
    }

    public void SetResolution()
    {
        _resolutions = Screen.resolutions;
        _resolution = _resolutions[_currentResolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreenMode);
    }

    public void IncreaseQualityIndex()
    {
        if (_currentQualityIndex < 2)
        {
            _currentQualityIndex++;
            _qualityTXT.text = _qualitiesTXT[_currentQualityIndex];
        }
        _onVideoChangedConsole?.Invoke("Quality");
        _onVideoChanged?.Invoke();
    }

    public void DecreaseQualityIndex()
    {
        if (_currentQualityIndex > 0)
        {
            _currentQualityIndex--;
            _qualityTXT.text = _qualitiesTXT[_currentQualityIndex];
        }
        _onVideoChangedConsole?.Invoke("Quality");
        _onVideoChanged?.Invoke();
    }

    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(_currentQualityIndex);
    }

    public delegate void VideoSettingsChanged(int resolution, int quality);
}
