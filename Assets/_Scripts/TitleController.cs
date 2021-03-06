﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{

    private void Start()
    {
        AudioManager.instance.Stop("game_music");
        AudioManager.instance.Stop("sad_music");
        AudioManager.instance.Play("title_music");

        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Exit"))
            Application.Quit();
        else if (Input.anyKeyDown)
            SceneManager.LoadSceneAsync("Gameplay");
    }
}
