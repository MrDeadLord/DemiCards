using DeadLords;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Card))]
public class CardsMod : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Card card = (Card)target;

        GUILayout.Space(10);

        using(new EditorGUI.DisabledGroupScope(!Application.isPlaying))
        if(GUILayout.Button(new GUIContent("Activate", "Активирует/играет карту")))
        {
            card.Active = true;
        }
    }
}
