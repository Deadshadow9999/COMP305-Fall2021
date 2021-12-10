using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public GameObject waypointObject;
    public float moveSpeed;
    public List<Transform> waypoints;

    private int currentTargetIndex = 0;

    private void Awake()
    {
        waypoints = new List<Transform>();
        foreach (Transform t in transform.parent.GetChild(1))
        {
            waypoints.Add(t);
        }

        if (waypoints.Count > 0)
        {
            transform.position = waypoints[0].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count > 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentTargetIndex].position, Time.deltaTime * moveSpeed);

            if (Vector2.Distance(transform.position, waypoints[currentTargetIndex].position) < 0.01f)
            {
                // Close enough to change targer
                currentTargetIndex = (currentTargetIndex + 1) % waypoints.Count;
            }
        }
    }

    public void AddNewWaypoint()
    {
        GameObject gameObject = Instantiate(waypointObject, Vector2.zero, Quaternion.identity);
        gameObject.transform.SetParent(transform.parent.GetChild(1));
        gameObject.name = "Waypoint" + waypoints.Count;
        waypoints.Add(gameObject.transform);
    }

    public void RemoveWaypoint(int index)
    {
        waypoints.RemoveAt(index);
        //waypoints.TrimExcess();
        DestroyImmediate(transform.parent.GetChild(1).GetChild(index).gameObject);
    }

    public void ClearWaypoints()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            DestroyImmediate(waypoints[i].gameObject);
        }

        waypoints.Clear();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
