using DeadLords.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Ход игрока
    /// </summary>
    public class PlayersTurn : BaseController
    {
        Hand _playersHand;
        BaseStats _bs;

        bool _cardsTook = false;
        bool _cardsPlaced = false;

        /// <summary>
        /// Кнопки/места расположения карт
        /// </summary>
        private List<CardsButton> _cardsButtons = new List<CardsButton>();

        #region Unity-time
        private void Start()
        {
            _playersHand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
            _bs = Main.Instance.GetObjectManager.Player.GetComponent<BaseStats>();

            _cardsButtons = Main.Instance.GetObjectManager.GetCardsButtons;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Перестаем выполнять, если коллода еще не загружена
            if (!Main.Instance.deckLoadedPl)
                return;

            //Взятие карт в руку
            if (!_cardsTook)
            {
                _playersHand.TakingCards(_bs.CardsTake);
                _cardsTook = true;
                Debug.Log("Cards collected");
            }

            //Расположение карт в интерфейсе
            if (!_cardsPlaced)
            {
                PlacingCards();
                _cardsPlaced = true;
                Debug.Log("Cards placed");
            }

            if (_cardsTook && _cardsPlaced)
                Off();
        }
        #endregion

        /// <summary>
        /// Расстановка карт в интерфейс для игрока
        /// </summary>
        void PlacingCards()
        {
            switch (_playersHand.Cards.Count)
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

            InitCards();
        }

        void InitCards()
        {
            int i = 0;  //Счетчик очередности

            foreach (CardsButton cb in _cardsButtons)
            {
                if (cb.Enabled)
                {
                    cb.GetComponent<Card>().Assignment(_playersHand.Cards[i]);

                    i++;
                }
            }
        }
    }
}