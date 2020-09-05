using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    public class Creature : MonoBehaviour, IChangeValue
    {
        [SerializeField] private int _attack;
        [SerializeField] private int _maxHP;

        private int _curHP;
        private bool _isActive;

        public void Death()
        {

        }

        public void CantAttack()
        {

        }

        /// <summary>
        /// Атака цели
        /// </summary>
        /// <param name="obj">Цель</param>
        public void Attack(IChangeValue obj)
        {
            if (obj != null)
            {                
                HealDam(_attack, false);
            }
        }   //Добавить анимации и т.д.

        public void HealDam(int value, bool isHeal)
        {
            if (isHeal)
            {
                if(_curHP < _maxHP)
                {
                    _curHP += value;

                    if (_curHP > _maxHP)
                        _curHP = _maxHP;
                }
            }
            else
            {
                _curHP -= value;

                if (_curHP <= 0)
                    Death();
            }
        }

        public void BustDebust(int att, int hp, string act)
        {
            switch (act)
            {
                case "+/+":
                    _attack += att;
                    _curHP += hp;
                    break;

                case "+/-":
                    _attack += att;
                    _curHP -= hp;

                    if (_curHP <= 0)
                        Death();
                    break;

                case "-/+":
                    _attack -= att;
                    _curHP += hp;

                    if (_attack < 0)
                    {
                        _attack = 0;
                        CantAttack();
                    }
                    break;

                case "-/-":
                    _attack -= att;
                    if (_attack < 0)
                    {
                        _attack = 0;
                        CantAttack();
                    }

                    _curHP -= hp;
                    if (_curHP <= 0)
                        Death();
                    break;
            }
        }

        public int GetAttack { get { return _attack; } }
        public int GetMaxHP { get { return _maxHP; } }
        public int GetCurHP { get { return _curHP; } }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
    }
}