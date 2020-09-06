using UnityEngine;
using UnityEditor;
using DeadLords;
using System.Linq;

[CustomEditor(typeof(Card))]
public class CardsMod : Editor
{
    string[] costTypes = { "action", "mana" };
    int costIndex;
    string[] spellTypes = { "Creature", "Spell" };
    int spellIndex;
    string bonusName = string.Empty;
    bool isTexting;

    #region Путь 2. Сохраняем карты через редактор

    string savePath = Application.dataPath + "/SaveData/Cards.txt";

    #endregion

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Card cards = (Card)target;

        if(GUILayout.Button(new GUIContent("Save", "Сохраняет вариацию карты в файл")))
        {

        }

        #region Использовать карту через редактор
        EditorGUILayout.Space(10);

        isTexting = EditorGUILayout.BeginToggleGroup(new GUIContent("Играть в редакторе",
            "Работает только в редакторе И во время игры"), isTexting);

        #region Настойка стоимости карты
        GUILayout.BeginHorizontal();

        cards.Cost = EditorGUILayout.IntField("Стоимость карты", cards.Cost);

        costIndex = EditorGUILayout.Popup(costIndex, costTypes);
        if (costIndex == 0)
            cards.IsManaSpell = false;
        else
            cards.IsManaSpell = true;

        GUILayout.EndHorizontal();
        #endregion

        #region Выбор типа карты
        GUILayout.BeginHorizontal();

        GUILayout.Label(new GUIContent("Тип карты", "Призыв или заклинание"));

        spellIndex = GUILayout.SelectionGrid(spellIndex, spellTypes, spellTypes.Count());


        GUILayout.EndHorizontal();

        if (spellIndex == 0)
        {
            cards.CardsCreature = EditorGUILayout.ObjectField(cards.CardsCreature, typeof(GameObject), true) as GameObject;
            cards.IsCreature = true;
        }
        else
        {
            cards.IsCreature = false;
            cards.CardsBonus.unikName = EditorGUILayout.TextField("Название заклинания", bonusName);
        }

        #endregion

        using (new EditorGUI.DisabledGroupScope(!Application.isPlaying))
        {
            if (GUILayout.Button(new GUIContent("Play card", "Можно активировать лишь когда игра запущена")))
                cards.Active = true;
        }

        EditorGUILayout.EndToggleGroup();
        #endregion
    }
}