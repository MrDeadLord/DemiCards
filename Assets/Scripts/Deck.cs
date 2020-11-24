using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    public class Deck : MonoBehaviour
    {
        List<Card> cards = new List<Card>();

        /// <summary>
        /// Добавление card в колоду
        /// </summary>
        /// <param name="card">Добавляемая карта</param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Удалить карту из колоды
        /// </summary>
        /// <param name="card">Удаляемая карта</param>
        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        /// <summary>
        /// Непосредственно сама колода
        /// </summary>
        public List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }
    }
}