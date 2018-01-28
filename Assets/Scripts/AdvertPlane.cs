using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertPlane : MonoBehaviour 
{
    public float speed;
    public Vector3 spawnOffsetFromCamera;
    private float startPosX;

    private void Start()
    {
        transform.localPosition = spawnOffsetFromCamera;
    }

    private void Update()
    {
        if (transform.localPosition.x > 450f)
            Destroy(gameObject);
        else
            transform.position += Vector3.right * Time.deltaTime * speed;
    }
}
