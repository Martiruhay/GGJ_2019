using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    [Header("Values")]
    public float bulletDamage, bombDamage;
    public float bulletStunDuration, bombStunDuration;
    public float aimerMinSpeed, aimerMaxSpeed;
    public float endGameTime;
    public int homeMaxHp;

    [Header("References")]
    public GameObject player1;
    public GameObject player2;

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
        AudioManager.instance.Stop("sad_music");

        //endGameObject.SetActive(false);
    }

    public void EndGame(int winer)
    {
        StartCoroutine(EndGameRoutine(winer));
    }

    private IEnumerator EndGameRoutine(int winer)
    {
        Debug.Log("Player " + winer + " wins!");

        PlayerPrefs.SetInt("winer", winer);

        player1.SetActive(false);
        player2.SetActive(false);

        yield return new WaitForSeconds(endGameTime);

        SceneManager.LoadSceneAsync("End");
    }
}
