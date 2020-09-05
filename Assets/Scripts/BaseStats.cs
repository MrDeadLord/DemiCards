using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    public class BaseStats : MonoBehaviour, IChangeValue
    {
        [SerializeField] private int _maxHP;
        [SerializeField] private int _maxMana;
        [SerializeField] private int _startActionPoints;
        [SerializeField] [Range(0, 11)] private int _maxHandSize;

        private int _curHP;
        private int _curMana;

        private int _attack = 0;    //Изначально герой не может атаковать

        private string _className;
        private int _exp;   //текущее кол-во опыта
        private int _nextLvlExp;    //необходимый опыт до след. уровня
        private int _lvl;

        private int _strengh;
        private int _agility;
        private int _inteligence;

        public void HealDam(int value, bool isHeal)
        {
            if (isHeal)
            {
                _curHP += value;

                if (_curHP > _maxHP)
                    _curHP = _maxHP;
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
                    }
                    break;

                case "-/-":
                    _attack -= att;
                    if (_attack < 0)
                        _attack = 0;

                    _curHP -= hp;
                    if (_curHP <= 0)
                        Death();
                    break;
            }
        }

        private void Death()
        {

        }

        #region Получение данных в других классах
        public int GetMaxHP { get { return _maxHP; } }
        public int GetCurHP { get { return _curHP; } }
        public int GetMaxMana { get { return _maxMana; } }
        public int GetCurMana { get { return _curMana; } }

        public int MaxHandSize
        {
            get { return _maxHandSize; }
            set { _maxHandSize = value; }
        }
        public int StartActionPoints
        {
            get { return _startActionPoints; }
            set { _startActionPoints = value; }
        }
        #endregion
    }
}
