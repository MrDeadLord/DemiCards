using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class ActCard : BaseController
    {
        #region Переменные
        Card _card = new Card();

        Hand _hand;
        Renderer _renderer;
        List<Material> _allMats = new List<Material>();

        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            _hand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
            _renderer = GetComponent<Renderer>();

            _allMats = Main.Instance.GetObjectManager.GetCardsMaterials;

            Off();
        }
        #endregion Unity-time

        /// <summary>
        /// Отмена действия карты. Возвращение ее в руку
        /// </summary>
        public void Cancel()
        {
            Off();
        }

        public override void On()
        {
            Enabled = true;

            name = Card.CardsData.cardName; //присвоение имени

            _renderer.enabled = true;   //включение рендера

            foreach (Material mat in _allMats)
            {
                if (mat.name == name)
                    _renderer.material = mat;
            }   //Выбор нужного материала            
        }

        public override void Off()
        {
            Enabled = false;

            _renderer.enabled = false;
        }

        public Card Card
        {
            get { return _card; }
            set { _card = value; }
        }
    }
}