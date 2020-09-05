using DeadLords;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class NewCardsEditor : EditorWindow
{
    #region Переменные

    List<Card> cardsList = new List<Card>();
    string savePath;

    //Для удобства в редакторе
    /// <summary>
    /// Типы стоимости карт
    /// </summary>
    string[] costTypes = { "action", "mana" };
    /// <summary>
    /// Типы карт
    /// </summary>
    string[] spellTypes = { "Creature", "Spell" };
    int indexCost, indexType;
    /// <summary>
    /// Все возможные цели заклинаний
    /// </summary>
    string[] targets = { "Summoner", "Enemy", "Summoner and enemy", "Elly creature", "Enemy creature",
        "All ally's creatures", "All enemy's creatures", "All creatures", "Everyone"};
    /// <summary>
    /// Выбранная цель(индекс). Для записи выбранной цели
    /// </summary>
    int indexTarget;

    //Переменные-составляющие карты
    string _name, _inGameName;
    int _cost;
    bool _isManaSpell;
    bool _isCreature;

    GameObject _creature;
    //Далее параметры бонуса
    string _bonusName, _bonusFullName;
    string _bonusType;
    string _bonusTarget;
    int _bonusAtt, _bonusHP;

    #endregion

    [MenuItem("DeadLords/new Cards Editor", false, 53)]
    public static void NewCardsEditorWindow()
    {
        EditorWindow.GetWindow(typeof(NewCardsEditor));
    }

    private void OnEnable()
    {
        savePath = Application.dataPath + "/SaveData/Cards.txt";

        if (File.Exists(savePath))
        {
            if (File.ReadAllLines(savePath).Length != 0)
            {
                foreach (string s in File.ReadAllLines(savePath))
                {
                    cardsList.Add(JsonUtility.FromJson<Card>(s));
                }
            }
        }
        else
        {
            File.Create(savePath);
        }
    }

    private void OnGUI()
    {
        CreateCard();
    }

    void CreateCard()
    {
        GUILayout.Label("Создание карты", EditorStyles.boldLabel);
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Название карты(для редактора):");
        _name = GUILayout.TextField(_name);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Название карты(для юзера):");
        _inGameName = GUILayout.TextField(_inGameName);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Стоимость и типа карты:");
        _cost = EditorGUILayout.IntSlider(_cost, 0, 20);

        indexCost = EditorGUILayout.Popup(indexCost, costTypes);
        if (indexCost == 0)
            _isManaSpell = false;
        else
            _isManaSpell = true;

        indexType = EditorGUILayout.Popup(indexType, spellTypes);
        if (indexType == 0)
            _isCreature = true;
        else
            _isCreature = false;

        if (_isCreature)
        {
            _creature = EditorGUILayout.ObjectField(_creature, typeof(GameObject), true) as GameObject;
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (!_isCreature)
        {
            GUILayout.Label("Бонус", EditorStyles.boldLabel);

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Название бонуса(для редактора):");
            _bonusName = GUILayout.TextField(_bonusName);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Полное описание бонуса для юзера:");
            _bonusFullName = GUILayout.TextArea(_bonusFullName);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Цель и цифры бонуса:");
            indexTarget = EditorGUILayout.Popup(indexTarget, targets);
            GUILayout.EndHorizontal();

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Attack:");
            _bonusAtt = EditorGUILayout.IntSlider(_bonusAtt, 0, 20);
            GUILayout.Label("HP:");
            _bonusHP = EditorGUILayout.IntSlider(_bonusHP, 0, 20);
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(10);
        if (GUILayout.Button(new GUIContent("Save", "Сохраняет карту(-ы) в файл")))
        {
            var card = new Card();

            card.Name = _name;
            card.InGameName = _inGameName;
            card.Cost = _cost;
            card.IsManaSpell = _isManaSpell;
            card.IsCreature = _isCreature;
            card.CardsCreature = _creature;
            card.BonusName = _bonusName;
            card.BonusFullName = _bonusFullName;
            card.BonusType = _bonusType;
            card.BonusTarget = _bonusTarget;
            card.BonusAtt = _bonusAtt;
            card.BonusHP = _bonusHP;

            var str = JsonUtility.ToJson(card);
            using (StreamWriter sw = File.AppendText(savePath))
                sw.WriteLine(str);
        }
    }
}
