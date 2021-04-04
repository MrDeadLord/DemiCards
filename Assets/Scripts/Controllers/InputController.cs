using UnityEngine;
using UnityEngine.EventSystems;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Смартфонное управление
    /// </summary>
    public class InputController : BaseController, IPointerEnterHandler, IPointerExitHandler
    {
        #region ========== Variables ========

        [SerializeField] InputControllerHand _inpHand;

        RectTransform _cardsPanel;

        Vector3 _startScale, _downScale;

        // Флаг для понимания был палец на панели или еще нет
        bool _active = false;

        #endregion ========== Variables ========

        private void Start()
        {
            _cardsPanel = GetComponent<RectTransform>();
            _startScale = _cardsPanel.transform.localScale;
            _downScale.y = _startScale.y / 2;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            // Если экрана не касаются, то курсор сбрасывается на позицию по умолчанию НАВЕРНОЕ. Проверить!
            if (Input.touches.Length == 0)
                Cursor.lockState = CursorLockMode.Confined;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_active)
            {
                _cardsPanel.transform.localScale = _startScale; // Восстановление норм размера

                _active = false;    // Карта не выбрана
                _inpHand.Off();     // Выбор карт отключаем
            }
            else
            {
                // Если карта еще не выбрана, то включаем выбор карт
                _inpHand.On();
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_active)
            {
                _cardsPanel.transform.localScale = _downScale;
                _inpHand.ActivationPhase();
            }            
        }

        public bool CardSelected { set { _active = value; } }
    }
}