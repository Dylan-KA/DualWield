using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RangeEnemy))]
public class RangeEditor : Editor
{
    private void OnSceneGUI()
    {
        BaseEnemy fov = (BaseEnemy)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.GetViewDistance());

        Vector3 viewAngle01 = DirectionFromFieldOfView(fov.transform.eulerAngles.y, -fov.GetFieldOfView() / 2);
        Vector3 viewAngle02 = DirectionFromFieldOfView(fov.transform.eulerAngles.y, fov.GetFieldOfView() / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.GetViewDistance());
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.GetViewDistance());

        if (fov.GetIsPlayerSeen())
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.GetPlayerTransform().position);
        }
    }

    private Vector3 DirectionFromFieldOfView(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
