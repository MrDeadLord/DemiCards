using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeadLords
{
    public class SaveLoad : MonoBehaviour
    {
        string pathInv = Application.dataPath + "/Inventory";     //Путь к сохранению инвентаря
        //string pathCards = Application.dataPath + "/Cards";   //Путь к сохранению карт. Возможно нужно сохранять список всех возможных карт в игре
        string pathStats = Application.dataPath + "/Stats";   //Путь к сохранению BaseStats

        Inventory inv = Main.Instance.GetObjectManager.Player.GetComponent<Inventory>();

        public void SaveInv()
        {

        }
    }
}