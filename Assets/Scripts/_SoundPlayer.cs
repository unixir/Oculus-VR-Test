using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class _SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _repeatAS, _singleShotAS;
    [SerializeField] List<AudioClip> _clips;
    [SerializeField] List<_ClipList> _clipLists;

    public List<AudioClip> Clips { get => _clips; set => _clips = value; }
    public List<_ClipList> ClipLists { get => _clipLists; set => _clipLists = value; }

    public void PlaySound(AudioClip clip, bool repeat = false)
    {
        if (repeat)
        {
            _repeatAS.clip = clip;
            _repeatAS.loop = true;
            _repeatAS.Play();
        }
        else if (_singleShotAS != null)
        {
            _singleShotAS.PlayOneShot(clip);
        }
        else
            AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public void PlayRandomSound(_ClipList clipList, bool repeat = false)
    {
        PlaySound(clipList._clips[Random.Range(0, clipList._clips.Count)], repeat);
    }

    public void StopPlayingAll()
    {
        if (_repeatAS != null) _repeatAS.Stop();
        if (_singleShotAS != null) _singleShotAS.Stop();
    }

    public void StopPlaying(AudioClip clip)
    {
        if (_repeatAS != null && _repeatAS.clip == clip) _repeatAS.Stop();
        if (_singleShotAS != null && _singleShotAS.clip == clip) _singleShotAS.Stop();
    }
}
[System.Serializable]
public struct _ClipList
{
    public List<AudioClip> _clips;
}

