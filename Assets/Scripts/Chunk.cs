using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] float minRotSpeed, maxRotSpeed;
    float speed;
    Vector3 speedV;
    private void Awake()
    {
        if (Random.Range(0, 2) > 0)
            speed = Random.Range(minRotSpeed, maxRotSpeed);
        else
            speed = Random.Range(-maxRotSpeed, -minRotSpeed);
        speedV = new Vector3(0, speed, 0);
    }
    private void Update()
    {
        transform.Rotate(speedV*Time.deltaTime, Space.Self);
    }
}
