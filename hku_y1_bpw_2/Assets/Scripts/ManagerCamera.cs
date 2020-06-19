using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCamera : MonoBehaviour
{
    [Header("Camera")]
    // Smoothness of camera
    public float cameraSmoothSpeed = 0.125f;
    /// <summary>
    /// The offset from the target
    /// </summary>
    public Vector3 cameraOffset; 
    /// <summary>
    /// Used for calculation
    /// </summary>
    private Vector3 velocity = Vector3.zero;
    /// <summary>
    /// The transform of the target (player in this case)
    /// </summary>
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        // Get component
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowTarget();    
    }

    /// <summary>
    /// Follow the target with smoothness
    /// </summary>
    private void FollowTarget()
    {
        Vector3 desiredPos = player.transform.position + cameraOffset;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, cameraSmoothSpeed);
        transform.position = new Vector3(smoothPos.x, smoothPos.y, -1);
    }
}
