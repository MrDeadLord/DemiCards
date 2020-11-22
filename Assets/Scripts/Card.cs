using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Класс карты, где хранится вся информация о ней. Присваивается к UI.button
    /// </summary>
    public class Card : MonoBehaviour
    {
        #region Переменные
        [SerializeField] [Tooltip("Полное содержание карты")] private CardData _cardData = new CardData();

        /// <summary>
        /// Полное содержание бонуса(если карта - заклинание)
        /// </summary>
        private BonusData _cardsBonus = new BonusData();

        /// <summary>
        /// Существо, если это призыв. Иначе - null
        /// </summary>
        private Creature _creature = new Creature();

        /// <summary>
        /// true если карта на столе, false - в руке
        /// </summary>
        private bool _isActive = false;
        #endregion

        #region Unity time
        private void Update()
        {
            if (!_isActive)
                return;

            if (_cardData.creatureName != string.Empty)
                Summon();
            else
                ActivateBonus();
        }

        #endregion Unity time

        /// <summary>
        /// Присваивание карты (карта = card)
        /// </summary>
        /// <param name="card">Карта, которую нужно присвоить</param>
        public void Assignment(Card card)
        {            
            _cardData = card.CardsData;
            _cardsBonus = card.CardsBonus;
            _creature = card.CardsCreature;
        }

        /// <summary>
        /// Призыв существа
        /// </summary>
        void Summon()
        {
            if (Main.Instance.GetSpawnController.CanSpawn(transform.parent.tag))
            {
                Main.Instance.GetSpawnController.Spawn(_creature.GetComponent<GameObject>(), transform.parent.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Too many creatures allready");
                _isActive = false;
            }
        }

        /// <summary>
        /// Активация заклинания
        /// </summary>
        void ActivateBonus()
        {
        }

        #region Получение переменных
        /// <summary>
        /// Содержание карты
        /// </summary>
        public CardData CardsData
        {
            get { return _cardData; }
            set { _cardData = value; }
        }

        /// <summary>
        /// Существо карты
        /// </summary>
        public Creature CardsCreature
        {
            get { return _creature; }
            set { _creature = value; }
        }

        /// <summary>
        /// Заклинание карты
        /// </summary>
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

        #endregion Для редактора
    }
}