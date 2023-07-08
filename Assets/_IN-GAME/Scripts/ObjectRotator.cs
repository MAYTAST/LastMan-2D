using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectRotator : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public Transform centerObject;

    private void Update()
    {
        transform.RotateAround(centerObject.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    private void Start()
    {
        transform.DOScale(new Vector3(0.25f, 0.25f, 0.25f), 5f);
    }
}
