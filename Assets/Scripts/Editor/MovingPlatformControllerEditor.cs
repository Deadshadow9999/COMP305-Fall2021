using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovingPlatformController))]

public class MovingPlatformControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MovingPlatformController controller = (MovingPlatformController)target;

        controller.waypointObject = (GameObject)EditorGUILayout.ObjectField("Waypoint object", controller.waypointObject, typeof(GameObject), false);
        controller.moveSpeed = EditorGUILayout.FloatField("Speed: ", controller.moveSpeed);

        EditorGUILayout.LabelField("Waypoints", EditorStyles.boldLabel);

        if(controller.waypoints != null && controller.waypoints.Count != 0)
        {
            for (int i = 0; i < controller.waypoints.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                controller.waypoints[i].gameObject.name = EditorGUILayout.TextField(controller.waypoints[i].gameObject.name);
                controller.waypoints[i].position = EditorGUILayout.Vector2Field("", controller.waypoints[i].position);
                if (GUILayout.Button("Delete"))
                {
                    // Remove specific waypoint
                    controller.RemoveWaypoint(i);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        if (GUILayout.Button("Add waypoint"))
        {
            // Add new waypoint to the list
            controller.AddNewWaypoint();
        }

        if (GUILayout.Button("Clear waypoints"))
        {
            // Clear waypoints from the list
            controller.ClearWaypoints();
        }

        //base.OnInspectorGUI();
    }
}
