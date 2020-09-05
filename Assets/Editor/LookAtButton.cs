using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LookAtButton : Editor
{
    [MenuItem("DeadLords/Look At last selected object %l", false, 2)]
    public static void Looker()
    {
        Transform[] selectedObjs = Selection.transforms;

        var list = new List<Transform>();

        foreach (Transform t in selectedObjs)
            list.Add(t);

        foreach(Transform tl in list)
        {
            tl.LookAt(Selection.activeTransform);
            if (tl == Selection.activeTransform)
                continue;
        }
    }

    [MenuItem("DeadLords/Look At last selected object %l", true)]
    public static bool Validation()
    {
        return Selection.transforms.Length >= 2;
    }
}
