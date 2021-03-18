﻿using DeadLords.Controllers;
using UnityEngine;

namespace DeadLords
{
    public class CardActivator : MonoBehaviour
    {
        SpawnController _sc;
        Hand _hand;

        private void Start()
        {
            _sc = Main.Instance.GetSpawnController;
            _hand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
        }

        /// <summary>
        /// Активация карты. Независимо от типа
        /// </summary>
        public void ActivateCard(Card cd)
        {

            if (cd.CardsData.creatureName != string.Empty)
                Summon(cd);
            else
                CastSpell(cd);

            if (_hand.Cards.Contains(cd))
            {
                _hand.Cards.Remove(cd);
            }
            else
                Debug.LogError("Hand doesn't contain activated card");

            _hand.TakingCards(0);
        }

        //SUMMON и ACTIVATE ВЫНЕСТИ В ОТДЕЛЬНЫЙ КЛАСС
        /// <summary>
        /// Призыв существа
        /// </summary>
        void Summon(Card card)
        {
            if (_sc.CanSpawn("Player"))
            {
                if (card.CardsCreature == null)
                    Debug.LogError("No creature on ActCard");
                else
                    _sc.Spawn(card.CardsCreature.gameObject, "Player");
            }
            else
            {
                Debug.LogError("Too many creatures allready");
            }
        }

        /// <summary>
        /// Активация заклинания
        /// </summary>
        void CastSpell(Card card)
        {
            Debug.Log("Spell activated");
        }
    }
}