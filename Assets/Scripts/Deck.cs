using System.Collections.Generic;
using UnityEngine;
using DeadLords.Controllers;

namespace DeadLords
{
    public class Deck : MonoBehaviour
    {
        List<Card> cards = new List<Card>();

        /// <summary>
        /// Добавление CardData в колоду
        /// </summary>
        /// <param name="CardData">Добавляемая карта</param>
        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        /// <summary>
        /// Удалить карту из колоды
        /// </summary>
        /// <param name="CardData">Удаляемая карта</param>
        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        /// <summary>
        /// Взять случайную карту из колоды
        /// </summary>
        /// <returns>Взятая карта</returns>
        public Card GrabRandCard()
        {
            int id = Random.Range(0, cards.Count);

            Card card = new Card();

            card = cards[id];

            cards.RemoveAt(id);

            return card;
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