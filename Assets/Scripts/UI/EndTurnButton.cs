using UnityEngine;
using DeadLords;

public class EndTurnButton : MonoBehaviour
{
    public void EndTurn()
    {        
        Main.Instance.GetSceneLiveController.EndOfTurn();
        Main.Instance.GetInputController.Off();
    }
}