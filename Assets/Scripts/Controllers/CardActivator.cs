using UnityEngine;

namespace DeadLords.Controllers
{
    public class CardActivator : BaseController
    {
        #region ========== Variables ========

        bool _targetSet = false;
        SpawnController _sc;
        Hand _hand;
        Card _actCard;
        TargetSelector _targSel;

        #endregion ========== Variables ========

        private void Start()
        {
            _sc = Main.Instance.GetSpawnController;
            _hand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
            _actCard = Main.Instance.GetObjectManager.ActivatingCard;
            _targSel = Main.Instance.GetTargetSelector;
            _targetSet = Main.Instance.GetObjectManager.TargetSet;
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Устранение вечной ошибки следующего if()
            if (Input.touches.Length == 0)
                return;

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                if (_targetSet)
                    ActivateCard();
                else
                    Cancel();
            }
        }

        #region ========== Methods ========

        public void PreActivate(Card activatingCard)
        {
            On();

            _actCard.Enable(activatingCard);

            GetComponent<Animator>().SetTrigger("Selected");

            _targSel.EnableSelection(activatingCard);
        }

        /// <summary>
        /// Активация карты. Независимо от типа
        /// </summary>
        public void ActivateCard()
        {
            Off();

            // Непосредственно активация действия карты
            Debug.Log("Activating card: " + _actCard.CardsData);
            if (_actCard.CardsData.creatureName != string.Empty)
                Summon(_actCard.CardsCreature);
            else
                CastSpell(_actCard);

            // Удаление карты с рук
            if (_hand.Cards.Contains(_actCard))
            {
                _hand.Cards.Remove(_actCard);
                Debug.Log("Card " + _actCard.CardsData.cardName + " deleted");
                Debug.Log("now we have(cards): " + _hand.Cards.Count);
            }

            _hand.TakingCards(0);
        }

        void Cancel()
        {
            Off();

            _actCard.Disable();

            GetComponent<Animator>().SetTrigger("Deselect");

            _targSel.Off();
        }

        /// <summary>
        /// Призыв существа
        /// </summary>
        void Summon(Creature creature)
        {
            if (_sc.CanSpawn("Player"))
            {
                _sc.Spawn(creature.gameObject, "Player");
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

        #endregion ========== Methods ========
    }
}