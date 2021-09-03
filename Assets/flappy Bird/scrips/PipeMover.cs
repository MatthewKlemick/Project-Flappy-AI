using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMover : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f), Space.World);
    }
}
