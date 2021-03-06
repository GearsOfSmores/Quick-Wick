using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        transform.position = new Vector3(
           Mathf.Clamp(targetToFollow.position.x, -133f, 800f),
           Mathf.Clamp(targetToFollow.position.y, 0f, 20f),
           transform.position.z);
    }
}

