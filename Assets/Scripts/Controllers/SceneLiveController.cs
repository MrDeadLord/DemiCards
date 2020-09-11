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
        private List<Card> _playersDeck, _enemysDeck;
        private List<Card> _playersPlayedCards, _enemysPlayedCards;
        private List<Creature> _playersDeadCr, _enemysDeadCr;

        private int _playersMaxHandSize = 11, _enemysMaxHandSize = 11;

        private int _cardsTake = 2; //Кол-во карт получаемых в начале хода

        /// <summary>
        /// Четное расположение
        /// </summary>
        private List<Button> _cardsEven;
        /// <summary>
        /// Нечетное расположение
        /// </summary>
        private List<Button> _cardsOdd;

        string cardsPathPlayer = Application.dataPath + "/SaveData/Player's Deck.txt";
        string cardsPathEnemy = Application.dataPath + "/SaveData/Enemy's Deck.txt";
        #endregion

        private void Start()
        {
            #region Инициализируем кол-во карт у игрока и врага
            _playersMaxHandSize = Main.Instance.GetObjectManager.Player.GetComponent<BaseStats>().MaxHandSize;
            _playersDeck = Main.Instance.GetObjectManager.Player.GetComponent<Inventory>().CardsDeck;


            _enemysMaxHandSize = Main.Instance.GetObjectManager.Enemy.GetComponent<BaseStats>().MaxHandSize;
            _enemysDeck = Main.Instance.GetObjectManager.Enemy.GetComponent<Inventory>().CardsDeck;
            #endregion

            _cardsEven = Main.Instance.GetObjectManager.GetCardsEven;
            _cardsOdd = Main.Instance.GetObjectManager.GetCardsOdd;

            //Выбор кто будет ходить первым если это начало боя
            if (totalTurns == 0)
            {
                var r = new System.Random();

                if (r.Next(0, 1) == 0)
                    _isPlayersTurn = true;
                else
                    _isPlayersTurn = false;

                if (File.Exists(cardsPathPlayer))
                {
                    for(int i = 0; i < File.ReadAllLines(cardsPathPlayer).Length; i++)
                    {
                        _playersDeck.Add(new Card());
                        _playersDeck[i].CardsData = JsonUtility.FromJson<CardData>(File.ReadAllLines(cardsPathPlayer)[i]);
                    }
                }
            }
            else if (totalTurns != 0)
            {
                //Сюда вписать код загрузки данных из файла сохранения после того как сделаю скрипты сохранения и загрузки
            }
        }

        private void Update()
        {
            if (IsPlayersTurn)
                PlayersTurn();
            else
                EnemysTurn();
        }


        public void EndOfaTurn()
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
        }

        private void EnemysTurn()
        {

        }

        private void PlacingCards()
        {
            switch (PlayersHand.Count)
            {
                case 1:
                    _cardsOdd[5].enabled = true;

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