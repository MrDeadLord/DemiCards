using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeadLords;
using System.IO;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Очередность ходов, время хода, активация подбора карт и т.д.
    /// </summary>
    public class SceneLiveController : BaseController
    {
        #region Переменные
        public int totalTurns = 0;

        private bool _isPlayersTurn;    //true если ходит игрок, иначе - противник

        private List<Card> _playersHand, _enemysHand;
        private List<Card> _playersDeck = new List<Card>();
        private List<Card> _enemysDeck = new List<Card>();
        private List<Card> _playersPlayedCards, _enemysPlayedCards;
        private List<Creature> _playersDeadCr, _enemysDeadCr;

        private int _playersMaxHandSize = 11, _enemysMaxHandSize = 11;

        private int _cardsTake = 2; //Кол-во карт получаемых в начале хода

        /// <summary>
        /// Кнопки/места расположения карт
        /// </summary>
        private List<Button> _cardsButtons;

        /// <summary>
        /// Список карт в кнопках
        /// </summary>
        private List<Card> _cards;
        #endregion

        private void Start()
        {
            #region Инициализируем кол-во карт у игрока и врага
            _playersMaxHandSize = Main.Instance.GetObjectManager.Player.GetComponent<BaseStats>().MaxHandSize;
            _playersDeck = Main.Instance.GetObjectManager.Player.GetComponent<Inventory>().CardsDeck;


            _enemysMaxHandSize = Main.Instance.GetObjectManager.Enemy.GetComponent<BaseStats>().MaxHandSize;
            _enemysDeck = Main.Instance.GetObjectManager.Enemy.GetComponent<Inventory>().CardsDeck;
            #endregion

            _cardsButtons = Main.Instance.GetObjectManager.GetCardsButtons;

            foreach(Button butt in _cardsButtons)
            {
                _cards.Add(butt.GetComponent<Card>());
            }

            //Выбор кто будет ходить первым если это начало боя
            if (totalTurns == 0)
            {
                var r = new System.Random();

                if (r.Next(0, 1) == 0)
                    _isPlayersTurn = true;
                else
                    _isPlayersTurn = false;
            }
            else if (totalTurns != 0)
            {
                //Сюда вписать код загрузки данных из файла сохранения после того как сделаю скрипты сохранения и загрузки
            }
        }

        private void Update()
        {
            if (!Enabled)
                return;

            if (IsPlayersTurn)
                PlayersTurn();
            else
                EnemysTurn();
        }


        public void EndOfTurn()
        {
            totalTurns++;
            _isPlayersTurn = !_isPlayersTurn;
        }

        private void PlayersTurn()
        {
            for (int i = 0; i < _cardsTake; i++)    //Берем то кол-во карт, которое допускает _cardsTake
            {
                int n = (int)Random.Range(0, _playersDeck.Count);   //Случайное число в пределах колоды
                _playersHand.Add(_playersDeck[n]);  //Добавление карты из колоды в руку
                _playersDeck.RemoveAt(n);   //Удаление карты из коллоды
            }

            PlacingCards();



            base.Off();
        }

        private void EnemysTurn()
        {
            for (int i = 0; i < _cardsTake; i++)    //Берем то кол-во карт, которое допускает _cardsTake
            {
                int n = (int)Random.Range(0, _enemysDeck.Count);   //Случайное число в пределах колоды
                _enemysHand.Add(_enemysDeck[n]);  //Добавление карты из колоды в руку
                _enemysDeck.RemoveAt(n);   //Удаление карты из коллоды
            }

            //Здесь прописать ИИ врага

            EndOfTurn();
            base.Off();
        }

        /// <summary>
        /// Расстановка карт в интерфейс для игрока
        /// </summary>
        private void PlacingCards()
        {
            switch (_playersHand.Count)
            {
                case 1:
                    _cardsButtons[5].enabled = true;
                    _cards[5] = _playersHand[0];
                    break;
                case 2:
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[7].enabled = true;

                    _cards[3] = _playersHand[0];
                    _cards[7] = _playersHand[1];
                    break;
                case 3:
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[7].enabled = true;

                    _cards[3] = _playersHand[0];
                    _cards[5] = _playersHand[1];
                    _cards[7] = _playersHand[2];
                    break;
                case 4:
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[8].enabled = true;

                    _cards[2] = _playersHand[0];
                    _cards[4] = _playersHand[1];
                    _cards[6] = _playersHand[2];
                    _cards[8] = _playersHand[3];
                    break;
                case 5:
                    _cardsButtons[1].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[7].enabled = true;
                    _cardsButtons[9].enabled = true;

                    _cards[1] = _playersHand[0];
                    _cards[3] = _playersHand[1];
                    _cards[5] = _playersHand[2];
                    _cards[7] = _playersHand[3];
                    _cards[9] = _playersHand[4];
                    break;
                case 6:
                    _cardsButtons[0].enabled = true;
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[8].enabled = true;
                    _cardsButtons[10].enabled = true;

                    _cards[0] = _playersHand[0];
                    _cards[2] = _playersHand[1];
                    _cards[4] = _playersHand[2];
                    _cards[6] = _playersHand[3];
                    _cards[8] = _playersHand[4];
                    _cards[10] = _playersHand[5];
                    break;                
                case 7:
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[7].enabled = true;
                    _cardsButtons[8].enabled = true;

                    _cards[2] = _playersHand[0];
                    _cards[3] = _playersHand[1];
                    _cards[4] = _playersHand[2];
                    _cards[5] = _playersHand[3];
                    _cards[6] = _playersHand[4];
                    _cards[7] = _playersHand[5];
                    _cards[8] = _playersHand[6];
                    break;
                case 8:
                    _cardsButtons[1].enabled = true;
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[8].enabled = true;
                    _cardsButtons[11].enabled = true;

                    _cards[1] = _playersHand[0];
                    _cards[2] = _playersHand[1];
                    _cards[3] = _playersHand[2];
                    _cards[4] = _playersHand[3];
                    _cards[5] = _playersHand[4];
                    _cards[6] = _playersHand[5];
                    _cards[8] = _playersHand[6];
                    _cards[11] = _playersHand[7];
                    break;
                case 9:
                    _cardsButtons[1].enabled = true;
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[7].enabled = true;
                    _cardsButtons[8].enabled = true;
                    _cardsButtons[9].enabled = true;

                    _cards[1] = _playersHand[0];
                    _cards[2] = _playersHand[1];
                    _cards[3] = _playersHand[2];
                    _cards[4] = _playersHand[3];
                    _cards[5] = _playersHand[4];
                    _cards[6] = _playersHand[5];
                    _cards[7] = _playersHand[6];
                    _cards[8] = _playersHand[7];
                    _cards[9] = _playersHand[8];
                    break;
                case 10:
                    _cardsButtons[0].enabled = true;
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[7].enabled = true;
                    _cardsButtons[8].enabled = true;
                    _cardsButtons[9].enabled = true;
                    _cardsButtons[10].enabled = true;

                    _cards[0] = _playersHand[0];
                    _cards[2] = _playersHand[1];
                    _cards[3] = _playersHand[2];
                    _cards[4] = _playersHand[3];
                    _cards[5] = _playersHand[4];
                    _cards[6] = _playersHand[5];
                    _cards[7] = _playersHand[6];
                    _cards[8] = _playersHand[7];
                    _cards[9] = _playersHand[8];
                    _cards[10] = _playersHand[9];
                    break;
                case 11:
                    _cardsButtons[0].enabled = true;
                    _cardsButtons[1].enabled = true;
                    _cardsButtons[2].enabled = true;
                    _cardsButtons[3].enabled = true;
                    _cardsButtons[4].enabled = true;
                    _cardsButtons[5].enabled = true;
                    _cardsButtons[6].enabled = true;
                    _cardsButtons[7].enabled = true;
                    _cardsButtons[8].enabled = true;
                    _cardsButtons[9].enabled = true;
                    _cardsButtons[10].enabled = true;
                    _cardsButtons[11].enabled = true;

                    _cards[0] = _playersHand[0];
                    _cards[1] = _playersHand[1];
                    _cards[2] = _playersHand[2];
                    _cards[3] = _playersHand[3];
                    _cards[4] = _playersHand[4];
                    _cards[5] = _playersHand[5];
                    _cards[6] = _playersHand[6];
                    _cards[7] = _playersHand[7];
                    _cards[8] = _playersHand[8];
                    _cards[9] = _playersHand[9];
                    _cards[10] = _playersHand[9];
                    _cards[11] = _playersHand[10];
                    break;
            }
        }

        public bool IsPlayersTurn
        {
            get { return _isPlayersTurn; }
            set { _isPlayersTurn = value; }
        }

        //Карты на руках
        public List<Card> PlayersHand { get { return _playersHand; } }
        public List<Card> EnemysHand { get { return _enemysHand; } }

        //Сыграные карты
        public List<Card> PlayersPlayedCards { get { return _playersPlayedCards; } }
        public List<Card> EnemysPlayedCards { get { return _enemysPlayedCards; } }

        //Мертвые существа
        public List<Creature> PlayersDeadCr { get { return _playersDeadCr; } }
        public List<Creature> EnemysDeadCr { get { return _enemysDeadCr; } }

        //Коллоды
        public List<Card> PlaeyrsDeck
        {
            get { return _playersDeck; }
            set { _playersDeck = value; }
        }
        public List<Card> EnemysDeck
        {
            get { return _enemysDeck; }
            set { _enemysDeck = value; }
        }
    }
}