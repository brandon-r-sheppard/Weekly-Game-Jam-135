using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (AIFieldOfView))]
public class FieldOfViewEditor : Editor
{

    private void OnSceneGUI()
    {
        AIFieldOfView fov = (AIFieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.ViewRadius);
        Vector3 viewAngleA = fov.DirFromAngle(-fov.ViewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov.ViewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRadius);

    }

}
