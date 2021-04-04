using UnityEngine;

namespace DeadLords.Controllers
{
    public class InputControllerHand : BaseController
    {
        #region ========== Variables ========

        [SerializeField] InputController _in;
        RaycastHit _hit;
        Card _card;

        #endregion ========== Variables ========

        private void Update()
        {
            if (!Enabled)
                return;

            // Выбор карты
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit))
            {
                if(_hit.transform.tag == "Card")
                {
                    _card = _hit.transform.GetComponent<Card>();
                    _card.GetComponent<Animator>().SetTrigger("Detailed"); // Выделенная карта

                    _in.CardSelected = true;
                }
            }
        }

        /// <summary>
        /// Предактивация карты и уменьшение размера карт на руке
        /// </summary>
        public void ActivationPhase()
        {
            _card.GetComponent<CardActivator>().PreActivate(_card);

            // Уменьшение размера оставшихся карт на руках
            transform.localScale /= 2;
        }

        public override void Off()
        {
            base.Off();

            // Возврат карты в руку            
            _card.GetComponent<Animator>().SetTrigger("Deselected");
            transform.localScale *= 2;
        }
    }
}