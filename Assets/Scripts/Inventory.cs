using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    public class Inventory : MonoBehaviour
    {
        private Item[] _items;  //Все вещи в инвентаре
        private List<Card> _cards;  //Все карты, что есть у игрока

        private Item[] _wearedItems;    //Надетые вещи
        private List<Card> _cardsDeck;  //Активная коллода

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