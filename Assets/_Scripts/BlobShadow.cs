using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobShadow : MonoBehaviour
{
    private float yIni;
    public float maxAlpha, maxH;
    public Transform follow;
    private SpriteRenderer sr;
    Vector3 iniS;

    void Start()
    {
        yIni = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        iniS = transform.localScale;
    }

    
    void Update()
    {
        transform.position = new Vector3(follow.position.x, yIni, 0.0f);
        float dif = follow.position.y - yIni;
        sr.color = new Color(0f, 0f, 0f, Mathf.Clamp(1 - (dif /maxH), 0f, 0.3f));
        transform.localScale = iniS * Mathf.Clamp(1 - (dif / maxH), 0.6f, 1f) * 1.5f;
    }
}