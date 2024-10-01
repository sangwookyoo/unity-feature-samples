using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum Sound
{
    BGM,
    Effect,
    MaxCount,
}

public class SoundManager
{
    private const string PATH_SOUND_BGM = "Sounds/BGM";
    private const string PATH_SOUND_EFFECT = "Sounds/Effect";
    
    private readonly AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    private readonly Dictionary<string, AudioClip> _audioClips = new();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound_Root");

        if (root == null)
        {
            root = new GameObject("@Sound_Root");
            Object.DontDestroyOnLoad(root);
            
            for (Sound i = 0; i < Sound.MaxCount; i++)
            {
                GameObject go = new GameObject(i.ToString());
                _audioSources[(int)i] = go.AddComponent<AudioSource>();
                go.transform.SetParent(root.transform);
            }
            
            _audioSources[(int)Sound.BGM].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource source in _audioSources)
        {
            if (source != null)
            {
                source.clip = null;
                source.Stop();
            }
        }

        _audioClips.Clear();
    }
    
    public void PlayBGM(string path, float pitch = 1.0f)
    {
        AudioClip clip = GetOrAddAudioClip(Path.Combine(PATH_SOUND_BGM, path));
        if (clip == null) return;
            
        AudioSource source = _audioSources[(int)Sound.BGM];
        if (source.isPlaying) source.Stop();
        source.pitch = pitch;
        source.clip = clip;
        source.Play();
    }

    public void PlayEffect(string path, float pitch = 1.0f)
    {
        AudioClip clip = GetOrAddAudioClip(Path.Combine(PATH_SOUND_EFFECT, path));
        if (clip == null) return;
        
        AudioSource source = _audioSources[(int)Sound.Effect];
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }

    private AudioClip GetOrAddAudioClip(string path)
    {
        if (_audioClips.TryGetValue(path, out AudioClip clip))
            return clip;
        
        clip = Resources.Load<AudioClip>(path);
        
        if (clip != null)
            _audioClips.Add(path, clip);
        else
            Debug.LogWarning($"[Sound]: AudioClip Missing: @{path}");
        
        return clip;
    }
}