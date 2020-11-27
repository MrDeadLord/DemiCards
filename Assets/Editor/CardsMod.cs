using DeadLords.Controllers;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardActivator))]
public class CardsMod : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardActivator card = (CardActivator)target;

        GUILayout.Space(10);

        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
            if (GUILayout.Button(new GUIContent("Activate", "Активирует/играет карту")))
            {
                card.On();
            }
    }
}
