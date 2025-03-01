using UnityEditor;
using UnityEngine;

namespace SpeedJam
{
    [CustomEditor(typeof(GravitationalObject))]
    public class GravitationalFieldsGizmo : Editor
    {   
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        static void DrawGizmos(GravitationalObject obj, GizmoType gizmoType)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(obj.transform.position, obj.GravitationalField);
        }
    }
}