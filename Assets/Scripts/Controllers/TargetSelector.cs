using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class TargetSelector : BaseController
    {
        #region Переменные
        [SerializeField] Hand _hand;
        [SerializeField] [Tooltip("модель, что будет всплывать во время предактивации карты")] ActCard _actCard;
        [SerializeField] [Tooltip("0-старт, 1-союзн., 2-враги, 3-все")] Transform[] _cameraPositions;

        Transform _camera;
        //Действующие лица и приложенные к ним эффекты
        Selector _playerSel, _enemySel;
        List<Selector> _crPlayerSel = new List<Selector>();
        List<Selector> _crEnemySel = new List<Selector>();
        List<Selector> _spawnPointsPlSels = new List<Selector>();
        #endregion

        #region Unity-time
        private void Start()
        {
            _camera = Camera.main.transform;

            _playerSel = Main.Instance.GetObjectManager.Player.GetComponent<Selector>();
            _enemySel = Main.Instance.GetObjectManager.Enemy.GetComponent<Selector>();

            Init();
        }
        #endregion

        public override void On()
        {
            Enabled = true;

            _actCard.On();

            PlaceCam();
            EnableSelection();

            //Запуск спецэффектов
        }

        public override void Off()
        {
            base.Off();

            _actCard.Off();

            Cancel();
        }

        /// <summary>
        /// Инициализация. Присвоение актуальных живых существ и активных точек спауна
        /// </summary>
        public void Init()
        {
            foreach (Creature cr in Main.Instance.GetObjectManager.CrPlayer)
                _crPlayerSel.Add(cr.GetComponentInChildren<Selector>());

            foreach (Creature cr in Main.Instance.GetObjectManager.CrEnemy)
                _crEnemySel.Add(cr.GetComponentInChildren<Selector>());

            foreach (Selector sel in Main.Instance.GetObjectManager.PlayerSpawnPoints.GetComponentsInChildren<Selector>())
                _spawnPointsPlSels.Add(sel);
        }

        /// <summary>
        /// Расположение камеры
        /// </summary>
        void PlaceCam()
        {
            //Расположение камеры
            if (_actCard.Card.CardsCreature != null
                || _actCard.Card.CardsBonus.target == TargetType.allSummonerCreatures
                || _actCard.Card.CardsBonus.target == TargetType.summoner
                || _actCard.Card.CardsBonus.target == TargetType.summonerCreature)
            {
                _camera.position = _cameraPositions[1].position;
                _camera.rotation = _cameraPositions[1].rotation;
            }
            else if (_actCard.Card.CardsBonus.target == TargetType.opponent
                || _actCard.Card.CardsBonus.target == TargetType.opponentCreature
                || _actCard.Card.CardsBonus.target == TargetType.allOpponentCreatures)
            {
                _camera.position = _cameraPositions[2].position;
                _camera.rotation = _cameraPositions[2].rotation;
            }
            else if (_actCard.Card.CardsBonus.target == TargetType.allCreatures
                || _actCard.Card.CardsBonus.target == TargetType.allPlayers
                || _actCard.Card.CardsBonus.target == TargetType.everyone)
            {
                _camera.position = _cameraPositions[3].position;
                _camera.rotation = _cameraPositions[3].rotation;
            }
            else
            {
                _camera.position = _cameraPositions[0].position;
                _camera.rotation = _cameraPositions[0].rotation;
            }
        }

        /// <summary>
        /// Включение необходимых выделений
        /// </summary>
        void EnableSelection()
        {
            //Если вызов существа - подсвечиваем возможные точки спауна
            if (_actCard.Card.CardsCreature != null)
            {
                foreach (Selector sel in _spawnPointsPlSels)
                    sel.On();

                return;
            }

            //Если заклинание - подсвечиваем возможную цель. Если целей несколько, сразу же выбирает их
            switch (_actCard.Card.CardsBonus.target)
            {
                case TargetType.everyone:
                    _enemySel.On();
                    _enemySel.Animator.SetBool("Selected", true);

                    _playerSel.Animator.SetBool("Selected", true);

                    foreach (Selector sel in _crPlayerSel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }

                    foreach (Selector sel in _crEnemySel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }
                    break;

                case TargetType.allCreatures:
                    foreach (Selector sel in _crPlayerSel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }

                    foreach (Selector sel in _crEnemySel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }
                    break;

                case TargetType.allSummonerCreatures:
                    foreach (Selector sel in _crPlayerSel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }
                    break;

                case TargetType.summonerCreature:
                    foreach (Selector sel in _crPlayerSel)
                        sel.On();
                    break;

                case TargetType.allOpponentCreatures:
                    foreach (Selector sel in _crEnemySel)
                    {
                        sel.On();
                        sel.Animator.SetBool("Selected", true);
                    }
                    break;

                case TargetType.opponentCreature:
                    foreach (Selector sel in _crEnemySel)
                        sel.On();
                    break;

                //При воздействии на обоих игроков, активируем только врага, т.к. у игрока кативируется в начале хода
                case TargetType.allPlayers:
                    _enemySel.On();
                    _enemySel.Animator.SetBool("Selected", true);

                    _playerSel.Animator.SetBool("Selected", true);
                    break;

                case TargetType.opponent:
                    _enemySel.On();
                    _enemySel.Animator.SetBool("Selected", true);
                    break;

                case TargetType.summoner:
                    _playerSel.Animator.SetBool("Selected", true);
                    break;
            }
        }

        /// <summary>
        /// Отмена действия карты. Возврат камеры и карт на места
        /// </summary>
        void Cancel()
        {
            //Размещение камеры на стартовой позиции
            _camera.position = _cameraPositions[0].position;
            _camera.rotation = _cameraPositions[0].rotation;

            _playerSel.Animator.SetBool("Selected", false);

            _enemySel.Off();

            foreach (Selector sel in _spawnPointsPlSels)
                sel.Off();

            foreach (Selector sel in _crPlayerSel)
                sel.Off();

            foreach (Selector sel in _crEnemySel)
                sel.Off();
        }
    }
}