using System.Collections.Generic;
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

        /// <summary>
        /// Полное содержание бонуса(если карта - заклинание)
        /// </summary>
        private BonusData _cardsBonus;
        /// <summary>
        /// Существо, если это призыв. Иначе - null
        /// </summary>
        private Creature _creature;

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
            
        }
                
        #region Для редактора
        /// <summary>
        /// Содержание карты
        /// </summary>
        public CardData CardsData
        {
            get { return _cardData; }
            set { _cardData = value; }
        }
        /// <summary>
        /// Существо карты
        /// </summary>
        public Creature CardsCreature
        {
            get { return _creature; }
            set { _creature = value; }
        }
        /// <summary>
        /// Заклинание карты
        /// </summary>
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