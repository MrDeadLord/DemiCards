using DeadLords.Helpers;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Присваивается к UI.button
    /// </summary>
    [RequireComponent(typeof(Material))]
    public class Card : MonoBehaviour
    {
        [SerializeField] [Tooltip("Название карты")] private string _name;
        [SerializeField] [Tooltip("Название карты для игрока")] private string _inGameName;
        [SerializeField] [Tooltip("Стоимость карты")] [Range(0, 10)] private int _cost;
        [SerializeField] [Tooltip("Uncheck, если стоимость в ОД")] private bool _manaSpell;
        [Space(10)]
        [SerializeField] [Tooltip("Вызываемое существо")] private GameObject _creature;

        #region Бонус, если он есть
        [Space(10)]
        [Header("Бонус, если его нет - не заполнять")]

        [SerializeField] [Tooltip("Название заклинания")] private string _bonusName;
        [SerializeField] [Tooltip("Тип бонуса/заклинания(Для вызова в функции)")] private string _bonusType;
        [SerializeField] [Tooltip("Полное описание бонуса")] private string _bonusFullName;
        [SerializeField] [Tooltip("Цель/цели заклинания/бонуса")] private string _bonusTarget;
        [SerializeField] [Tooltip("Значение атаки. Если оно не нужно - 0")] private int _bonusAtt;
        [SerializeField] [Tooltip("Значение HP. Если оно не нужно - 0")] private int _bonusHP;
        #endregion

        /// <summary>
        /// True если карта с вызовом существа
        /// </summary>
        private bool _isCreature;
        /// <summary>
        /// true если карта на столе, false - в руке
        /// </summary>
        private bool _isActive = false;

        private void Start()
        {
            if (_creature != null)
                _isCreature = true;
            else if (_bonusName != string.Empty)
                _isCreature = false;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            if (_isCreature)
                Summon();
            else
                ActivateBonus();
        }

        private void Summon()
        {
            if (Main.Instance.GetSpawnController.CanSpawn(transform.parent.tag))
            {
                Main.Instance.GetSpawnController.Spawn(_creature, transform.parent.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Too many creatures allready");
                _isActive = false;
            }
        }

        private void ActivateBonus()
        {

        }

        #region Для редактора
        /// <summary>
        /// Название карты
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// Название карты для игрока
        /// </summary>
        public string InGameName
        {
            get { return _inGameName; }
            set { _inGameName = value; }
        }
        /// <summary>
        /// Стоимость карты
        /// </summary>
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        /// <summary>
        /// Стоимость в мане?
        /// </summary>
        public bool IsManaSpell
        {
            get { return _manaSpell; }
            set { _manaSpell = value; }
        }
        /// <summary>
        /// Существо ли это?
        /// </summary>
        public bool IsCreature
        {
            get { return _isCreature; }
            set { _isCreature = value; }
        }
        /// <summary>
        /// Существо, если карта - вызов существа
        /// </summary>
        public GameObject CardsCreature
        {
            get { return _creature; }
            set { _creature = value; }
        }

        #region Бонус и вся его инфа
        /// <summary>
        /// Имя бонуса
        /// </summary>
        public string BonusName
        {
            get { return _bonusName; }
            set { _bonusName = value; }
        }
        /// <summary>
        /// Тип бонуса
        /// </summary>
        public string BonusType
        {
            get { return _bonusType; }
            set { _bonusType = value; }
        }
        /// <summary>
        /// Полное описание бонуса
        /// </summary>
        public string BonusFullName
        {
            get { return _bonusFullName; }
            set { _bonusFullName = value; }
        }
        /// <summary>
        /// Цель бонуса
        /// </summary>
        public string BonusTarget
        {
            get { return _bonusTarget; }
            set { _bonusTarget = value; }
        }
        /// <summary>
        /// Значение Attack бонуса
        /// </summary>
        public int BonusAtt
        {
            get { return _bonusAtt; }
            set { _bonusAtt = value; }
        }
        /// <summary>
        /// Значение НР бонуса
        /// </summary>
        public int BonusHP
        {
            get { return _bonusHP; }
            set { _bonusHP = value; }
        }
        #endregion

        /// <summary>
        /// Если нужно применить карту - true
        /// </summary>
        public bool Active
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion
    }
}