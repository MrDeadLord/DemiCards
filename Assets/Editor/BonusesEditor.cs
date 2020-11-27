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

    //Переменные карты

    string _unikName, _fullName;
    /// <summary>
    /// Выбранная цель
    /// </summary>
    TargetType _target;
    SpellType _type;
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
    }

    private void OnGUI()
    {
        GUILayout.Label("Работа со списком ВСЕХ бонусов в игре", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (bonusList.Count == 0)   ///Ничего не нажато, а бонусов нет. Кнопки находятся в ShowBonus
            AddBonus();
        else if (!editButt && !addButt)  //Список типов есть, бонусы есть, ничего не нажато. Кнопки находятся в ShowBonus
            ShowBonus(selectedBonus);
        else if (editButt)
            EditBonus(selectedBonus);
        else if (addButt)
            AddBonus();
    }

    /// <summary>
    /// Показ выбранного бонуса/заклинания
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
        GUILayout.Label(_type.ToString());
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
        GUILayout.Label(bonusList[selected].target.ToString());
        _target = bonusList[selected].target;
        GUILayout.EndHorizontal();

        //Если это хилка, атаку не отображаем
        if (bonusList[selected].type != SpellType.heal)
        {
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Значение attack:");
            GUILayout.Label(bonusList[selected].att.ToString());
            _att = bonusList[selected].att;
            GUILayout.EndHorizontal();
        }

        if (bonusList[selected].type != SpellType.damage)
        {
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Значение HP:      ");
            GUILayout.Label(bonusList[selected].hp.ToString());
            _hp = bonusList[selected].hp;
            GUILayout.EndHorizontal();
        }

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

        _type = (SpellType)EditorGUILayout.EnumPopup(_type);
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

        _target = (TargetType)EditorGUILayout.EnumPopup(_target);
        GUILayout.EndHorizontal();

        //Если НЕ хилка, то отображаем атаку
        if (_type != SpellType.heal)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Значение attack:", "Если не нужно - 0"));

            _att = EditorGUILayout.IntSlider(_att, -20, 20);
            GUILayout.EndHorizontal();
        }
        //Если это все же хил, то значение атаки = 0
        else
            _att = 0;

        //Если это НЕ атака, то отображаем хп
        if (_type != SpellType.damage)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Значение HP:      ", "Если не нужно - 0"));
            _hp = EditorGUILayout.IntSlider(_hp, -20, 20);
            GUILayout.EndHorizontal();
        }
        //Если это все же нанесение урона(т.е. атака), но хп = 0
        else
            _hp = 0;

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли был изменен список сокращенных имен, необходимо сопоставить его с Bonus скриптом", MessageType.Info);

        #region Сохранение
        GUILayout.Space(10);
        GUILayout.Label("Сохранить можно только если все поля заполнены");

        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_unikName == string.Empty || _fullName == string.Empty))
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

        _type = (SpellType)EditorGUILayout.EnumPopup(_type);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.Label(new GUIContent("Полное описание бонуса/заклинания(для игрока):",
            "Beta: пока нужно следить что бы описание совпадало с остальными данными"));
        _fullName = EditorGUILayout.TextArea(_fullName);

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Цель:");

        _target = (TargetType)EditorGUILayout.EnumPopup(_target);
        GUILayout.EndHorizontal();

        //Если НЕ хилка, то отображаем атаку
        if (_type != SpellType.heal)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Значение attack:", "Если не нужно - 0"));

            _att = EditorGUILayout.IntSlider(_att, -20, 20);
            GUILayout.EndHorizontal();
        }
        //Если это все же хил, то значение атаки = 0
        else
            _att = 0;

        //Если это НЕ атака, то отображаем хп
        if (_type != SpellType.damage)
        {
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label(new GUIContent("Значение HP:      ", "Если не нужно - 0"));
            _hp = EditorGUILayout.IntSlider(_hp, -20, 20);
            GUILayout.EndHorizontal();
        }
        //Если это все же нанесение урона(т.е. атака), но хп = 0
        else
            _hp = 0;

        //Помогайка
        EditorGUILayout.HelpBox("Главные сокращения:" +
                "\nCreature-cr, destroy - dist," +
                "\nЕсли был изменен список сокращенных имен, необходимо сопоставить его с Bonus скриптом", MessageType.Info);

        #region Сохранение
        GUILayout.Space(10);
        GUILayout.Label("Сохранить можно только если все поля заполнены");

        GUILayout.BeginHorizontal();
        using (new EditorGUI.DisabledGroupScope(_unikName == string.Empty || _fullName == string.Empty))
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