using DeadLords.Controllers;

namespace DeadLords
{
    /// <summary>
    /// Ход игрока
    /// </summary>
    public class PlayersTurn : BaseController
    {
        Hand _playersHand;
        BaseStats _bs;

        #region Unity-time
        private void Start()
        {
            _playersHand = Main.Instance.GetObjectManager.Player.GetComponent<Hand>();
            _bs = Main.Instance.GetObjectManager.Player.GetComponent<BaseStats>();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Перестаем выполнять, если коллода еще не загружена
            if (!Main.Instance.deckLoadedPl)
                return;

            //Взятие карт в руку
            _playersHand.TakingCards(_bs.CardsTake);
            
            //Влючение интерфейса выделения игрока
            Main.Instance.GetObjectManager.Player.GetComponentInChildren<Selector>().On();

            Off();
        }
        #endregion
    }
}