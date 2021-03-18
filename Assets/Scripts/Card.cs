using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Класс карты, где хранится вся информация о ней. Присваивается к UI.button
    /// </summary>
    public class Card : MonoBehaviour
    {
        #region ========== Variables ========

        CardData _cardData = new CardData();

        /// <summary>
        /// Полное содержание бонуса(если карта - заклинание)
        /// </summary>
        private BonusData _cardsBonus = new BonusData();

        /// <summary>
        /// Существо, если это призыв. Иначе - null
        /// </summary>
        private Creature _creature = new Creature();

        #endregion ========== Variables ========

        #region ========== Methods ========

        public void Enable(Card card)
        {
            gameObject.SetActive(true);

            Assignment(card);

            // Присвоение материала карте
            foreach (var mat in Main.Instance.GetObjectManager.GetCardsMaterials)
            {
                if(_cardData.cardName == mat.name)
                {
                    GetComponent<Renderer>().material = mat;
                    return;
                }
            }                     
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

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

        #endregion ========== Methods ========

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
        #endregion Для редактора
    }
}