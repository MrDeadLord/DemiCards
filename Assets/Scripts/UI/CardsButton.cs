using DeadLords.Controllers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeadLords
{
    /// <summary>
    /// Перемещает карту в центр экрана
    /// </summary>
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Card))]
    public class CardsButton : BaseController, ISelectHandler, IPointerDownHandler, IPointerExitHandler
    {
        string _name;
        Button _cardButt;
        Vector3 _startPosition;
        Quaternion _startRotation;
        Vector3 _startScale;
        Image _img;

        #region Unity-time
        private void Start()
        {
            _name = gameObject.name;
            _cardButt = GetComponent<Button>();
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            _startScale = transform.localScale;
            _img = GetComponent<Image>();

            Off();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            if (!Main.Instance.deckLoadedPl)
                return;

            Init();
        }

        /// <summary>
        /// Инициализация карт
        /// </summary>
        /// <returns>null</returns>
        void Init()
        {
            //Если карта пустая - убираем из видимости
            if (GetComponent<Card>().CardsData != null)
            {
                On();

                _name = GetComponent<Card>().CardsData.cardName;
            }
            else
            {
                Off();
            }
        }
        #endregion

        public override void On()
        {
            Enabled = true;
            _cardButt.enabled = true;
            _img.enabled = true;
        }

        public override void Off()
        {
            _cardButt.enabled = false;
            _img.enabled = false;
            Enabled = false;
        }

        #region Поведение кнопки
        private void OnClickEvent()
        {
            Debug.Log("Clicked");
        }

        public void OnSelect(BaseEventData eventData)
        {
            //transform.localScale *= 2;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Held down");
            transform.localScale *= 2;
            transform.position = Input.mousePosition;
            transform.rotation.Set(0, 0, 0, 0);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            transform.localScale = _startScale;
        }
        #endregion
    }
}