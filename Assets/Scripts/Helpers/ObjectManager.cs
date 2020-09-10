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
        [SerializeField] [Tooltip("Игрок")] private Player player;
        [SerializeField] [Tooltip("Противник")] private Enemy enemy;
        [Space(10)]
        [SerializeField] [Tooltip("Существа игрока")] private Creature[] crPlayer;
        [SerializeField] [Tooltip("Существа врага")] private Creature[] crEnemy;

        [Space(10)]
        [Header("Карты")]
        [SerializeField] [Tooltip("Точки расположения четного кол-ва карт")] private List<Button> cardsEven;
        [SerializeField] [Tooltip("Точки расположения нечетного кол-ва карт")] private List<Button> cardsOdd;

        #region Доступ к переменным извне
        public Transform PlayerSpawnPoints { get { return playerSpawnPoints; } }
        public Transform EnemySpawnPoints { get { return enemySpawnPoints; } }
        public Player Player { get { return player; } }
        public Enemy Enemy { get { return enemy; } }
        public Creature[] CrPlayer { get { return crPlayer; } }
        public Creature[] CrEnemy { get { return crEnemy; } }

        public List<Button> GetCardsEven { get { return cardsEven; } }
        public List<Button> GetCardsOdd { get { return cardsOdd; } }
        #endregion
    }
}