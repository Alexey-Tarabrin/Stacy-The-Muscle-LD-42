using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
[ExecuteInEditMode]
public class CustomAudioSource : MonoBehaviour
{
    public AudioClip[] _audioClips;
    private AudioSource _audioSource;
    [Range(0f, 1f)] public float Volume = 1;
    [Range(.1f, 3f)] public float Pitch = 1;
    public bool Loop;

    private void Start()
    {
        if (_audioSource == null)
            _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
    }


    public void PlayRandom()
    {
        _audioSource.volume = Volume;
        _audioSource.pitch = Pitch;
        _audioSource.loop = Loop;
        _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
        _audioSource.Play();
    }
}