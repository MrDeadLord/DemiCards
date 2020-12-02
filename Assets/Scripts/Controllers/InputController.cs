using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Смартфонное управление
    /// </summary>
    public class InputController : BaseController
    {
        private void Update()
        {
            if (!Enabled)
                return;

            //Если начало касания было в районе карты, а сейчас находится выше - кативируем
            if(Input.GetTouch(0).deltaPosition.y < 150
                && Input.GetTouch(0).deltaPosition.y > 30
                && Input.GetTouch(0).position.y > 150)
            {
                Main.Instance.GetCardActivator.On();
            }

            if (Main.Instance.GetCardActivator.Enabled)
            {

            }
        }
    }
}