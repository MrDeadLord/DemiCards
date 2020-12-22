using UnityEngine;
using UnityEngine.EventSystems;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Смартфонное управление
    /// </summary>
    public class InputController : BaseController, IPointerEnterHandler, IPointerExitHandler
    {
        TargetSelector _ts;

        Canvas _cardsCanv;

        Vector3 _startScale, _downScale;

        bool _active = false;

        private void Start()
        {
            _ts = Main.Instance.GetTargetSelector;
            _cardsCanv = GetComponent<Canvas>();
            _startScale = _cardsCanv.transform.localScale;
            _downScale = _startScale / 2;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Устранение вечной ошибки следующего if()
            if (Input.touches.Length == 0)
                return;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_active)
            {
                _cardsCanv.transform.localScale = _startScale;
                _ts.Cancel(false);

                _active = false;
            }
                
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _ts.On();
            
            _active = true;
            _cardsCanv.transform.localScale = _downScale;
        }
    }
}