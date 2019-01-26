using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public Transform boi;
    public float rotationSpeed;

    private void Start()
    {
        AudioManager.instance.Stop("game_music");
        AudioManager.instance.Play("title_music");
    }

    private void Update()
    {
        boi.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
