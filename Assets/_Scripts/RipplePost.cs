using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RipplePost : MonoBehaviour
{
    public Material RippleMaterial;
    public float MaxAmount = 50f;

    [Range(0, 1)]
    public float Friction = .9f;

    private float Amount = 0f;
    private Camera cam;

    public static RipplePost instance;

    private void Start()
    {
        instance = this;
        cam = GetComponent<Camera>();
    }

    public void SetPoint(Vector3 pos)
    {
        pos = cam.WorldToScreenPoint(pos);
        this.Amount = this.MaxAmount;
        this.RippleMaterial.SetFloat("_CenterX", pos.x);
        this.RippleMaterial.SetFloat("_CenterY", pos.y);
    }

    void Update()
    {
        this.RippleMaterial.SetFloat("_Amount", this.Amount);
        this.Amount *= this.Friction;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, this.RippleMaterial);
    }
}
