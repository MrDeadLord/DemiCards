using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace DeadLords
{
    /// <summary>
    /// Перемещает карту в центр экрана
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class CardsButtons : MonoBehaviour, ISelectHandler, IPointerDownHandler, IPointerExitHandler
    {
        Button _cardButt;
        Vector3 _startPos;
        Vector3 _startScale;

        private void Start()
        {
            _cardButt = GetComponent<Button>();
            _startPos = transform.position;
            _startScale = transform.localScale;
        }

        private void Update()
        {
            _cardButt.onClick.AddListener(OnClickEvent);            
        }

        void OnClickEvent()
        {
            Debug.Log("Clicked");
        }

        public void OnSelect(BaseEventData eventData)
        {
            transform.localScale *= 2;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("down");
            transform.position = Input.mousePosition;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = _startScale;
            transform.position = _startPos;
        }
    }
}
