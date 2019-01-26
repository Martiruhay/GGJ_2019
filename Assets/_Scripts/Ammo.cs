using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public SpriteRenderer[] bullets;
    public SpriteRenderer[] balloons;

    public Color show, hide;

    public void SetAmmo(int nBullets, int nBalloons)
    {
        // Bullets
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < nBullets)
                bullets[i].color = show;
            else
                bullets[i].color = hide;
        }

        // Balloons
        for (int i = 0; i < balloons.Length; i++)
        {
            if (i < nBalloons)
                balloons[i].color = show;
            else
                balloons[i].color = hide;
        }
    }
}
