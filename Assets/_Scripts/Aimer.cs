using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public float speed;
    public float minAngle, maxAngle;

    int d = 1;
    float t = 0;

    void Start()
    {
        t = minAngle;
    }

    void Update()
    {
        t += speed * Time.deltaTime * d;
        if (t > maxAngle || t < minAngle)
            d *= -1;
        t = Mathf.Clamp(t, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, t);
    }
}
