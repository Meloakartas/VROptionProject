using UnityEngine;
using UnityEditor;

public static class DebugMenu
{
#if UNITY_EDITOR
    [MenuItem("Debug/Print Global Position")]
    public static void PrintGlobalPosition()
    {
        if (Selection.activeGameObject != null)
        {
            Debug.Log(Selection.activeGameObject.name + " is at " + Selection.activeGameObject.transform.position);
        }
    }
#endif
}