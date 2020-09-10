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

        private void ActivateBonus()
        {

        }

        #region Для редактора
        /// <summary>
        /// Название карты
        /// </summary>
        public string Name
        {
            get { return _cardData.cardName; }
            set { _cardData.cardName = value; }
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