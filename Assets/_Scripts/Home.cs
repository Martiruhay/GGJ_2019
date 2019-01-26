using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public static Controller controller;

    public int id;
    public float hp;

    private void Start()
    {
        hp = controller.homeMaxHp;
    }

    public void HitBullet()
    {
        hp -= controller.bulletDamage;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player " + id + "loses");
        controller.EndGame(id == 1 ? 2 : 1);
    }


}
