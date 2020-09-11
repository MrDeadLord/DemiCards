using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StarterPackCreator : EditorWindow
{
    #region Переменные
    /// <summary>
    /// Путь сохранения пака, т.е. player's deck
    /// </summary>
    string path;
    /// <summary>
    /// Финальный список карт для сохранения
    /// </summary>
    List<CardData> completeDeck = new List<CardData>();

    /// <summary>
    /// Выбранная карта
    /// </summary>
    int selectedCard;
    /// <summary>
    /// Полный список всех карт в игре
    /// </summary>
    List<CardData> allCards = new List<CardData>();
    /// <summary>
    /// Путь ко всем картам в игре
    /// </summary>
    string allCardsPath;
    /// <summary>
    /// Список названий всех карт
    /// </summary>
    List<string> allCardsNames = new List<string>();

    /// <summary>
    /// Индекс выбранного бонуса
    /// </summary>
    int selectedBonus;
    /// <summary>
    /// Список всех бонусов
    /// </summary>
    List<BonusData> allBonuses = new List<BonusData>();
    /// <summary>
    /// Путь к списку бонусов
    /// </summary>
    string allBonusesPath;

    /// <summary>
    /// Составленная коллода. Список имен для интерфейса
    /// </summary>
    List<string> deck = new List<string>();
    /// <summary>
    /// Выбранная карта из уже существующих
    /// </summary>
    int selectedDecksCard;
    #endregion

    #region Обязательные методы интерфейса
    [MenuItem("DeadLords/Starter Packs Creator", false, 53)]
    public static void StarterPackCreatorWindow()
    {
        EditorWindow.GetWindow(typeof(StarterPackCreator));
    }

    private void OnEnable()
    {
        path = Application.dataPath + "/SaveData/Player's Deck.txt";
        allCardsPath = Application.dataPath + "/AllCards.txt";
        allBonusesPath = Application.dataPath + "/Bonuses.txt";

        CardData card;

        //Загрузка всех карт
        foreach (string s in File.ReadAllLines(allCardsPath))
        {
            card = JsonUtility.FromJson<CardData>(s);
            allCards.Add(card);
            allCardsNames.Add(card.cardName);
        }

        //Загрузка всех бонусов
        foreach (string s in File.ReadLines(allBonusesPath))
        {
            allBonuses.Add(JsonUtility.FromJson<BonusData>(s));
        }

        if (File.Exists(path))
        {
            foreach(string st in File.ReadAllLines(path))
            {
                completeDeck.Add(JsonUtility.FromJson<CardData>(st));
            }

            foreach (CardData cd in completeDeck)
                deck.Add(cd.cardName);
        }
    }

    private void OnGUI()
    {
        CreatePack();
    }
    #endregion
    void CreatePack()
    {
        selectedCard = EditorGUILayout.Popup(selectedCard, allCardsNames.ToArray());

        string type;

        if (allCards[selectedCard].manaSpell)
            type = "mana";
        else
            type = "action";


        EditorGUILayout.HelpBox("Card info:"
            + "\n" + allCards[selectedCard].cardName
            + "\n" + allCards[selectedCard].inGameName
            + "\n" + allCards[selectedCard].cost.ToString()
            + "\n" + type
            + "\n" + allCards[selectedCard].creatureName
            + "\n" + allCards[selectedCard].cardsBonusIndex.ToString(), MessageType.Info);

        if (allCards[selectedCard].creatureName == string.Empty)
        {
            EditorGUILayout.HelpBox("Spell info:"
                + "\n" + allBonuses[selectedBonus].unikName
                + "\n" + allBonuses[selectedBonus].fullName
                + "\n" + allBonuses[selectedBonus].type
                + "\n" + allBonuses[selectedBonus].target
                + "\n" + allBonuses[selectedBonus].att + "/" + allBonuses[selectedBonus].hp
                , MessageType.Info);
        }

        GUILayout.Space(10);
        selectedDecksCard = GUILayout.SelectionGrid(selectedDecksCard, deck.ToArray(), deck.Count);

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Add", "Добавить в коллоду выбранную карту")))
        {
            completeDeck.Add(allCards[selectedCard]);

            deck.Add(allCardsNames[selectedCard]);
            
        }

        if (GUILayout.Button(new GUIContent("Delete", "Удалить выбранную карту из коллоды")))
        {
            completeDeck.RemoveAt(selectedDecksCard);

            deck.RemoveAt(selectedDecksCard);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Save", "Сохранение в файл")))
        {
            List<string> saveList = new List<string>(); //Лист для сохранения. Зашифрованый в json

            foreach (CardData cd in completeDeck)
                saveList.Add(JsonUtility.ToJson(cd));

            File.WriteAllLines(path, saveList);

            Debug.Log("Saved in " + path);
        }
    }
}
