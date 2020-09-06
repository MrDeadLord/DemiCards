using DeadLords.Controllers;
using DeadLords.Interface;
using UnityEngine;

namespace DeadLords
{
    public class BonusController : BaseController
    {
        //Частые названия сокращены:
        //Creature - cr, destroy - dist
        //Даже если не задействуется урон существа или его на объекте вовсе нет, стоит указывать +/+,-/-,+/-,-/+
        //Нулевое значение ничего не изменит
        //!!!НЕ ЗАБЫВАТЬ ПОПОЛНЯТЬ DeadLords/BonusNames!!!

        SceneLiveController lc = Main.Instance.GetSceneLiveController;

        /// <summary>
        /// Вызов бонуса
        /// </summary>
        /// <param name="type">Название/тип бонуса</param>
        /// <param name="target">Цель</param>
        /// <param name="att">Значение изменения атаки</param>
        /// <param name="hp">Значение изменения HP</param>
        /// <param name="speller">Кто вызывает спел</param>
        public void CallBonus(string type, IChangeValue target, int att, int hp, GameObject speller)
        {
            switch (type)
            {
                #region HEAL
                case "heal":
                    HealObject(target, hp);
                    break;
                case "heal ally cr":
                    HealObject(target, hp);
                    break;
                case "heal all ally cr":
                    HealAllAllyCreatures(speller, hp);
                    break;
                case "heal all cr":
                    HealAllCreatures(hp);
                    break;
                #endregion

                #region Single Bust/Debust
                case "bust/bust":
                    BustDebust(target, att, hp, "+/+");
                    break;
                case "debust/debust":
                    BustDebust(target, att, hp, "-/-");
                    break;
                case "bust/debust":
                    BustDebust(target, att, hp, "+/-");
                    break;
                case "debust/bust":
                    BustDebust(target, att, hp, "-/+");
                    break;
                #endregion

                #region Bust/Debust all allyes
                case "bust/bust all allyes cr":
                    BustDebuffAllAllyCreatures(speller, att, hp, "+/+");
                    break;
                case "debust/debust all allyes cr":
                    BustDebuffAllAllyCreatures(speller, att, hp, "-/-");
                    break;
                case "bust/debust all allyes cr":
                    BustDebuffAllAllyCreatures(speller, att, hp, "+/-");
                    break;
                case "debust/bust all allyes cr":
                    BustDebuffAllAllyCreatures(speller, att, hp, "-/+");
                    break;
                #endregion

                #region Bust/debust ALL cr
                case "bust/bust all cr":
                    BustAllCreatures(att, hp, "+/+");
                    break;
                case "debust/debust all cr":
                    BustAllCreatures(att, hp, "-/-");
                    break;
                case "bust/debust all cr":
                    BustAllCreatures(att, hp, "+/-");
                    break;
                case "debust/bust all cr":
                    BustAllCreatures(att, hp, "-/+");
                    break;
                #endregion

                default:
                    Debug.LogError("Ошибка");
                    break;
            }
        }

        #region Исцеление

        /// <summary>
        /// Исцеляет выбранную цель
        /// </summary>
        /// <param name="healObj">Цель исцеления</param>
        /// <param name="value">Кол-во исцеления</param>
        private void HealObject(IChangeValue healObj, int value)
        {
            healObj.HealDam(value, true);
        }

        /// <summary>
        /// Хилит всех союзных существ
        /// </summary>
        /// <param name="summoner">Призыватель</param>
        /// <param name="value">Значение</param>
        private void HealAllAllyCreatures(GameObject speller, int value)
        {
            if (speller.tag == "Player")
            {
                foreach (Creature cr in Main.Instance.GetObjectManager.CrPlayer)
                    cr.HealDam(value, true);
            }
            else
            {
                foreach (Creature cr in Main.Instance.GetObjectManager.CrEnemy)
                    cr.HealDam(value, true);
            }
        }

        private void HealAllCreatures(int value)
        {
            foreach (Creature cr in Main.Instance.GetObjectManager.CrPlayer)
                cr.HealDam(value, true);
            foreach (Creature cr in Main.Instance.GetObjectManager.CrEnemy)
                cr.HealDam(value, true);
        }
        #endregion

