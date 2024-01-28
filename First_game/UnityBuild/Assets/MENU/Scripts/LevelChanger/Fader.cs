using System;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string FADER_PATH = "Fader";
    private Action _fadedInCallBack;
    private Action _fadedOutCallBack;
    private static Fader _instance;
    public bool _isFading { get; private set; }

    public static Fader instance
    {
        get
        {
            if (_instance == null)
            {
                var prefab = Resources.Load<Fader>(FADER_PATH);
                _instance = Instantiate(prefab);
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void FadeIn(Action fadedInCallBack)
    {
        if (_isFading)
            return;

        _isFading = true;
        _fadedInCallBack = fadedInCallBack;
        _animator.SetBool("faded", true);
    }

    public void FadeOut(Action fadedOutCallBack)
    {
        if (_isFading)
            return;

        _isFading = true;
        _fadedOutCallBack = fadedOutCallBack;
        _animator.SetBool("faded", false);
    }

    private void Handle_FadeInAnimationOver()
    {
        _fadedInCallBack?.Invoke();
        _fadedInCallBack = null;
        _isFading = false;
    }

    private void Handle_FadeOutAnimationOver()
    {
        _fadedOutCallBack?.Invoke();
        _fadedOutCallBack = null;
        _isFading = false;
    }
}
