using JetBrains.Annotations;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioSource _seSource;

    [SerializeField] private List<SoundData> _seList;
    private Dictionary<string, AudioClip> _seDict;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _seDict = new Dictionary<string, AudioClip>();
        foreach (var se in _seList)
        {
            _seDict.Add(se.name, se.clip);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (_bgmSource.clip == clip) return;
        _bgmSource.clip = clip;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    // SEçƒê∂
    public void PlaySE(string name)
    {
        if (_seDict.TryGetValue(name, out var clip))
        {
            _seSource.PlayOneShot(clip);
        }
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PauseBGM()
    {
        _bgmSource.Pause();
    }

    public void UnPauseBGM()
    {
        _bgmSource.UnPause();
    }
}
