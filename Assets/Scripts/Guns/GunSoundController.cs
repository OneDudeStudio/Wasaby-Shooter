using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSoundController : MonoBehaviour
{
    private bool _shootAudioClipChecker = true;
    [SerializeField] private AudioClip clip1;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private AudioSource _source;

    [SerializeField] private AudioClip _outOfAmmoSound;

    public void PlayShootSound()
    {
        if (_shootAudioClipChecker)
        {
            _source.PlayOneShot(clip1);
        }

        else
        {
            _source.PlayOneShot(clip2);
        }

        _shootAudioClipChecker = !_shootAudioClipChecker;
    }

    public void PlayOutOfAmmoSound()
    {
        _source.PlayOneShot(_outOfAmmoSound);
    }
}
