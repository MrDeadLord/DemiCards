using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    public class BaseController : MonoBehaviour
    {
        private bool _enabled = false; //По умолчанию контроллер выключен

        /// <summary>
        /// Включен ли контроллер
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /// <summary>
        /// Переопределяемый метод, что будет происходить при включении контроллера
        /// </summary>
        public virtual void On()
        {
            _enabled = true;
        }

        /// <summary>
        /// Также переопределяемый метод, что определяет действия при выключении
        /// </summary>
        public virtual void Off()
        {
            _enabled = false;
        }

        //Принцип работы таков, что в Update() проверяется включен ли контроллер.
        //Если нет, то апдейт прекращает выполнение, что разгружает систему
    }
}