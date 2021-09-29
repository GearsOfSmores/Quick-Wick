using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    private void Update()
    {
        transform.position = new Vector3(
           Mathf.Clamp(targetToFollow.position.x, 0f, 40f),
           Mathf.Clamp(targetToFollow.position.y, 0f, 0f),
           transform.position.z);
    }
}

