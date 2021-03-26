using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet : Pooler
{
    [SerializeField] GameObject impactFXPrefab;
    [SerializeField] float impactLifetime = 3f, lightLifetime = 1f;
    [SerializeField] List<AudioClip> clips;

    Camera cam;
    const string IMPACT_KEY = "Impact FX";
    void Start()
    {
        FillPool(IMPACT_KEY, impactFXPrefab, 10);
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 20f))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    //Debug.Log(hit.collider.gameObject.name + " " + hit.point);
                    StartCoroutine(ShowImpactVFXAt(hit.point+Vector3.up*0.01f));
                    PlayHitSoundAt(hit.point);
                }
            }
        }
    }

    void PlayHitSoundAt(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clips[Random.Range(0, 3)], pos);
    }

    IEnumerator ShowImpactVFXAt(Vector3 pos)
    {
        if (PoolCount(IMPACT_KEY) >= 0)
        {
            GameObject impact = InstantiateFromPool(IMPACT_KEY, pos, Quaternion.identity);
            var ps = impact.GetComponent<ParticleSystem>();
            ps.Play();
            yield return new WaitForSeconds(lightLifetime);
            impact.GetComponentInChildren<Light>().enabled = false;
            yield return new WaitForSeconds(impactLifetime - lightLifetime);
            ReturnToPool(IMPACT_KEY, impact);
        }
    }
}
