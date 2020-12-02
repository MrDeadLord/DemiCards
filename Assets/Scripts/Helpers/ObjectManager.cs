using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeadLords.Helpers
{
    /// <summary>
    /// Хранилище ссылок на все нужные в игре(в бою) объекты(кроме карт)
    /// </summary>
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] [Tooltip("Точки появления созданий игрока")] private Transform playerSpawnPoints;
        [SerializeField] [Tooltip("Точки появления существ противника")] private Transform enemySpawnPoints;

        [Space(10)]
        [Header("Все живые на поле боя")]
        [SerializeField] GameObject player;
        [SerializeField] GameObject enemy;


        [Space(10)]
        [SerializeField] [Tooltip("Существа игрока")] private Creature[] crPlayer;
        [SerializeField] [Tooltip("Существа врага")] private Creature[] crEnemy;

        [Space(10)]
        [Header("Карты")]
        [SerializeField] [Tooltip("Точки расположения карт")] private List<CardsButton> cardsButtons;

        [Space(5)]
        [SerializeField] [Tooltip("Картинки(спрайты) всех карт")] private List<Sprite> cardsSprites;
        [SerializeField] [Tooltip("Материалы всех карт")] private List<Material> cardsMaterials;

        [Space(10)]
        [SerializeField] [Tooltip("Полный список всех существ в игре")] private List<Creature> crList = new List<Creature>();

        #region Доступ к переменным извне

        public Transform PlayerSpawnPoints { get { return playerSpawnPoints; } }
        public Transform EnemySpawnPoints { get { return enemySpawnPoints; } }

        public Creature[] CrPlayer { get { return crPlayer; } }
        public Creature[] CrEnemy { get { return crEnemy; } }

        public GameObject Player { get { return player; } }
        public GameObject Enemy { get { return enemy; } }

        public List<CardsButton> GetCardsButtons { get { return cardsButtons; } }

        public List<Creature> GetCreatures { get { return crList; } }

        public List<Sprite> GetCardsSprites { get { return cardsSprites; } }
        public List<Material> GetCardsMaterials { get { return cardsMaterials; } }

        #endregion Доступ к переменным извне
    }
}