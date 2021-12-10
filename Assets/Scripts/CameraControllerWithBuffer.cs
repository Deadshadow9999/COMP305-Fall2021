using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerWithBuffer : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Range(1.0f, 10.0f)][SerializeField] private float cameraOffsetX = 5.0f;
    [Range(1.0f, 10.0f)][SerializeField] private float cameraOffsetY = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // Check the X threshold
        if(player.position.x < transform.position.x - (0.5f * cameraOffsetX)) // Left side
        {
            transform.position = new Vector3(
                player.position.x + (0.5f * cameraOffsetX),
                transform.position.y,
                transform.position.z);
        }
        else if (player.position.x > transform.position.x + (0.5f * cameraOffsetX)) // Right side
        {
            transform.position = new Vector3(
                player.position.x - (0.5f * cameraOffsetX),
                transform.position.y,
                transform.position.z);
        }

        // Check the Y threshold
        if(player.position.y < transform.position.y - (0.5f * cameraOffsetY)) // Top side
        {
            transform.position = new Vector3(
                transform.position.x,
                player.position.y + (0.5f * cameraOffsetY),
                transform.position.z);
        }
        else if (player.position.y > transform.position.y + (0.5f * cameraOffsetY)) // Top side
        {
            transform.position = new Vector3(
                transform.position.x,
                player.position.y - (0.5f * cameraOffsetY),
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(cameraOffsetX, cameraOffsetY, 0.0f));
    }
}
