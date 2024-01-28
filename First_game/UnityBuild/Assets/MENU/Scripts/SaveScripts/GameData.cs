using System;

[Serializable]
public class SettingsData
{
    public float _music;
    public float _effects;
    public int _difficultyIndex;
    public int _resolutionIndex;
    public int _qualityIndex;

    public SettingsData()
    {
        _music = 1f;
        _effects = 1f;
        _difficultyIndex = 0;
        _resolutionIndex = -1;
        _qualityIndex = 2;
    }
}