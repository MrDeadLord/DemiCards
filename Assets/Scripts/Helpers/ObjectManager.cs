using System.Collections.Generic;
using UnityEngine;
using DeadLords.Controllers;

namespace DeadLords.Helpers
{
    /// <summary>
    /// Хранилище ссылок на все нужные в игре(в бою) объекты(кроме карт)
    /// </summary>
    public class ObjectManager : MonoBehaviour
    {
        [SerializeField] [Tooltip("Карты на руках игрока")] List<Card> cardsVisual = new List<Card>();

        [Space(10)]
        [SerializeField] [Tooltip("Точки появления созданий игрока")] private Transform playerSpawnPoints;
        [SerializeField] [Tooltip("Точки появления существ противника")] private Transform enemySpawnPoints;

        [Header("Игроки")]
        [SerializeField] GameObject player;
        [SerializeField] GameObject enemy;
                
        [Space(5)]
        [SerializeField] [Tooltip("Картинки(спрайты) всех карт")] private List<Sprite> cardsSprites;
        [SerializeField] [Tooltip("Материалы всех карт")] private List<Material> cardsMaterials;

        [Space(10)]
        [SerializeField] [Tooltip("Полный список всех существ в игре")] private List<Creature> crList = new List<Creature>();

        public List<Creature> CrPlayer { get; set; }
        public List<Creature> CrEnemy { get; set; }

        #region ==== Publics ====

        public List<Card> PlayersHand { get { return cardsVisual; } set { cardsVisual = value; } }

        public Transform PlayerSpawnPoints { get { return playerSpawnPoints; } }
        public Transform EnemySpawnPoints { get { return enemySpawnPoints; } }
                
        public GameObject Player { get { return player; } }
        public GameObject Enemy { get { return enemy; } }

        public List<Creature> GetCreatures { get { return crList; } }

        public List<Sprite> GetCardsSprites { get { return cardsSprites; } }
        public List<Material> GetCardsMaterials { get { return cardsMaterials; } }

        #endregion ==== Publics ====
    }
}