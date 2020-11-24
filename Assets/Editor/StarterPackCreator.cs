using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class StarterPackCreator : EditorWindow
{
    #region Переменные
    /// <summary>
    /// Путь сохранения пака игрока(player's deck)
    /// </summary>
    string path;
    /// <summary>
    /// Путь сохранения пака для врага(enemy's deck)
    /// </summary>
    string pathEnemy;
    /// <summary>
    /// Финальный список карт для сохранения игрока
    /// </summary>
    List<CardData> completeDeck = new List<CardData>();
    /// <summary>
    /// Финальный список карт для сохранения врага
    /// </summary>
    List<CardData> completeDeckEnemy = new List<CardData>();

    /// <summary>
    /// Выбранный игрок или враг кому будет создаваться коллода
    /// </summary>
    int selectedSum;
    /// <summary>
    /// Варианты кому создаем коллоду
    /// </summary>
    string[] summs = { "Player", "Enemy" };

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
    /// Составленная коллода игрока. Список имен для интерфейса
    /// </summary>
    List<string> deck = new List<string>();
    /// <summary>
    /// Составленная коллода врага. Список имен для интерфейса
    /// </summary>
    List<string> deckEnemy = new List<string>();
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
        allCardsPath = Application.dataPath + "/AllCards.txt";
        allBonusesPath = Application.dataPath + "/Bonuses.txt";

        path = Application.dataPath + "/SaveData/Player's Deck.txt";
        pathEnemy = Application.dataPath + "/SaveData/Enemy's Deck.txt";

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

        //Если коллода игрока уже есть - загружаем и ее
        if (File.Exists(path))
        {
            foreach (string st in File.ReadAllLines(path))
            {
                completeDeck.Add(JsonUtility.FromJson<CardData>(st));
            }

            foreach (CardData cd in completeDeck)
                deck.Add(cd.cardName);
        }

        //Если коллода врага уже есть - загружаем и ее
        if (File.Exists(pathEnemy))
        {
            foreach (string st in File.ReadAllLines(pathEnemy))
            {
                completeDeckEnemy.Add(JsonUtility.FromJson<CardData>(st));
            }

            foreach (CardData cd in completeDeckEnemy)
                deckEnemy.Add(cd.cardName);
        }
    }

    private void OnGUI()
    {
        selectedSum = GUILayout.SelectionGrid(selectedSum, summs, summs.Length);
        GUILayout.Space(10);

        if (selectedSum == 0)
        {
            CreatePack();
        }
        else
        {
            CreatePack();
        }
    }
    #endregion

    void CreatePack()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Список всех карт:");
        selectedCard = EditorGUILayout.Popup(selectedCard, allCardsNames.ToArray());
        GUILayout.EndHorizontal();

        string type;

        if (allCards[selectedCard].manaSpell)
            type = "mana";
        else
            type = "action";


        EditorGUILayout.HelpBox("Card info:"
            + "\n" + "Card name:          " + allCards[selectedCard].cardName
            + "\n" + "Card's ingame name: " + allCards[selectedCard].inGameName
            + "\n" + "Cost: " + allCards[selectedCard].cost.ToString() + type
            + "\n" + "Creature name:      " + allCards[selectedCard].creatureName
            + "\n" + "Bonus index:        " + allCards[selectedCard].cardsBonusIndex.ToString(), MessageType.Info);

        //Если это заклинание
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

        if (GUILayout.Button(new GUIContent("Add", "Добавить в коллоду выбранную карту")))
        {
            //Добавляем карту в коллоду выбранному сопернику
            if (selectedSum == 0)
            {
                completeDeck.Add(allCards[selectedCard]);

                deck.Add(allCardsNames[selectedCard]);
            }
            else
            {
                completeDeckEnemy.Add(allCards[selectedCard]);

                deckEnemy.Add(allCardsNames[selectedCard]);
            }

        }

        GUILayout.Space(10);
        selectedDecksCard = GUILayout.SelectionGrid(selectedDecksCard, deck.ToArray(), deck.Count);

        GUILayout.Space(10);        
        if (GUILayout.Button(new GUIContent("Delete", "Удалить выбранную карту из коллоды")))
        {
            //Удаляем карту в выбранной коллоде
            if (selectedSum == 0)
            {
                completeDeck.RemoveAt(selectedDecksCard);

                deck.RemoveAt(selectedDecksCard);
            }
            else
            {
                completeDeckEnemy.RemoveAt(selectedDecksCard);

                deckEnemy.RemoveAt(selectedDecksCard);
            }
                
        }

        GUILayout.Space(5);
        if (GUILayout.Button(new GUIContent("Save", "Сохранение в файл")))
        {
            List<string> saveList = new List<string>(); //Лист для сохранения. Будет зашифрован в json

            //Сохранение пака игрока
            foreach (CardData cd in completeDeck)
                saveList.Add(JsonUtility.ToJson(cd));

            File.WriteAllLines(path, saveList);

            //Сохранение пака врага
            foreach (CardData cd in completeDeckEnemy)
                saveList.Add(JsonUtility.ToJson(cd));

            File.WriteAllLines(pathEnemy, saveList);

            Debug.Log("Saved in " + path);
            Debug.Log("Saved in " + pathEnemy);
        }
    }
}
