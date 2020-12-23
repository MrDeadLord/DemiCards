using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Загрузка активной колоды
    /// </summary>
    public class LoadDeck : MonoBehaviour
    {
        /// <summary>
        /// Путь к коллоде
        /// </summary>
        private string pathDeck;
        /// <summary>
        /// Временная карта для загрузки
        /// </summary>
        private Card tempCard;
        /// <summary>
        /// Активная коллода
        /// </summary>
        private List<Card> tempDeck = new List<Card>();

        private void Start()
        {
            //Определение какую колоду нужно загрузить
            if (gameObject.tag == "Player")
            {
                Main.Instance.deckLoadedPl = false;
                pathDeck = Application.dataPath + "/SaveData/Player's Deck.txt";
            }
            else
            {
                Main.Instance.deckLoadedEn = false;
                pathDeck = Application.dataPath + "/SaveData/Enemy's Deck.txt";
            }

            Load();
        }

        /// <summary>
        /// Загрузка и присвоение карте всех переменных, кроме имени самой карты
        /// </summary>
        void Load()
        {
            if (File.Exists(pathDeck))
            {
                int i = 0;  //Id for cards

                foreach (string line in File.ReadAllLines(pathDeck))
                {
                    //Загрузка базовой информации карты
                    CardData ca = new CardData();
                    
                    //Creating new Card with uniq id
                    tempCard = new Card();                    
                    tempCard.id = i;
                    i++;

                    ca = JsonUtility.FromJson<CardData>(line);
                    tempCard.CardsData = ca;

                    //Получение бонуса или существа
                    if (ca.creatureName == string.Empty)
                    {
                        tempCard.CardsBonus = BonusConvert(ca.cardsBonusIndex);
                    }
                    else
                    {
                        foreach (Creature cr in Main.Instance.GetObjectManager.GetCreatures)
                        {                            
                            if (ca.creatureName == cr.name)
                            {
                                tempCard.CardsCreature = cr;
                                break;
                            }
                        }
                    }
                    
                    tempDeck.Add(tempCard);
                }

                //Добавление загруженой колоды непосредственно игроку и врагу
                if (gameObject.tag == "Player")
                {
                    GetComponent<Deck>().Cards = tempDeck;                    
                    Main.Instance.deckLoadedPl = true;
                }
                else
                {
                    GetComponent<Deck>().Cards = tempDeck;
                    Main.Instance.deckLoadedEn = true;
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
    }
}