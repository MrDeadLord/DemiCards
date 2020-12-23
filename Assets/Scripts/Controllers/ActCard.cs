using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class ActCard : BaseController
    {
        #region Переменные
        Card _card = new Card();

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
                _activator.ActivateCard(_card.id);

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
            Debug.Log("actCard ON");
            _renderer.enabled = true;   //включение рендера

            foreach (Material mat in _allMats)
            {
                if (mat.name == _card.CardsData.cardName)
                    _renderer.material = mat;
            }   //Выбор нужного материала

            gameObject.name = _card.CardsData.cardName;
        }

        public override void Off()
        {
            Enabled = false;

            _renderer.enabled = false;

            _card = null;
        }

        public Card Card
        {
            get { return _card; }
            set { _card = value; }
        }
    }
}