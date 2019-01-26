using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndController : MonoBehaviour
{
    public GameObject player1EndObject, player2EndObject;

    IEnumerator Start()
    {
        int winer = PlayerPrefs.GetInt("winer", 1);

        AudioManager.instance.Stop("game_music");
        AudioManager.instance.Play("sad_music");

        if (winer == 1)
        {
            player1EndObject.SetActive(true);
            player2EndObject.SetActive(false);
        }
        else
        {
            player1EndObject.SetActive(false);
            player2EndObject.SetActive(true);
        }

        yield return null;
    }
}
