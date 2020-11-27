using DeadLords.Controllers;
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
        [SerializeField] [Tooltip("Картинка, что будет высвечиваться при нажатии на карту")] Image _sizeUpImage;

        Button _cardButt;
        Vector3 _startPosition;
        Quaternion _startRotation;
        Vector3 _startScale;
        Image _img;

        Color _alphaOff = new Color(255, 255, 255, 0);
        Color _alphaOn = new Color(255, 255, 255, 255);

        /// <summary>
        /// Переменная для выключения постоянной инициации
        /// </summary>
        bool _loaded = false;

        #region Unity-time
        private void Start()
        {
            _sizeUpImage.enabled = false;

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

            if (_loaded)
                return;

            Init();
        }
        #endregion

        /// <summary>
        /// Инициализация карт
        /// </summary>
        /// <returns>null</returns>
        void Init()
        {
            gameObject.name = GetComponent<Card>().CardsData.cardName;
            
            GetComponent<ImagenizeButton>().Imagine();
            _loaded = true;
        }

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
            _loaded = false;
        }

        #region Поведение кнопки
                
        public void OnSelect(BaseEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {            
            _sizeUpImage.enabled = true;
            _sizeUpImage.sprite = _img.sprite;

            _img.color = _alphaOff;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _sizeUpImage.enabled = false;

            _img.color = _alphaOn;

            if (Input.GetTouch(0).position.y > 160)
            {
                GetComponent<CardActivator>().On();
            }
        }
        #endregion
    }
}