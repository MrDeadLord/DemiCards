using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Карты на руках игрока. Операции с ними
    /// </summary>
    public class Hand : MonoBehaviour
    {
        /// <summary>
        /// Карты на руке игрока
        /// </summary>
        List<Card> _hand = new List<Card>();

        /// <summary>
        /// Карты видные игроку
        /// </summary>
        List<Card> _handVisual = new List<Card>();

        #region ========== Methods ========

        #region ========== Unity-time ========
        
        private void Start()
        {
            _handVisual = Main.Instance.GetObjectManager.PlayersHand;
        }

        #endregion ========== Unity-time ========

        /// <summary>
        /// Добавление карт на руку из колоды
        /// </summary>
        /// <param name="count">Кол-во карт</param>
        public void TakingCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // Завершение выполнения, если колода закончилась
                if (GetComponent<Deck>().Cards.Count == 0)
                    return;

                // Добавление карты из коллоды на руку
                _hand.Add(GetComponent<Deck>().GrabRandCard());
            }

            PlacingCards();
        }

        /// <summary>
        /// Расстановка карт в интерфейс для игрока
        /// </summary>
        void PlacingCards()
        {
            // Отключение всех кнопок карт
            foreach (Card card in _handVisual)
                card.Disable();

            // Включение нужных карт по кол-ву и присвоение им карт
            switch (_hand.Count)
            {
                case 1:
                    _handVisual[5].Enable(_hand[0]);
                    break;

                case 2:
                    _handVisual[2].Enable(_hand[0]);
                    _handVisual[6].Enable(_hand[1]);
                    break;

                case 3:
                    _handVisual[1].Enable(_hand[0]);
                    _handVisual[4].Enable(_hand[1]);
                    _handVisual[7].Enable(_hand[2]);
                    break;

                case 4:
                    _handVisual[0].Enable(_hand[0]);
                    _handVisual[2].Enable(_hand[1]);
                    _handVisual[6].Enable(_hand[2]);
                    _handVisual[8].Enable(_hand[3]);
                    break;

                case 5:
                    _handVisual[0].Enable(_hand[0]);
                    _handVisual[2].Enable(_hand[1]);
                    _handVisual[4].Enable(_hand[2]);
                    _handVisual[6].Enable(_hand[3]);
                    _handVisual[8].Enable(_hand[4]);
                    break;

                case 6:
                    _handVisual[2].Enable(_hand[0]);
                    _handVisual[3].Enable(_hand[1]);
                    _handVisual[4].Enable(_hand[2]);
                    _handVisual[5].Enable(_hand[3]);
                    _handVisual[6].Enable(_hand[4]);
                    _handVisual[7].Enable(_hand[5]);
                    break;

                case 7:
                    _handVisual[1].Enable(_hand[0]);
                    _handVisual[2].Enable(_hand[1]);
                    _handVisual[3].Enable(_hand[2]);
                    _handVisual[4].Enable(_hand[3]);
                    _handVisual[5].Enable(_hand[4]);
                    _handVisual[6].Enable(_hand[5]);
                    _handVisual[7].Enable(_hand[6]);
                    break;

                case 8:
                    _handVisual[1].Enable(_hand[0]);
                    _handVisual[2].Enable(_hand[1]);
                    _handVisual[3].Enable(_hand[2]);
                    _handVisual[4].Enable(_hand[3]);
                    _handVisual[5].Enable(_hand[4]);
                    _handVisual[6].Enable(_hand[5]);
                    _handVisual[7].Enable(_hand[6]);
                    _handVisual[8].Enable(_hand[7]);
                    break;

                case 9:
                    _handVisual[0].Enable(_hand[0]);
                    _handVisual[1].Enable(_hand[1]);
                    _handVisual[2].Enable(_hand[2]);
                    _handVisual[3].Enable(_hand[3]);
                    _handVisual[4].Enable(_hand[4]);
                    _handVisual[5].Enable(_hand[5]);
                    _handVisual[6].Enable(_hand[6]);
                    _handVisual[7].Enable(_hand[7]);
                    _handVisual[8].Enable(_hand[8]);
                    break;
            }
        }               

        #endregion ========== Methods ========

        #region Получение переменных

        /// <summary>
        /// Карты на руке игрока
        /// </summary>
        public List<Card> Cards
        {
            get { return _hand; }
            set { _hand = value; }
        }

        #endregion
    }
}