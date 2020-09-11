using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BonusesEditor : EditorWindow
{
    #region Переменные
    /// <summary>
    /// Путь к файлу сохранения бонусов
    /// </summary>
    string path = string.Empty;
    bool editButt, addButt;  //Кнопки рядом с выбором бонуса/заклинания

    /// <summary>
    /// Список существующих(созданных) бонусов/заклинаний
    /// </summary>
    List<BonusData> bonusList;
    /// <summary>
    /// Список сокращенных имен, которые есть у существующих бонусов, для удобства.
    /// </summary>
    List<string> bonusNames = new List<string>();

    int selectedBonus = 0;
    /// <summary>
    /// Все возможные цели заклинаний
    /// </summary>
    string[] targets = { "Summoner", "Enemy", "Summoner and enemy", "Elly creature", "Enemy creature",
        "All ally's creatures", "All enemy's creatures", "All creatures", "Everyone"};
    /// <summary>
    /// Выбранная цель(индекс). Для записи выбранной цели
    /// </summary>
    int selectedTarget;

    //Для редактирования типа
    /// <summary>
    /// Путь к файлу с именами
    /// </summary>
    string _allBonusTypesPath;
    /// <summary>
    /// Список всех возможных имен бонусов
    /// </summary>
    List<string> _allBonusTypes = new List<string>();
    int _selectedTypeIndex;
    string newType = string.Empty;

    bool _editTypesButt = false;

    //Для создания и редактирования бонусов нужны переменные
    string _unikName, _type, _fullName, _target;
    int _att, _hp;
    #endregion

    [MenuItem("DeadLords/Bonuses Editor", false, 51)]
    public static void BonusesEditorWindow()
    {
        EditorWindow.GetWindow(typeof(BonusesEditor));
    }

    private void OnEnable()
    {
        //Если файл есть, открывает, записывает в bonusList. Если файла нет - создает пустой
        path = Application.dataPath + "/Bonuses.txt";
        _allBonusTypesPath = Application.dataPath + "/AllBonusTypes.txt";

        bonusList = bonusList = new List<BonusData>();

        //Получение списка всех бонусов, что были созданы, если их нет - создаем пустой файл, куда потом будем записывать данные
        if (File.Exists(path))
        {
            foreach (string b in File.ReadAllLines(path))
                bonusList.Add(JsonUtility.FromJson<BonusData>(b));

            foreach (BonusData bd in bonusList)
                bonusNames.Add(bd.unikName);
        }
        else
            File.Create(path);

        //Названия всех возможных имен бонусов хранится отдельно
        if (File.Exists(_allBonusTypesPath))
        {
            if (File.ReadAllLines(_allBonusTypesPath).Length == 0)
                return;

            foreach (string s in File.ReadAllLines(_allBonusTypesPath))
                _allBonusTypes.Add(s);
        }
        else
            File.Create(_allBonusTypesPath);
    }

    private void OnGUI()
    {
        GUILayout.Label("Работа со списком ВСЕХ бонусов в игре", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (_allBonusTypes.Count == 0)  //Если нет типов бонусов
            EditBonusTypes();
        else if (bonusList.Count == 0 && !_editTypesButt)   ///Ничего не нажато, а бонусов нет. Кнопки находятся в ShowBonus
            AddBonus();
        else if (!editButt && !addButt && !_editTypesButt)  //Список типов есть, бонусы есть, ничего не нажато. Кнопки находятся в ShowBonus
            ShowBonus(selectedBonus);
        else if (editButt)
            EditBonus(selectedBonus);
        else if (addButt)
            AddBonus();

        if (_editTypesButt)
        {
            addButt = false;
            editButt = false;
            EditBonusTypes();
        }
    }

    /// <summary>
    /// Редактирование имен
    /// </summary>
    private void EditBonusTypes()
    {
        if (!_editTypesButt)
            _editTypesButt = true;

        var backup = _allBonusTypes;

        GUILayout.Space(10);

        GUILayout.Label("Edit bonus types");
        _selectedTypeIndex = EditorGUILayout.Popup(_selectedTypeIndex, _allBonusTypes.ToArray());

        if (_allBonusTypes.Count == 0 || _allBonusTypes[_selectedTypeIndex] == string.Empty)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Добавить новое имя:");
            newType = EditorGUILayout.TextField(newType);

            using (new EditorGUI.DisabledGroupScope(newType == string.Empty))
            {
                if (GUILayout.Button(new GUIContent("Add", "Добавить имя в список")))
                {
                    if (_allBonusTypes.Count == 0)
                        _allBonusTypes.Add(newType);
                    else
                        _allBonusTypes[_selectedTypeIndex] = newType;

                    newType = string.Empty;

                    Debug.Log("Type added!");
                }
            }

            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Edit tag:");
            newType = EditorGUILayout.TextField(newType);

            using (new EditorGUI.DisabledGroupScope(newType == string.Empty))
            {
                if (GUILayout.Button(new GUIContent("Confurm edit", "Подтвертить новое имя и перезаписать его в список")))
                    _allBonusTypes[_selectedTypeIndex] = newType;
            }

            if (GUILayout.Button("Add new"))
            {
                if (!_allBonusTypes.Contains(newType))
                    _allBonusTypes.Add(newType);
            }

            GUILayout.EndHorizontal();
        }

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли был изменен этот список, необходимо сопоставить его с Cards/Bonus скриптом", MessageType.Info);

        #region Кнопки сохранить и отменить
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("Confurm", "Сохраняет имена в файл")))
        {
            File.WriteAllLines(_allBonusTypesPath, _allBonusTypes);

            _editTypesButt = false;
        }

        if (GUILayout.Button(new GUIContent("Cancel", "Отменяет все изменения в этом окне и возвращает на экран с Info")))
        {
            _allBonusTypes = backup;
            _editTypesButt = false;
        }
        GUILayout.EndHorizontal();
        #endregion
    }

    /// <summary>
    /// Добавление нового бонуса/заклинания
    /// </summary>
    private void AddBonus()
    {
        GUILayout.Label("Добавление бонуса", EditorStyles.boldLabel);
        GUILayout.Space(10);

        BonusData bd = new BonusData();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Название заклинания:", "Уникальное имя"));
        _unikName = GUILayout.TextArea(_unikName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Тип бонуса/заклинания(для вызова метода):", "Может повторяться"));
        _selectedTypeIndex = EditorGUILayout.Popup(_selectedTypeIndex, _allBonusTypes.ToArray());
        _type = _allBonusTypes[_selectedTypeIndex];
        _editTypesButt = GUILayout.Button(new GUIContent("Edit", "Изменить список типов"));
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Полное описание бонуса/заклинания(для игрока):",
            "Beta: пока нужно следить что бы описание совпадало с остальными данными"));
        _fullName = GUILayout.TextArea(_fullName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Цель(-и):");
        selectedTarget = EditorGUILayout.Popup(selectedTarget, targets);
        _target = targets[selectedTarget];
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Значение attack:", "Если не нужно - 0"));
        _att = EditorGUILayout.IntSlider(_att, 0, 20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Значение HP:      ", "Если не нужно - 0"));
        _hp = EditorGUILayout.IntSlider(_hp, 0, 20);
        GUILayout.EndHorizontal();

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли необходимо сделать, например, +att/-hp, нужно написать bust/debust" +
                "\nЕсли был изменен список сокращенных имен, необходимо сопоставить его с Bonus скриптом", MessageType.Info);

        #region Сохранение
        GUILayout.Space(10);
        GUILayout.Label("Сохранить можно только если все поля заполнены");

        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_unikName == string.Empty || _fullName == string.Empty
            || _target == string.Empty))
        {
            //Кнопка сохранения
            if (GUILayout.Button(new GUIContent("Add bonus", "Завершение редактирование и переход к окну Info")))
            {
                //Перепись данных. Редактируемый бонус будет удален и добавлен в конец списка
                bd.unikName = _unikName.ToLower();
                bd.type = _type;
                bd.fullName = _fullName;
                bd.target = _target;
                bd.att = _att;
                bd.hp = _hp;

                bonusList.Add(bd);
                bonusNames.Add(bd.unikName);

                addButt = false;    //Что бы вернуться к окну Info
            }
        }

        //Кнопка отмены
        if (GUILayout.Button(new GUIContent("Cancel", "Отменяет все операции")))
        {
            addButt = false;
        }
        GUILayout.EndHorizontal();
        #endregion
    }

    /// <summary>
    /// Показвыбранного бонуса/заклинания
    /// </summary>
    /// <param name="selected">Индекс бонуса/заклинания</param>
    private void ShowBonus(int selected)
    {
        GUILayout.BeginHorizontal();
        selectedBonus = EditorGUILayout.Popup(selectedBonus, bonusNames.ToArray());

        editButt = GUILayout.Button("Edit");
        addButt = GUILayout.Button("Add new");
        GUILayout.EndHorizontal();

        GUILayout.Label("Полное описание бонуса:", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Название(уникальное имя):");
        GUILayout.Label(bonusList[selected].unikName);
        _unikName = bonusList[selected].unikName;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Тип(для вызова):");
        GUILayout.Label(bonusList[selected].type);
        _type = bonusList[selected].type;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Полное описание бонуса/заклинания(для игрока):");
        GUILayout.Label(bonusList[selected].fullName);
        _fullName = bonusList[selected].fullName;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Цель:");
        GUILayout.Label(bonusList[selected].target);
        _target = bonusList[selected].target;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Значение attack:");
        GUILayout.Label(bonusList[selected].att.ToString());
        _att = bonusList[selected].att;
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Значение HP:      ");
        GUILayout.Label(bonusList[selected].hp.ToString());
        _hp = bonusList[selected].hp;
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли необходимо сделать, например, +att/-hp, будет написано bust/debust", MessageType.Info);

        #region Сохранение в файл
        if (GUILayout.Button(new GUIContent("Save", "Сохраняет все новое/редактированное в файл")))
        {
            List<string> saveNames = new List<string>();    //Списокдля сохранения

            foreach (BonusData bd in bonusList)
                saveNames.Add(JsonUtility.ToJson(bd));

            File.WriteAllLines(path, saveNames.ToArray());
            Debug.Log("Bonuses saved");
        }
        #endregion
    }

    /// <summary>
    /// Изменение выбранного бонуса/заклинания
    /// </summary>
    /// <param name="selected">Индекс выбранного бонуса</param>
    private void EditBonus(int selected)
    {
        GUILayout.Label("Редактирование бонуса", EditorStyles.boldLabel);
        GUILayout.Space(10);

        BonusData bd = new BonusData();

        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Название заклинания:", "Уникальное имя"));
        _unikName = GUILayout.TextArea(_unikName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Тип бонуса/заклинания(для вызова метода):", "Может повторяться"));
        _selectedTypeIndex = EditorGUILayout.Popup(_selectedTypeIndex, _allBonusTypes.ToArray());
        _type = _allBonusTypes[_selectedTypeIndex];
        _editTypesButt = GUILayout.Button(new GUIContent("Edit", "Изменить список типов"));
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.Label(new GUIContent("Полное описание бонуса/заклинания(для игрока):",
            "Beta: пока нужно следить что бы описание совпадало с остальными данными"));
        _fullName = EditorGUILayout.TextArea(_fullName);

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Цель:");
        selectedTarget = EditorGUILayout.Popup(selectedTarget, targets);
        _target = targets[selectedTarget];
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Значение attack:", "Если не нужно - 0"));
        _att = EditorGUILayout.IntSlider(_att, 0, 20);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label(new GUIContent("Значение HP:", "Если не нужно - 0"));
        _hp = EditorGUILayout.IntSlider(_hp, 0, 20);
        GUILayout.EndHorizontal();

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли необходимо сделать, например, +att/-hp, нужно написать bust/debust" +
                "\nЕсли был изменен список сокращенных имен, необходимо сопоставить его с Bonus скриптом", MessageType.Info);

        #region Сохранение
        GUILayout.Space(10);
        GUILayout.Label("Сохранить можно только если все поля заполнены");

        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_unikName == string.Empty || _fullName == string.Empty
            || _target == string.Empty))
        {
            if (GUILayout.Button(new GUIContent("Confurm", "Завершение редактирование и переход к окну Info")))
            {
                //Перепись данных.
                bd.unikName = _unikName.ToLower();
                bd.type = _type;
                bd.fullName = _fullName;
                bd.target = _target;
                bd.att = _att;
                bd.hp = _hp;

                bonusList[selected] = bd;
                bonusNames[selected] = bd.unikName;

                editButt = false;
            }
        }

        if (GUILayout.Button(new GUIContent("Cancel", "Отменяет все операции")))
            editButt = false;
        GUILayout.EndHorizontal();
        #endregion
    }
}