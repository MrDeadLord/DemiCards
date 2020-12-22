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
    public class CardsButton : BaseController, IPointerEnterHandler, IPointerExitHandler
    {
        #region Переменные
        [SerializeField] [Tooltip("Картинка, что будет высвечиваться при нажатии на карту")] Image _sizeUpImage;
        [SerializeField] [Tooltip("Карта, что отображается при использовании")] ActCard _actCard;
        [SerializeField] [Tooltip("Эффект выделения")] GameObject _selector;

        InputController _inpContr;

        Button _cardButt;
        /// <summary>
        /// Картинка внутри карты (на руках)
        /// </summary>
        Image _img;

        //Цвета для отключения видимости объектов
        Color _alphaOff = new Color(255, 255, 255, 0);
        Color _alphaOn = new Color(255, 255, 255, 255);

        /// <summary>
        /// Переменная для выключения постоянной инициации
        /// </summary>
        bool _loaded = false;
        #endregion Переменные

        #region Unity-time
        private void Start()
        {
            _inpContr = GetComponentInParent<InputController>();

            _sizeUpImage.enabled = false;

            _cardButt = GetComponent<Button>();
            _img = GetComponent<Image>();

            ToggleSelector(false);

            Off();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            if (!_loaded)
                Init();
        }
        #endregion

        /// <summary>
        /// Вкл/выкл селектора карты
        /// </summary>
        /// <param name="def">True - влк.</param>
        public void ToggleSelector(bool def)
        {
            foreach (Renderer rend in _selector.GetComponentsInChildren<Renderer>())
                rend.enabled = def;
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

        /// <summary>
        /// Инициализация карт(Присвоение нужного имени и картинки)
        /// </summary>
        /// <returns>null</returns>
        void Init()
        {
            gameObject.name = GetComponent<Card>().CardsData.cardName;

            GetComponent<ImagenizeButton>().Imagine();
            _loaded = true;
        }

        #region Поведение кнопки

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Input.touches.Length == 0)
                return;

            if (Input.touches[0].phase == TouchPhase.Ended)
                return;

            if (!_inpContr.Enabled)
                _inpContr.On();

            _sizeUpImage.enabled = true;
            _sizeUpImage.sprite = _img.sprite;

            _img.color = _alphaOff;

            _actCard.Card = GetComponent<Card>();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _sizeUpImage.enabled = false;

            _img.color = _alphaOn;
        }
        #endregion
    }
}