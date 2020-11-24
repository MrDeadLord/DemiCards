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

        List<Card> _deck = new List<Card>();

        /// <summary>
        /// Добавление карт на руку из коллоды
        /// </summary>
        /// <param name="count">Кол-во карт</param>
        public void TakingCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _deck = GetComponent<Deck>().Cards;  //Обновление колоды

                //Завершение выполнения, если колода закончилась
                if (_deck.Count == 0)
                    return;

                int r = Random.Range(0, _deck.Count);

                _hand.Add(_deck[r]);  //Добавление на руку

                GetComponent<Deck>().RemoveCard(_deck[r]);   //Удаление из колоды
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