using UnityEngine;
using DeadLords;

public class EndTurnButton : MonoBehaviour
{
    public void EndTurn()
    {
        Main.Instance.GetSceneLiveController.On();
        Main.Instance.GetSceneLiveController.EndOfTurn();
    }
}