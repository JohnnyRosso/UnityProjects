using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisabledThemes : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audio;

    private void Awake()
    {
        for (int i = 0; i < _audio.Length; i++)
            _audio[i].gameObject.SetActive(false);
    }
}
