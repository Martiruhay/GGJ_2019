using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aimer : MonoBehaviour
{
    public static Controller controller;

    public float speed;
    public float minAngle, maxAngle;

    public Transform playerArm;

    Transform point;

    int d = 1;
    float t = 0;
    public float triggers = 1;

    private void Awake()
    {
        point = transform.GetChild(0);
    }

    void Start()
    {
        t = Random.Range(minAngle, maxAngle);
    }

    void Update()
    {
        speed = Mathf.Lerp(controller.aimerMinSpeed, controller.aimerMaxSpeed, triggers);
        t += speed * Time.deltaTime * d;
        if (t > maxAngle || t < minAngle)
            d *= -1;
        t = Mathf.Clamp(t, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, t);

        // Handle arm
        playerArm.position = point.position;
    }
}
