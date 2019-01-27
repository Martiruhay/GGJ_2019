using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Home : MonoBehaviour
{
    public static Controller controller;

    public int id;
    public float hp;

    public Image hpImage;

    private void Start()
    {
        hp = controller.homeMaxHp;
    }

    private void Update()
    {
        hpImage.fillAmount = hp / controller.homeMaxHp;
    }

    public void HitBullet()
    {
        hp -= controller.bulletDamage;

        if (hp <= 0)
            Die();
    }

    public void HitBomb()
    {
        hp -= controller.bombDamage;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player " + id + "loses");
        controller.EndGame(id == 1 ? 2 : 1);
    }


}
