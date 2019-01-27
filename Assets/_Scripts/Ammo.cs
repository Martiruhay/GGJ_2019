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

    public void SetAmmo(int nBullets, int nBalloons)
    {
        // Bullets
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < nBullets)
            {
                bullets[i].material.SetColor("_Color", myColor);
                bullets[i].material.SetFloat("_Glow2", glowInt);
            }
            else{
                bullets[i].material.SetColor("_Color", disableColor);
                bullets[i].material.SetFloat("_Glow2", 1.0f);
            }
        }

        // Balloons
        for (int i = 0; i < balloons.Length; i++)
        {
            if (i < nBalloons)
            {
                balloons[i].material.SetColor("_Color", myColor);
                balloons[i].material.SetFloat("_Glow2", glowInt);
            }
            else{
                balloons[i].material.SetColor("_Color", disableColor);
                balloons[i].material.SetFloat("_Glow2", 1.0f);
            }
        }
    }
}
