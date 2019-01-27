using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakestChild : MonoBehaviour
{
    public Transform referenceRot, referencePos;
    public Vector3 offset;

    void Update()
    {
        transform.rotation = referenceRot.rotation;
        transform.position = referenceRot.position;
    }
    void FixedUpdate()
    {
        transform.rotation = referenceRot.rotation;
        transform.position = referenceRot.position;
    }
}
