using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Очередность ходов, время хода, активация подбора карт и т.д.
    /// </summary>
    public class SceneLiveController : BaseController
    {
        #region Переменные

        [SerializeField] int _totalTurns = 0;

        bool _isPlayersTurn;    //true если ходит игрок, иначе - противник
        bool _firstRound = true;   //Флаг первого раунда
        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            On();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            if (Main.Instance.deckLoadedPl && _firstRound)
                Init();
            else if (!Main.Instance.deckLoadedPl && _firstRound)
                return;

            if (_isPlayersTurn && Main.Instance.deckLoadedPl)
            {
                Main.Instance.GetPlayersTurn.On();
                base.Off();
            }
            else if (!_isPlayersTurn && Main.Instance.deckLoadedEn)
            {
                Debug.Log("Enemy's turn");  //Здесь же будет base.Off() и контроль будет передаваться ИИ
                EndOfTurn();
            }
        }
        #endregion

        /// <summary>
        /// Определение кто будет ходить(Сейчас, при загрузке уровня)
        /// </summary>
        void Init()
        {
            //Выбор кто будет ходить первым если это начало боя
            if (_totalTurns == 0)
            {
                if (Random.Range(0, 1) == 0)
                    _isPlayersTurn = true;
                else
                    _isPlayersTurn = false;
            }
            else if (_totalTurns != 0)
            {
                //Сюда вписать код загрузки данных из файла сохранения после того как сделаю скрипты сохранения и загрузки
            }

            _firstRound = false;
        }

        /// <summary>
        /// Завершение хода. TotalTurns++
        /// </summary>
        public void EndOfTurn()
        {
            _totalTurns++;
            _isPlayersTurn = !_isPlayersTurn;

            base.On();
        }

        #region Получение перменных извне
        public bool IsPlayersTurn
        {
            get { return _isPlayersTurn; }
            set { _isPlayersTurn = value; }
        }

        public int TotalTurns
        {
            get { return _totalTurns; }
            set { _totalTurns = value; }
        }
        #endregion
    }
}