using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrillScrew : MonoBehaviour
{
    [SerializeField] bool isDrilling = false;
    [SerializeField] ParticleSystem ps;
    [SerializeField] AudioSource _AS;
    [SerializeField] AudioClip drillClip;

    private void Start()
    {
        _AS.clip = drillClip;
        _AS.loop = true;
        StartDrilling();
    }

    void StartDrilling()
    {
        isDrilling = true;
        ps.Play();
        _AS.Play();
        _AS.DOFade(1f, 1f).From(0f);
    }

    void StopDrilling()
    {
        isDrilling = false;
        ps.Stop();
        _AS.Stop();
        _AS.DOFade(0f, 1f).From(1f);
    }
}