        #region Бусты/Дебусты атаки и хп

        /// <summary>
        /// Изменение параметров ATT/HP в заданном направлении(+/-)
        /// </summary>
        /// <param name="obj">Объект изменения</param>
        /// <param name="value">Значение(ATT.HP)</param>
        /// <param name="act">Действие(+/+,-/-,+/-,-/+)</param>
        private void BustDebust(IChangeValue obj, int att, int hp, string act)
        {
            obj.BustDebust(att, hp, act);
        }

        /// <summary>
        /// Изменение ATT/HP у всех союзных созданий
        /// </summary>
        /// <param name="crs">Существа, которых будет бустить/дебафить</param>
        /// <param name="value">Кол-во</param>
        /// <param name="act">Действие(+/+,-/-,+/-,-/+)</param>
        private void BustDebuffAllAllyCreatures(GameObject speller, int att, int hp, string act)
        {
            if (speller.tag == "Player")
            {
                foreach (Creature cr in Main.Instance.GetObjectManager.CrPlayer)
                {
                    BustDebust(cr, att, hp, act);
                }
            }
            else
            {
                foreach (Creature cr in Main.Instance.GetObjectManager.CrEnemy)
                {
                    BustDebust(cr, att, hp, act);
                }
            }
        }

        /// <summary>
        /// Изменение ATT/HP ВСЕХ существ на поле боя
        /// </summary>
        /// <param name="value">значение(ATT/HP)</param>
        /// <param name="act">Действие(+/+,-/-,+/-,-/+)</param>
        private void BustAllCreatures(int att, int hp, string act)
        {
            foreach (Creature cr in Main.Instance.GetObjectManager.CrPlayer)
            {
                BustDebust(cr, att, hp, act);
            }

            foreach (Creature cr in Main.Instance.GetObjectManager.CrEnemy)
            {
                BustDebust(cr, att, hp, act);
            }
        }
        #endregion

        #region Прочее
        /// <summary>
        /// Мгновенное уничтожение существа cr
        /// </summary>
        /// <param name="cr">Уничтожаемое существо</param>
        /// <param name="effect">Эффект при убийстве</param>
        private void KillCreature(Creature cr, ParticleSystem effect)
        {
            Instantiate(effect, cr.transform.position, Quaternion.identity);
            cr.Death();
        }   //Под вопросом эффект

        /// <summary>
        /// Взять карту из коллоды
        /// </summary>
        /// <param name="speller">Заклинатель</param>
        /// <param name="isTargetApponent">Является ли целью аппонент(с чьей коллоды берется карта)</param>
        private void GrabaCard(GameObject speller, bool isTargetApponent)
        {
            if (speller.tag == "Player")
            {
                if (isTargetApponent)
                {
                    int n = (int)Random.Range(0, lc.EnemysDeck.Count);
                    lc.PlayersHand.Add(lc.EnemysDeck[n]);
                    lc.EnemysDeck.RemoveAt(n);
                }   //Берется карта из коллоды врага и добавляется в руку игроку
                else
                {
                    int n = (int)Random.Range(0, lc.PlaeyrsDeck.Count);
                    lc.PlayersHand.Add(lc.PlaeyrsDeck[n]);
                    lc.PlaeyrsDeck.RemoveAt(n);
                }   //Берется карта из коллоды и добавляется в руку(Игрок)
            }
            else
            {
                if (isTargetApponent)
                {
                    int n = (int)Random.Range(0, lc.PlaeyrsDeck.Count);
                    lc.EnemysHand.Add(lc.PlaeyrsDeck[n]);
                    lc.PlaeyrsDeck.RemoveAt(n);
                }   //Берется карта из коллоды игрока и добавляется в руку врагу
                else
                {
                    int n = (int)Random.Range(0, lc.EnemysDeck.Count);
                    lc.EnemysHand.Add(lc.EnemysDeck[n]);
                    lc.EnemysDeck.RemoveAt(n);
                }   //Берется карта из коллоды и добавляется в руку(Враг)
            }
        }

        private void DisableCreature(Creature targetCr)
        {
            targetCr.IsActive = false;
        }
        #endregion

    }
}