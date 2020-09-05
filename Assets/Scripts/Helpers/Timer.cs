using System;

namespace DeadLords.Helpers
{
    public class Timer
    {
        private DateTime _start;
        private float _elapsed = -1;
        private TimeSpan _duration;

        /// <summary>
        /// Запуск таймера
        /// </summary>
        /// <param name="elapsed">Длительность</param>
        public void Start(float elapsed)
        {
            _elapsed = elapsed;
            _start = DateTime.Now;
            _duration = TimeSpan.Zero;
        }
                
        public void Update()
        {
            if (_elapsed > 0)
            {
                _duration = DateTime.Now - _start;

                if (_duration.TotalSeconds > _elapsed)
                    _elapsed = 0;
                else if (_elapsed == 0)
                    _elapsed = -1;
            }
        }

        /// <summary>
        /// Возвращает true когда отсчет окончен
        /// </summary>
        /// <returns></returns>
        public bool IsEvent()
        {
            return _elapsed == 0;
        }
    }
}