using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class ActCard : BaseController
    {
        #region Переменные
        Card _card;

        Renderer _renderer;
        List<Material> _allMats = new List<Material>();
        CardActivator _activator;

        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            _card = GetComponent<Card>();
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
                _activator.ActivateCard(_card);

                //Запуск анимации вызова карты
            }
            else
            {
                //Запуск анимации отмены
            }

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