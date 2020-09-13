using System.Collections.Generic;
using System.IO;
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
        private List<Card> _cardsDeck;

        /// <summary>
        /// Путь к коллоде
        /// </summary>
        private string pathDeck;
        #endregion

        private void Awake()
        {
            if(gameObject.tag == "Player")
            {
                pathDeck = Application.dataPath + "/SaveData/Player's Deck.txt";
            }
            else
            {
                pathDeck = Application.dataPath + "/SaveData/Enemy's Deck.txt";
            }

            LoadDeck();
        }

        private void LoadDeck()
        {
            //Вытягивание карт из файла
            if (File.Exists(pathDeck))
            {
                for (int i = 0; i < File.ReadAllLines(pathDeck).Length; i++)
                {                    
                    var card = new Card();  //Создание новой карты
                    
                    CardData ca = new CardData();
                    ca = JsonUtility.FromJson<CardData>(File.ReadAllLines(pathDeck)[i]);
                    card.CardsData = ca;
                    Debug.Log(ca.cardName);
                    //Получение бонуса или существа
                    if (ca.creatureName == string.Empty)
                    {
                        card.CardsBonus = BonusConvert(ca.cardsBonusIndex);
                    }
                    else
                    {
                        //var creatures = new List<Creature>();
                        var creatures = Main.Instance.GetAllCreatures.crList;   //Список всех существ(Добавляется через редактор)

                        //В каждом существе ищем нужное нам имя
                        foreach (Creature cr in creatures)
                        {
                            //Если имя совпало - присваиваем карте существо
                            if (cr.name == ca.creatureName)
                            {
                                card.CardsCreature = cr;
                                return;
                            }                                
                        }
                    }

                    _cardsDeck.Add(card);   //Добавление готовой карты
                }
            }
        }

        /// <summary>
        /// Получение бонуса из id
        /// </summary>
        /// <param name="id">Bonus id</param>
        /// <returns></returns>
        private BonusData BonusConvert(int id)
        {
            string path = Application.dataPath + "/Bonuses.txt";

            var bd = new BonusData();

            bd = JsonUtility.FromJson<BonusData>(File.ReadAllLines(path)[id]);

            return bd;
        }

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