using System.Collections.Generic;
using UnityEngine;
using DeadLords.Controllers;

namespace DeadLords
{
    public class CardActivator : MonoBehaviour
    {
        Card _card;
        SpawnController _sc;

        private void Start()
        {
            _card = GetComponent<Card>();
            _sc = Main.Instance.GetSpawnController;
        }

        /// <summary>
        /// Активация карты. Независимо от типа
        /// </summary>
        public void ActivateCard()
        {
            if (_card.CardsCreature != null)
                Summon();
            else
                CastSpell();
        }

        //SUMMON и ACTIVATE ВЫНЕСТИ В ОТДЕЛЬНЫЙ КЛАСС
        /// <summary>
        /// Призыв существа
        /// </summary>
        void Summon()
        {
            if (_sc.CanSpawn(transform.parent.tag))
            {
                _sc.Spawn(_card.CardsCreature.gameObject, transform.parent.tag);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Too many creatures allready");
            }
        }

        /// <summary>
        /// Активация заклинания
        /// </summary>
        void CastSpell()
        {

        }
    }
}