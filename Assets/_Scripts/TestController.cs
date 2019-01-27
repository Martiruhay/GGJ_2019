using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public Slider s;

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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("Slow_P1: " + Input.GetAxis("Slow_P1"));
            //Debug.Log("Fast_P1: " + Input.GetAxis("Fast_P1"));
        }
        s.value = Input.GetAxis("Triggers_P1");

        // Player 2
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log("Horizontal_P2: " + Input.GetAxis("Horizontal_P2"));
        if (Input.GetButtonDown("Jump_P2"))
            Debug.Log("Jump_P2");
        if (Input.GetButtonDown("Fire1_P2"))
            Debug.Log("Fire1_P2");
        if (Input.GetButtonDown("Fire2_P2"))
            Debug.Log("Fire2_P2");
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //    Debug.Log("Slow_P2: " + Input.GetAxis("Slow_P2"));
        //if (Input.GetKeyDown(KeyCode.RightArrow))
        //    Debug.Log("Fast_P2: " + Input.GetAxis("Fast_P2"));
    }
}
