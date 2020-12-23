using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Класс карты, где хранится вся информация о ней. Присваивается к UI.button
    /// </summary>
    public class Card : MonoBehaviour
    {
        #region Переменные
        CardData _cardData = new CardData();

        /// <summary>
        /// Полное содержание бонуса(если карта - заклинание)
        /// </summary>
        BonusData _cardsBonus = new BonusData();

        /// <summary>
        /// Существо, если это призыв. Иначе - null
        /// </summary>
        Creature _creature = new Creature();

        public int id { get; set; }
        #endregion

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

        #region Чтобы работало сравнение
        public override int GetHashCode()
        {
            return id;
        }

        public override bool Equals(object other)
        {
            if (other == null || !(other is Card))
                return false;
            else
                return GetHashCode() == ((Card)other).GetHashCode();
        }
        #endregion Чтобы работало сравнение

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