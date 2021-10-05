using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    private void Update()
    {
        transform.position = new Vector3(
           Mathf.Clamp(targetToFollow.position.x, 0f, 150f),
           Mathf.Clamp(targetToFollow.position.y, 0f, 10f),
           transform.position.z);
    }
}

