using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Смартфонное управление
    /// </summary>
    public class InputController : BaseController
    {
        TargetSelector _ts;

        Canvas _cardsCanv;

        Vector3 _startScale, _downScale;

        private void Start()
        {
            _ts = Main.Instance.GetTargetSelector;
            _cardsCanv = Main.Instance.GetObjectManager.GetCardsCanvas;
            _startScale = _cardsCanv.transform.localScale;
            _downScale = _startScale / 2;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            if (Input.mousePosition.y > 150 && Input.touches[0].phase != TouchPhase.Ended)
            {
                _ts.On();

                _cardsCanv.transform.localScale = _downScale;
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
                _cardsCanv.transform.localScale = _startScale;
        }
    }
}