using DeadLords.Controllers;
using DeadLords.Helpers;
using UnityEngine;

namespace DeadLords
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        /// <summary>
        /// Флаг того, что колода игрока загружена
        /// </summary>
        public bool deckLoadedPl { get; set; }
        /// <summary>
        /// Флаг того, что колода врага загружена
        /// </summary>
        public bool deckLoadedEn { get; set; }

        private GameObject _controllers;
        private InputController _inputController;
        private ObjectManager _objectManager;
        private SpawnController _spawnController;
        private SceneLiveController _sceneLiveController;
        private PlayersTurn _playersTurn;
        private TargetSelector _TargetSelector;

        private void Awake()
        {
            Instance = this;

            _controllers = new GameObject("Controllers");
            _inputController = _controllers.AddComponent<InputController>();
            _spawnController = _controllers.AddComponent<SpawnController>();
            _sceneLiveController = _controllers.AddComponent<SceneLiveController>();
            _playersTurn = _controllers.AddComponent<PlayersTurn>();

            _objectManager = GetComponent<ObjectManager>();
            _TargetSelector = GetComponent<TargetSelector>();
        }

        #region Получение контроллеров извне

        /// <summary>
        /// Контроллер, отвечающий за управление
        /// </summary>
        public InputController GetInputController { get { return _inputController; } }

        /// <summary>
        /// Здесь хранится большинство информации, что нужна в разных скриптах
        /// </summary>
        public ObjectManager GetObjectManager { get { return _objectManager; } }

        /// <summary>
        /// Контролирует спаун существ
        /// </summary>
        public SpawnController GetSpawnController { get { return _spawnController; } }

        public SceneLiveController GetSceneLiveController { get { return _sceneLiveController; } }

        public PlayersTurn GetPlayersTurn { get { return _playersTurn; } }

        public TargetSelector GetTargetSelector { get { return _TargetSelector; } }

        #endregion Получение контроллеров извне
    }
}