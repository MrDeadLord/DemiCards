using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    public class Inventory : MonoBehaviour
    {
        #region Переменные
        private Item[] _items;  //Все вещи в инвентаре
        [SerializeField] private List<Card> _cards;  //Все карты, что есть у игрока

        /// <summary>
        /// Надетые вещи
        /// </summary>
        private Item[] _wearedItems;
        /// <summary>
        /// Активная коллода
        /// </summary>
        [SerializeField] private List<Card> _cardsDeck;
        #endregion


        #region Получение данных
        /// <summary>
        /// Надетые вещи
        /// </summary>
        public Item[] WearedItems
        {
            get { return _wearedItems; }
            set { _wearedItems = value; }
        }
        /// <summary>
        /// Активная коллода
        /// </summary>
        public List<Card> CardsDeck
        {
            get { return _cardsDeck; }
            set { _cardsDeck = value; }
        }
        #endregion
    }
}