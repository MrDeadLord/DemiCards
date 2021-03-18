using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class ActCard : BaseController
    {
        #region Переменные
        public Card card { get; set; }

        Renderer _renderer;
        List<Material> _allMats = new List<Material>();
        CardActivator _activator;

        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            _renderer = GetComponent<Renderer>();

            _allMats = Main.Instance.GetObjectManager.GetCardsMaterials;

            _activator = GetComponent<CardActivator>();

            Off();
        }
        #endregion Unity-time

        /// <summary>
        /// Отмена действия карты. Возвращение ее в руку(false). Удаление карты с руки(true)
        /// </summary>
        /// <param name="isDone"></param>
        public void Cancel(bool isDone)
        {
            if (isDone)
            {
                _activator.ActivateCard(card);

                //Запуск анимации вызова карты
            }
            /*else
            {
                //Запуск анимации отмены
            }*/

            Off();
        }

        public override void On()
        {
            Enabled = true;

            _renderer.enabled = true;   //включение рендера

            if (card == null)
                return;

            foreach (Material mat in _allMats)
            {
                if (mat.name == card.CardsData.cardName)
                    _renderer.material = mat;
            }   //Выбор нужного материала

            gameObject.name = card.CardsData.cardName;
        }

        public override void Off()
        {
            Enabled = false;

            _renderer.enabled = false;

            card = null;
        }
    }
}