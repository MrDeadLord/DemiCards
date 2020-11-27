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
        /// Добавление карт на руку из коллоды
        /// </summary>
        /// <param name="count">Кол-во карт</param>
        public void TakingCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                //Завершение выполнения, если колода закончилась
                if (GetComponent<Deck>().Cards.Count == 0)
                    return;

                _hand.Add(GetComponent<Deck>().GrabRandCard());  //Добавление на руку
            }
        }

        /// <summary>
        /// Удаление карты из рук
        /// </summary>
        /// <param name="card">Какую карту удаляем</param>
        public void RemoveCard(Card card)
        {
            _hand.Remove(card);
        }

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