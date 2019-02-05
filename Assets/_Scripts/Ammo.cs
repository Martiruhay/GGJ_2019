using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public SpriteRenderer[] bullets;
    public SpriteRenderer[] balloons;

    public float glowInt;
    public Color myColor, disableColor;

    public Color show, hide;

    private float alpha1 = 1.0f;
    private float alpha2 = 0.7f;

    public void SetAmmo(int nBullets, int nBalloons)
    {
        // Bullets
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < nBullets)
            {
                bullets[i].material.SetColor("_Color", myColor);
                bullets[i].material.SetFloat("_Glow2", glowInt);
                bullets[i].material.SetFloat("_Alpha", alpha1);
            }
            else{
                bullets[i].material.SetColor("_Color", disableColor);
                bullets[i].material.SetFloat("_Glow2", 1.0f);
                bullets[i].material.SetFloat("_Alpha", alpha2);
            }
        }

        // Balloons
        for (int i = 0; i < balloons.Length; i++)
        {
            if (i < nBalloons)
            {
                balloons[i].material.SetColor("_Color", myColor);
                balloons[i].material.SetFloat("_Glow2", glowInt);
                balloons[i].material.SetFloat("_Alpha", alpha1);
            }
            else{
                balloons[i].material.SetColor("_Color", disableColor);
                balloons[i].material.SetFloat("_Glow2", 1.0f);
                balloons[i].material.SetFloat("_Alpha", alpha2);
            }
        }
    }
}
