using DeadLords;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CardsEditor : EditorWindow
{
    #region Переменные
    /// <summary>
    /// Путь к файлу с картами
    /// </summary>
    string path;
    /// <summary>
    /// Список существующих карт
    /// </summary>
    List<CardData> existingCards = new List<CardData>();
    /// <summary>
    /// Имена существующих карт
    /// </summary>
    List<string> existingCardsNames = new List<string>();

    /// <summary>
    /// Путь к файлу бонусов
    /// </summary>
    string bonusesPath;
    /// <summary>
    /// Список всех возможных/сохраненных бонусов
    /// </summary>
    List<BonusData> bonusList = new List<BonusData>();
    /// <summary>
    /// Список названий бонусов для выбора конкретного бонуса
    /// </summary>
    List<string> bonusNames = new List<string>();

    /// <summary>
    /// Типы стоимости карт
    /// </summary>
    string[] costTypes = { "action", "mana" };
    /// <summary>
    /// Типы карт
    /// </summary>
    string[] spellTypes = { "Creature", "Spell" };

    //Кнопки и индексы внутри окна
    int selectedCardIndex = 0;  //Индекс выбранной карты
    int costType, spellType, bonusIndex;    //Индексы выбранных типов
    bool editButt = false, addButt = false; //Кнопки редактирования и добавления

    //CardData для записи
    string _name;
    string _inGameName;
    int _cost;
    bool _isManaSpell;
    bool _isCreature;

    GameObject _creature;
    BonusData _bonus;

    #endregion

    [MenuItem("DeadLords/Cards Editor", false, 52)]
    public static void CardsEditorWindow()
    {
        EditorWindow.GetWindow(typeof(CardsEditor));
    }

    private void OnEnable()
    {
        path = Application.dataPath + "/AllCards.txt";
        bonusesPath = Application.dataPath + "/Bonuses.txt";

        #region Загрузка существующих данных(карты и бонусы)
        //Загрузка бонусов
        if (File.Exists(bonusesPath))
        {
            foreach (string s in File.ReadAllLines(bonusesPath))
                bonusList.Add(JsonUtility.FromJson<BonusData>(s));

            //Добавление названий бонусов
            foreach (BonusData bd in bonusList)
                bonusNames.Add(bd.unikName);
        }
        else if (!File.Exists(bonusesPath) || File.Exists(bonusesPath) && File.ReadAllLines(path).Length == 0)
        {
            Debug.LogError("Файла не существует либо он пустой. Создайте его в BonusesNames окне");
            return;
        }

        //Загрузка существующих карт
        if (File.Exists(path))
        {
            if (File.ReadAllLines(path).Length == 0)
                return;

            foreach (string str in File.ReadLines(path))
                existingCards.Add(JsonUtility.FromJson<CardData>(str));

            foreach (CardData c in existingCards)
                existingCardsNames.Add(c.cardName);
        }
        else
            File.Create(path);
        #endregion
    }

    private void OnGUI()
    {
        GUILayout.Label("Работа с картами. Создание/редактирование/просмотр", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (existingCards.Count == 0)
            CreateCard();
        else if (editButt == false && addButt == false)  //Кнопки внутри InfoScreen
            InfoScreen();
        else if (editButt)
            EditCard(selectedCardIndex);
        else if (addButt)
            CreateCard();
    }

    private void InfoScreen()
    {
        GUILayout.BeginHorizontal();
        selectedCardIndex = EditorGUILayout.Popup(selectedCardIndex, existingCardsNames.ToArray());
        editButt = GUILayout.Button(new GUIContent("Edit", "Edit selected card"));
        addButt = GUILayout.Button(new GUIContent("Add", "Add new card"));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.EndHorizontal();
    }

    private void EditCard(int selectedCard)
    {

    }

    private void CreateCard()
    {
        CardData cd = new CardData();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Уникальное имя для редактора", "Не будет видно в игре(игроку)"));
        _name = EditorGUILayout.TextField(_name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Внутриигровое имя", "Название карты которое будет видно непосредственно игроку"));
        _inGameName = EditorGUILayout.TextField(_inGameName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Стоимость карты и необходимый ресурс", "Т.е. мана или очки действия"));
        _cost = EditorGUILayout.IntSlider(_cost, 0, 10);

        costType = EditorGUILayout.Popup(costType, costTypes);
        if (costType == 0)
            _isManaSpell = false;
        else
            _isManaSpell = true;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Тип карты", "Вызов существа или заклинание"));
        spellType = EditorGUILayout.Popup(spellType, spellTypes);
        if (spellType == 0)
        {
            _isCreature = true;
            _creature = EditorGUILayout.ObjectField(_creature, typeof(GameObject), true) as GameObject;
        }
        else
        {
            _isCreature = false;
            bonusIndex = EditorGUILayout.Popup(bonusIndex, bonusNames.ToArray());

            _bonus = bonusList[bonusIndex];
        }
        GUILayout.EndHorizontal();

        if (!_isCreature)
            EditorGUILayout.HelpBox("BonusInfo:" +
                "\n" + _bonus.unikName +
                "\n" + _bonus.type +
                "\n" + _bonus.fullName +
                "\n" + _bonus.target +
                "\n" + _bonus.att + "/" + _bonus.hp, MessageType.Info);

        GUILayout.Space(10);
        GUILayout.Label("Сохранить можно только если все поля заполнены");

        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_name == string.Empty || _inGameName == string.Empty))
        {
            //Save button
            if (GUILayout.Button(new GUIContent("Save", "Сохраняет карту и добавляет ее в список. Не сохраняет в файл")))
            {

            }
        }

        GUILayout.EndHorizontal();
    }
}
