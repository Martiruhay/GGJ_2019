using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Player 1
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Horizontal_P1: " + Input.GetAxis("Horizontal_P1"));
        if (Input.GetButtonDown("Jump_P1"))
            Debug.Log("Jump_P1");
        if (Input.GetButtonDown("Fire1_P1"))
            Debug.Log("Fire1_P1");
        if (Input.GetButtonDown("Fire2_P1"))
            Debug.Log("Fire2_P1");

        // Player 2
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Horizontal_P2: " + Input.GetAxis("Horizontal_P2"));
        if (Input.GetButtonDown("Jump_P2"))
            Debug.Log("Jump_P2");
        if (Input.GetButtonDown("Fire1_P2"))
            Debug.Log("Fire1_P2");
        if (Input.GetButtonDown("Fire2_P2"))
            Debug.Log("Fire2_P2");
    }
}
