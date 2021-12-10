using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform moveThreshold;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x > moveThreshold.position.x)
        {
            // Camera follows the player
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(moveThreshold.position.x, moveThreshold.position.y + 10),
            new Vector2(moveThreshold.position.x, moveThreshold.position.y - 10)
            );
    }
}
