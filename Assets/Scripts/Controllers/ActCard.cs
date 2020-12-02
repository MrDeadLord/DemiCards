using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class ActCard : BaseController
    {
        #region Переменные
        public Card Card { get; set; }

        Hand _hand;
        Renderer _renderer;
        /// <summary>
        /// Материал активируемой карты
        /// </summary>
        Material _mat;
        List<Material> _allMats = new List<Material>();

        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            _hand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
            _renderer = GetComponent<Renderer>();
            _mat = GetComponent<Material>();

            _allMats = Main.Instance.GetObjectManager.GetCardsMaterials;
        }

        private void Update()
        {
            if (!Enabled)
                return;
        }
        #endregion Unity-time

        public override void On()
        {
            Enabled = true;

            name = Card.CardsData.cardName;

            _renderer.enabled = true;
            
            foreach (Material mat in _allMats)
            {
                if (mat.name == name)
                    _mat = mat;
            }
        }

        public override void Off()
        {
            Enabled = false;

            _renderer.enabled = false;
        }
    }
}