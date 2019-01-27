using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public float rotationSpeed;

    private void Start()
    {
        AudioManager.instance.Stop("game_music");
        AudioManager.instance.Stop("sad_music");
        AudioManager.instance.Play("title_music");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadSceneAsync("Gameplay");
    }
}
