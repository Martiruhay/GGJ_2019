using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float speedMin, speedMax;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedMin, speedMax);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-speed * Time.deltaTime, 0f, 0f);
        if (transform.position.x < -52f)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0f);
        }
    }
}
