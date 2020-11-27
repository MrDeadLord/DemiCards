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
    List<CardData> cardsList = new List<CardData>();
    /// <summary>
    /// Имена существующих карт
    /// </summary>
    List<string> cardsNames = new List<string>();

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
    int costType, spellType;    //Индексы выбранных типов
    bool editButt = false, addButt = false; //Кнопки редактирования и добавления

    //CardData для записи
    string _name;
    string _inGameName;
    int _cost;
    bool _isManaSpell;
    bool _isCreature;
    int bonusIndex;

    GameObject _creature;
    BonusData _bonus;   //Для интерфейса
    #endregion

    #region Обязательные методы интерфейса
    [MenuItem("DeadLords/Cards Editor", false, 52)]
    public static void CardsEditorWindow()
    {
        EditorWindow.GetWindow(typeof(CardsEditor));
    }

    private void OnEnable()
    {
        bonusesPath = Application.dataPath + "/Bonuses.txt";
        path = Application.dataPath + "/AllCards.txt";

        #region Загрузка существующих данных(бонусы и карты)
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
                cardsList.Add(JsonUtility.FromJson<CardData>(str));

            foreach (CardData c in cardsList)
                cardsNames.Add(c.cardName);
        }
        else
            File.Create(path);
        #endregion
    }

    private void OnGUI()
    {
        GUILayout.Label("Работа с картами. Создание/редактирование/просмотр", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (cardsList.Count == 0)
            CreateCard();
        else if (editButt == false && addButt == false)  //Кнопки внутри InfoScreen
            InfoScreen();
        else if (editButt)
            EditCard();
        else if (addButt)
            CreateCard();
    }
    #endregion
    private void InfoScreen()
    {
        GUILayout.BeginHorizontal();
        selectedCardIndex = EditorGUILayout.Popup(selectedCardIndex, cardsNames.ToArray());
        editButt = GUILayout.Button(new GUIContent("Edit", "Edit selected card"));
        addButt = GUILayout.Button(new GUIContent("Add", "Add new card"));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Уникальное имя(для редактора):");
        GUILayout.Label(cardsList[selectedCardIndex].cardName);
        _name = cardsList[selectedCardIndex].cardName;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Игровое название(для игрока):");
        GUILayout.Label(cardsList[selectedCardIndex].inGameName);
        _inGameName = cardsList[selectedCardIndex].inGameName;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Стоимость карты:");
        GUILayout.Label(cardsList[selectedCardIndex].cost.ToString());
        _cost = cardsList[selectedCardIndex].cost;

        //Тип стоимости
        if (cardsList[selectedCardIndex].manaSpell)
        {
            GUILayout.Label("mana");
            _isManaSpell = true;
        }
        else
        {
            GUILayout.Label("action");
            _isManaSpell = false;
        }            
        GUILayout.EndHorizontal();

        //Если вызов существа
        if (cardsList[selectedCardIndex].creatureName != string.Empty)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Имя призываемого существа:");
            GUILayout.Label(cardsList[selectedCardIndex].creatureName, EditorStyles.centeredGreyMiniLabel);
            GUILayout.EndHorizontal();

            _isCreature = true;
        }
        //Если заклинание
        else
        {
            _bonus = bonusList[cardsList[selectedCardIndex].cardsBonusIndex];

            GUILayout.Space(10);
            GUILayout.Label("Spell info:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Название(для редактора):");
            GUILayout.Label(_bonus.unikName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Полное описание(для игрока):");
            GUILayout.Label(_bonus.fullName);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Тип заклинания(для редактора):");
            GUILayout.Label(_bonus.type.ToString());
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Цeль(-и):");
            GUILayout.Label(_bonus.target.ToString());
            GUILayout.EndHorizontal();

            if (_bonus.type != SpellType.heal)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Значение attack:");
                GUILayout.Label(_bonus.att.ToString());
                GUILayout.EndHorizontal();
            }

            if (_bonus.type != SpellType.damage)
            {
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Значение HP:      ");
                GUILayout.Label(_bonus.hp.ToString());
                GUILayout.EndHorizontal();
            }
        }

        #region Сохранение в файл
        if (GUILayout.Button(new GUIContent("Save", "Сохранить все изменения в файл")))
        {
            var saveList = new List<string>();  //писок зашифрованных Json-ом строк

            foreach (CardData cd in cardsList)
                saveList.Add(JsonUtility.ToJson(cd));

            File.WriteAllLines(path, saveList.ToArray());

            Debug.Log("Saved in " + path);
        }
        #endregion
    }

    /// <summary>
    /// Сохдание новой карты
    /// </summary>
    private void CreateCard()
    {
        if (!addButt)
            addButt = true; //Если было запущено ввиду отсутствия карт в списке, нужно отметить, что сейчас действует это окно

        CardData cd = new CardData();

        GUILayout.Label("Создание новой карты", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Уникальное имя для редактора", "Не будет видно в игре(игроку)"));
        _name = EditorGUILayout.TextField(_name);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Внутриигровое имя", "Название карты которое будет видно непосредственно игроку"));
        _inGameName = EditorGUILayout.TextField(_inGameName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Стоимость карты и необходимый ресурс", "Т.е. мана или очки действия"));
        _cost = EditorGUILayout.IntSlider(_cost, 0, 10);

        costType = EditorGUILayout.Popup(costType, costTypes);
        if (costType == 0)
            _isManaSpell = false;
        else
            _isManaSpell = true;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
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

        //Помогайка
        GUILayout.Space(10);
        if (!_isCreature && _bonus.type != SpellType.heal)
            EditorGUILayout.HelpBox("Spell Info:" +
                "\n" + _bonus.unikName +
                "\n" + _bonus.type +
                "\n" + _bonus.fullName +
                "\n" + _bonus.target +
                "\n" + _bonus.att + "/" + _bonus.hp, MessageType.Info);
        else if (!_isCreature && _bonus.type == SpellType.heal)
            EditorGUILayout.HelpBox("Spell Info:" +
                "\n" + _bonus.unikName +
                "\n" + _bonus.type +
                "\n" + _bonus.fullName +
                "\n" + _bonus.target +
                "\n" + _bonus.hp, MessageType.Info);

        #region Сохранение в кэш/отмена
        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_name == string.Empty || _inGameName == string.Empty))
        {
            //Save button
            if (GUILayout.Button(new GUIContent("Save", "Сохраняет карту и добавляет ее в список. Не сохраняет в файл")))
            {
                cd.cardName = _name;
                cd.inGameName = _inGameName;
                cd.cost = _cost;
                cd.manaSpell = _isManaSpell;

                if (_isCreature)
                {
                    cd.creatureName = _creature.name;
                    cd.cardsBonusIndex = 0;
                }
                else
                {
                    cd.creatureName = string.Empty;
                    cd.cardsBonusIndex = bonusIndex;
                }

                cardsList.Add(cd);
                cardsNames.Add(cd.cardName);

                addButt = false;
            }
        }

        using (new EditorGUI.DisabledGroupScope(cardsList.Count == 0))
        {
            if (GUILayout.Button(new GUIContent("Cancel", "Отменить изменения")))
                addButt = false;
        }
        GUILayout.EndHorizontal();
        #endregion
    }

    /// <summary>
    /// Редактирование выбранной карты
    /// </summary>
    private void EditCard()
    {
        if (!editButt)
            editButt = true;

        CardData cd = new CardData();

        GUILayout.Label("Редактирование карты", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Уникальное имя для редактора", "Не будет видно в игре(игроку)"));
        _name = EditorGUILayout.TextField(_name);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Внутриигровое имя", "Название карты которое будет видно непосредственно игроку"));
        _inGameName = EditorGUILayout.TextField(_inGameName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Стоимость карты и необходимый ресурс", "Т.е. мана или очки действия"));
        _cost = EditorGUILayout.IntSlider(_cost, 0, 10);

        if (cardsList[selectedCardIndex].manaSpell)
            costType = 1;
        else
            costType = 0;

        costType = EditorGUILayout.Popup(costType, costTypes);
        if (costType == 0)
            _isManaSpell = false;
        else
            _isManaSpell = true;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
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

        //Помогайка
        GUILayout.Space(10);
        if (!_isCreature && _bonus.type != SpellType.heal)
            EditorGUILayout.HelpBox("Spell Info:" +
                "\n" + _bonus.unikName +
                "\n" + _bonus.type +
                "\n" + _bonus.fullName +
                "\n" + _bonus.target +
                "\n" + _bonus.att + "/" + _bonus.hp, MessageType.Info);
        else if (!_isCreature && _bonus.type == SpellType.heal)
            EditorGUILayout.HelpBox("Spell Info:" +
                "\n" + _bonus.unikName +
                "\n" + _bonus.type +
                "\n" + _bonus.fullName +
                "\n" + _bonus.target +
                "\n" + _bonus.hp, MessageType.Info);

        #region Сохранение в кэш/Отмена
        GUILayout.BeginHorizontal();
        //Save button
        using (new EditorGUI.DisabledGroupScope(_name == string.Empty || _inGameName == string.Empty))
        {
            if (GUILayout.Button(new GUIContent("Save", "Сохранение изменений. Не сохраняет в файл")))
            {
                cd.cardName = _name;
                cd.inGameName = _inGameName;
                cd.cost = _cost;
                cd.manaSpell = _isManaSpell;

                if (_isCreature)
                {
                    cd.creatureName = _creature.name;
                    cd.cardsBonusIndex = 0;
                }
                else
                {
                    cd.creatureName = string.Empty;
                    cd.cardsBonusIndex = bonusIndex;
                }

                cardsList[selectedCardIndex] = cd;
                cardsNames[selectedCardIndex] = cd.cardName;

                editButt = false;
            }
        }

        //Cancel button
        using (new EditorGUI.DisabledGroupScope(cardsList.Count == 0))
        {
            if (GUILayout.Button(new GUIContent("Cancel", "Отменить изменения")))
                editButt = false;
        }
        GUILayout.EndHorizontal();
        #endregion Сохранение в кэш/Отмена

        #region Инфо о редактируемой карте
        EditorGUILayout.HelpBox("---Редактируемая карта---", MessageType.Info);

        GUILayout.Space(10);
        GUILayout.Label("Карта:", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Уникальное имя(для редактора):");
        GUILayout.Label(cardsList[selectedCardIndex].cardName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Игровое название(для игрока):");
        GUILayout.Label(cardsList[selectedCardIndex].inGameName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Стоимость карты:");
        GUILayout.Label(cardsList[selectedCardIndex].cost.ToString());

        if (cardsList[selectedCardIndex].manaSpell)
            GUILayout.Label("mana");
        else
            GUILayout.Label("action");
        GUILayout.EndHorizontal();

        //Если вызов существа
        if (cardsList[selectedCardIndex].creatureName != string.Empty)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Имя призываемого существа:");
            GUILayout.Label(cardsList[selectedCardIndex].creatureName, EditorStyles.centeredGreyMiniLabel);
            GUILayout.EndHorizontal();
        }
        //Если заклинание
        else
        {
            _bonus = bonusList[cardsList[selectedCardIndex].cardsBonusIndex];

            GUILayout.Space(10);
            GUILayout.Label("Spell info:", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Название(для редактора):");
            GUILayout.Label(_bonus.unikName);
            GUILayout.EndHorizontal();
        }
        #endregion
    }
}
