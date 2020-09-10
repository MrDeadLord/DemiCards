using DeadLords.Controllers;
using DeadLords.Helpers;
using UnityEngine;

namespace DeadLords
{
    public class Main : MonoBehaviour
    {
        public static Main Instance { get; private set; }

        private GameObject _controllers;
        private InputController _inputController;
        private DayLightController _dayLightController;
        private ObjectManager _objectManager;
        private SpawnController _spawnController;
        private SceneLiveController _sceneLiveController;
        private BonusController _bonusController;
        private AllCreatures _allCreatures;

        private void Start()
        {
            Instance = this;

            _controllers = new GameObject("Controllers");
            _inputController = _controllers.AddComponent<InputController>();
            _spawnController = _controllers.AddComponent<SpawnController>();
            _sceneLiveController = _controllers.AddComponent<SceneLiveController>();
            _bonusController = _controllers.AddComponent<BonusController>();

            _dayLightController = GetComponent<DayLightController>();
            _objectManager = GetComponent<ObjectManager>();
            _allCreatures = GetComponent<AllCreatures>();
        }

        #region Получение контроллеров извне

        public InputController GetInputController { get { return _inputController; } }

        public DayLightController GetDayLightController { get { return _dayLightController; } }

        public ObjectManager GetObjectManager { get { return _objectManager; } }

        public SpawnController GetSpawnController { get { return _spawnController; } }

        public SceneLiveController GetSceneLiveController { get { return _sceneLiveController; } }

        public BonusController GetBonusController { get { return _bonusController; } }

        public AllCreatures GetAllCreatures { get { return _allCreatures; } }

        #endregion
    }
}
