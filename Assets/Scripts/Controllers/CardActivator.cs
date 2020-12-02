using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class CardActivator : BaseController
    {
        #region Переменные
        [SerializeField] Hand _hand;
        [SerializeField] [Tooltip("модель, что будет всплывать во время предактивации карты")] ActCard _actCard;
        [SerializeField] [Tooltip("0-старт, 1-союзн., 2-враги, 3-все")] Transform[] _cameraPositions;
        
        Transform _camera;
        //Действующие лица и приложенные к ним эффекты
        GameObject _player, _enemy;
        GameObject[] _playerCr, _enemyCr;
        //Animator _actCardAnimator;
        SpawnController _sc;
        #endregion

        #region Unity-time
        private void Start()
        {
            _sc = Main.Instance.GetSpawnController;
            _camera = Camera.main.transform;
        }

        private void Update()
        {
            if (!Enabled)
                return;


        }
        #endregion

        public override void On()
        {
            Enabled = true;

            _actCard.On();

            //Запуск спецэффектов

            PlaceCam();
            EffectsStart();
        }

        /// <summary>
        /// Расположение камеры
        /// </summary>
        public void PlaceCam()
        {
            //Расположение камеры
            if (_actCard.Card.CardsCreature != null
                || _actCard.Card.CardsBonus.target == TargetType.allSummonerCreatures
                || _actCard.Card.CardsBonus.target == TargetType.summoner
                || _actCard.Card.CardsBonus.target == TargetType.summonerCreature)
                _camera = _cameraPositions[1];
            else if (_actCard.Card.CardsBonus.target == TargetType.opponent
                || _actCard.Card.CardsBonus.target == TargetType.opponentCreature
                || _actCard.Card.CardsBonus.target == TargetType.allOpponentCreatures)
                _camera = _cameraPositions[2];
            else if (_actCard.Card.CardsBonus.target == TargetType.allCreatures
                || _actCard.Card.CardsBonus.target == TargetType.allPlayers
                || _actCard.Card.CardsBonus.target == TargetType.everyone)
                _camera = _cameraPositions[3];
        }

        public void EffectsStart()
        {
            switch (_actCard.Card.CardsBonus.target)
            {
                case TargetType.allCreatures:
                    
                    break;
            }
        }

        /// <summary>
        /// Призыв существа
        /// </summary>
        void Summon(Card card)
        {
            if (_sc.CanSpawn(transform.parent.tag))
            {
                _sc.Spawn(card.CardsCreature.gameObject, transform.parent.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Too many creatures allready");
            }
        }

        /// <summary>
        /// Активация заклинания
        /// </summary>
        void ActivateBonus()
        {

        }
    }
}