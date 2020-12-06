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
        /// Кнопки/места расположения карт
        /// </summary>
        List<CardsButton> _cardsButtons = new List<CardsButton>();

        private void Start() { _cardsButtons = Main.Instance.GetObjectManager.GetCardsButtons; }

        private void Update()
        {
            Debug.Log(_hand.Count);
        }

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

            PlacingCards();
        }
                
        /// <summary>
        /// Расстановка карт в интерфейс для игрока
        /// </summary>
        void PlacingCards()
        {
            //Отключение всех кнопок карт
            foreach (CardsButton cb in _cardsButtons)
                cb.Off();

            //Включение нужных карт по кол-ву
            switch (_hand.Count)
            {
                case 1:
                    _cardsButtons[5].On();
                    break;

                case 2:
                    _cardsButtons[3].On();
                    _cardsButtons[7].On();
                    break;

                case 3:
                    _cardsButtons[3].On();
                    _cardsButtons[5].On();
                    _cardsButtons[7].On();
                    break;

                case 4:
                    _cardsButtons[2].On();
                    _cardsButtons[4].On();
                    _cardsButtons[6].On();
                    _cardsButtons[8].On();
                    break;

                case 5:
                    _cardsButtons[1].On();
                    _cardsButtons[3].On();
                    _cardsButtons[5].On();
                    _cardsButtons[7].On();
                    _cardsButtons[9].On();
                    break;

                case 6:
                    _cardsButtons[0].On();
                    _cardsButtons[2].On();
                    _cardsButtons[4].On();
                    _cardsButtons[6].On();
                    _cardsButtons[8].On();
                    _cardsButtons[10].On();
                    break;

                case 7:
                    _cardsButtons[2].On();
                    _cardsButtons[3].On();
                    _cardsButtons[4].On();
                    _cardsButtons[5].On();
                    _cardsButtons[6].On();
                    _cardsButtons[7].On();
                    _cardsButtons[8].On();
                    break;

                case 8:
                    _cardsButtons[1].On();
                    _cardsButtons[2].On();
                    _cardsButtons[3].On();
                    _cardsButtons[4].On();
                    _cardsButtons[5].On();
                    _cardsButtons[6].On();
                    _cardsButtons[8].On();
                    _cardsButtons[11].On();
                    break;

                case 9:
                    _cardsButtons[1].On();
                    _cardsButtons[2].On();
                    _cardsButtons[3].On();
                    _cardsButtons[4].On();
                    _cardsButtons[5].On();
                    _cardsButtons[6].On();
                    _cardsButtons[7].On();
                    _cardsButtons[8].On();
                    _cardsButtons[9].On();
                    break;

                case 10:
                    _cardsButtons[0].On();
                    _cardsButtons[2].On();
                    _cardsButtons[3].On();
                    _cardsButtons[4].On();
                    _cardsButtons[5].On();
                    _cardsButtons[6].On();
                    _cardsButtons[7].On();
                    _cardsButtons[8].On();
                    _cardsButtons[9].On();
                    _cardsButtons[10].On();
                    break;

                case 11:
                    _cardsButtons[0].On();
                    _cardsButtons[1].On();
                    _cardsButtons[2].On();
                    _cardsButtons[3].On();
                    _cardsButtons[4].On();
                    _cardsButtons[5].On();
                    _cardsButtons[6].On();
                    _cardsButtons[7].On();
                    _cardsButtons[8].On();
                    _cardsButtons[9].On();
                    _cardsButtons[10].On();
                    _cardsButtons[11].On();
                    break;
            }

            //Присвоение карт к кнопкам
            InitCards();
        }

        /// <summary>
        /// Присвоение карт
        /// </summary>
        void InitCards()
        {
            int i = 0;  //Счетчик очередности

            foreach (CardsButton cb in _cardsButtons)
            {
                if (cb.Enabled)
                {
                    cb.GetComponent<Card>().Assignment(_hand[i]);

                    i++;
                }
            }
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