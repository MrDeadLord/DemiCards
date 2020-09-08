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

        private BonusData _cardsBonus;
        #endregion

        /// <summary>
        /// true если карта на столе, false - в руке
        /// </summary>
        private bool _isActive = false;

        #region Unity time        
        private void Update()
        {
            if (!_isActive)
                return;

            if (_creature.name != string.Empty)
                Summon();
            else
                ActivateBonus();
        }
        #endregion

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
        /// Существо, если карта - вызов существа
        /// </summary>
        public GameObject CardsCreature
        {
            get { return _creature; }
            set { _creature = value; }
        }

        public BonusData CardsBonus
        {
            get { return _cardsBonus; }
            set { _cardsBonus = value; }
        }

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