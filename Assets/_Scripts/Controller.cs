using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float bulletDamage, bombDamage;
    public float bulletStunDuration, bombStunDuration;
    public float aimerSpeed;

    private void Awake()
    {
        Player.controller = this;
        Home.controller = this;
        Aimer.controller = this;
    }

    private void Start()
    {
        AudioManager.instance.Play("game_music");
    }

    public void EndGame(int winer)
    {
        Debug.Log("Player " + winer + " wins!");
    }
}
