using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndController : MonoBehaviour
{
    public GameObject player1EndObject, player2EndObject;
    public SpriteRenderer congratulationsBG;

    public GameObject[] texts;

    public string nextScene;

    private void Update()
    {
        if (Input.anyKeyDown && Time.timeSinceLevelLoad > 2f)
        {
            SceneManager.LoadSceneAsync(nextScene);
        }
    }
    IEnumerator Start()
    {
        int winer = PlayerPrefs.GetInt("winer", 1);

        AudioManager.instance.Stop("game_music");
        AudioManager.instance.Play("sad_music");

        // Activate winner
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

        yield return new WaitForSeconds(2);

        float t1 = 0, t2 = 4;
        while (t1 < t2)
        {
            t1 += Time.deltaTime;
            Color c = congratulationsBG.color;
            c.a = Mathf.Lerp(1, .3f, t1 / t2);
            congratulationsBG.color = c;
            yield return null;
        }

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].SetActive(true);
            TextMeshProUGUI t = texts[i].GetComponent<TextMeshProUGUI>();

            t1 = 0; t2 = 2;
            while (t1 < t2)
            {
                t1 += Time.deltaTime;
                Color c = t.color;
                c.a = Mathf.Lerp(0, 1, t1 / t2);
                t.color = c;
                yield return null;
            }

            yield return new WaitForSeconds(2);

            t1 = 0; t2 = 2;
            while (t1 < t2)
            {
                t1 += Time.deltaTime;
                Color c = t.color;
                c.a = Mathf.Lerp(1, 0, t1 / t2);
                t.color = c;
                yield return null;
            }


            texts[i].SetActive(false);
        }

        SceneManager.LoadSceneAsync(nextScene);
    }
}
