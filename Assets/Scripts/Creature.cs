using UnityEngine;

namespace DeadLords
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private int _attack;
        [SerializeField] private int _maxHP;

        private int _curHP;        
        private bool _isActive;

        public void Death()
        {
            Destroy(gameObject);    //Добавить запуск анимации
        }

        /// <summary>
        /// Лечение
        /// </summary>
        /// <param name="value"></param>
        public void Heal(int value)
        {
            if (_curHP < _maxHP)
            {
                _curHP += value;

                if (_curHP > _maxHP)
                    _curHP = _maxHP;
            }
        }

        /// <summary>
        /// Получение урона. Смерть если _curHP <= 0
        /// </summary>
        /// <param name="value">Урон</param>
        public void TakeDamage(int value)
        {
            _curHP -= value;

            if (_curHP <= 0)
                Death();
        }

        /// <summary>
        /// Баф/дебаф атаки и НР существа
        /// </summary>
        /// <param name="att">Значение атаки(отричательное если дебаф)</param>
        /// <param name="hp">Значение НР(отрицательное если дебаф)</param>
        public void BustDebust(int att, int hp)
        {
            _attack += att;
            _curHP += hp;

            if (_attack < 0)
                _attack = 0;

            if (_curHP > _maxHP)
                _maxHP = _curHP;

            if (_curHP <= 0)
                Death();
        }

        #region Получение данных

        /// <summary>
        /// Значение атаки существа
        /// </summary>
        public int AttackValue
        {
            get { return _attack; }
            set { _attack = value; }
        }

        /// <summary>
        /// Максимально возможное здоровье существа
        /// </summary>
        public int MaxHP
        {
            get { return _maxHP; }
            set { _maxHP = value; }
        }

        /// <summary>
        /// Текущее здоровье существа
        /// </summary>
        public int GetCurHP { get { return _curHP; } }

        /// <summary>
        /// Активно ли существо. Может ли атаковать
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        #endregion
    }
}