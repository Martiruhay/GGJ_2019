using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float bulletDamage, bombDamage;

    private void Awake()
    {
        Player.controller = this;
        Home.controller = this;
    }

    public void EndGame(int winer)
    {
        Debug.Log("Player " + winer + " wins!");
    }
}
