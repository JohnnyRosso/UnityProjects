using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class VolumeManager : MonoBehaviour
{
    public static Action<string> _onVolumeChangedConsole;
    public static Action _onVolumeChanged;

    public Slider _musicSlider, _audioEffectsSlider;
    public AudioSource[] _musicAudio, _audioEffectsAudio;
    private TextMeshProUGUI _musicSliderValueTXT;
    private TextMeshProUGUI _audioEffectsSliderValueTXT;

    private void Awake()
    {
        _musicSliderValueTXT = _musicSlider.GetComponentInChildren<TextMeshProUGUI>();
        _audioEffectsSliderValueTXT = _audioEffectsSlider.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void MusicUpdate()
    {
        for (int i = 0; i < _musicAudio.Length; i++)
        {
            _musicAudio[i].volume = _musicSlider.value;
            _onVolumeChangedConsole?.Invoke("Music");
            _onVolumeChanged?.Invoke();
        }
        
        _musicSliderValueTXT.text = ((int)(_musicSlider.value*100)).ToString() + "%";
    }
    
    public void EffectsUpdate()
    {
        for (int i = 0; i < _audioEffectsAudio.Length; i++)
        {
            _audioEffectsAudio[i].volume = _audioEffectsSlider.value;
            _onVolumeChangedConsole?.Invoke("Effects");
            _onVolumeChanged?.Invoke();
        }

        _audioEffectsSliderValueTXT.text = ((int)(_audioEffectsSlider.value*100)).ToString() + "%";
    }
}
