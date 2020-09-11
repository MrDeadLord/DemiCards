﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DeadLords
{
    /// <summary>
    /// Присваивается к UI.button
    /// </summary>
    [RequireComponent(typeof(Material))]
    public class Card : MonoBehaviour
    {
        [SerializeField] [Tooltip("Полное содержание карты")] private CardData _cardData;

        #region Бонус, если он есть
        [Space(10)]
        [Header("Бонус, если его нет - не заполнять")]

        private BonusData _cardsBonus;
        #endregion

        /// <summary>
        /// true если карта на столе, false - в руке
        /// </summary>
        private bool _isActive = false;

        #region Unity time        
        private void Update()
        {
            if (!_isActive)
                return;

            if (_cardData.creatureName != string.Empty)
                Summon();
            else
                ActivateBonus();
        }
        #endregion

        /// <summary>
        /// Призыв существа
        /// </summary>
        private void Summon()
        {
            if (Main.Instance.GetSpawnController.CanSpawn(transform.parent.tag))
            {
                //Main.Instance.GetSpawnController.Spawn(_creature, transform.parent.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Too many creatures allready");
                _isActive = false;
            }
        }

        /// <summary>
        /// Активация заклинания
        /// </summary>
        private void ActivateBonus()
        {
            _cardsBonus = BonusConvert(_cardData.cardsBonusIndex);
        }

        /// <summary>
        /// Получение бонуса из id
        /// </summary>
        /// <param name="id">Bonus id</param>
        /// <returns></returns>
        private BonusData BonusConvert(int id)
        {
            string path = Application.dataPath + "/Bonuses.txt";

            var bd = new BonusData();

            bd = JsonUtility.FromJson<BonusData>(File.ReadAllLines(path)[id]);

            return bd;
        }

        #region Для редактора
        public CardData CardsData
        {
            get { return _cardData; }
            set { _cardData = value; }
        }

        public BonusData CardsBonus
        {
            get { return _cardsBonus; }
            set { _cardsBonus = value; }
        }
        /// <summary>
        /// Если нужно применить карту - true
        /// </summary>
        public bool Active
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion
    }
}