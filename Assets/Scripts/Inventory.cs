using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    public class Inventory : MonoBehaviour
    {
        private Item[] _items;  //Все вещи в инвентаре
        [SerializeField] [Tooltip("Оставшиеся в инвентаре карты")] private List<Card> _cardsLeft;

        /// <summary>
        /// Надетые вещи
        /// </summary>
        private Item[] _wearedItems;
    }
}