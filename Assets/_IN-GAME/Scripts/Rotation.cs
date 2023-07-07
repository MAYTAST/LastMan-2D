using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float angle = 45f;
    public Vector3 axis = Vector3.forward;

    private void Update()
    {
        // Get the center point of the parent GameObject
        Vector3 center = transform.position;

        transform.Rotate(Vector3.forward * Time.deltaTime);
    }
}
