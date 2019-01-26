using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [Header("Values")]
    public float bulletDamage, bombDamage;
    public float bulletStunDuration, bombStunDuration;
    public float aimerSpeed;
    public float endGameTime;
    public int homeMaxHp;

    [Header("References")]
    public GameObject endGameObject;
    public GameObject player1, player2;
    public GameObject player1EndObject;
    public GameObject player2EndObject;

    private void Awake()
    {
        Player.controller = this;
        Home.controller = this;
        Aimer.controller = this;
        Home.controller = this;
    }

    private void Start()
    {
        AudioManager.instance.Stop("title_music");
        AudioManager.instance.Play("game_music");

        endGameObject.SetActive(false);
    }

    public void EndGame(int winer)
    {
        StartCoroutine(EndGameRoutine(winer));
    }

    private IEnumerator EndGameRoutine(int winer)
    {
        Debug.Log("Player " + winer + " wins!");

        player1.SetActive(false);
        player2.SetActive(false);

        endGameObject.SetActive(true);
        //if (winer == 1)
        //    player1EndObject.SetActive(true);
        //else
        //    player2EndObject.SetActive(true);
        yield return new WaitForSeconds(endGameTime);
        SceneManager.LoadSceneAsync("Title");
    }
}
