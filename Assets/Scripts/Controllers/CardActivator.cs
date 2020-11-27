using UnityEngine;

namespace DeadLords.Controllers
{
    public class CardActivator : BaseController
    {
        [SerializeField] Hand _hand;
        Card _card;
        SpawnController _sc;

        private void Start()
        {
            _card = GetComponent<Card>();
            _sc = Main.Instance.GetSpawnController;
        }

        public override void On()
        {
            Enabled = true;

            _hand.RemoveCard(_card);

            if (_card.CardsCreature != null)
                Summon();
            else
                ActivateBonus();
        }

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
        void ActivateBonus()
        {
        }
    }
}