using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private void Awake()
    {
        Player.controller = this;
    }
}
