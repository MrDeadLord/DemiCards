using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace DeadLords
{
    public class SaveLoad : MonoBehaviour
    {
        string pathInv = Application.dataPath + "/SaveData/Inventory-";     //Путь к сохранению инвентаря
        string pathCards = Application.dataPath + "/SaveData/Cards-";   //Путь к сохранению карт
        string pathStats = Application.dataPath + "/SaveData/Stats-";   //Путь к сохранению BaseStats

        #region Переменные
        List<Card> _playersHand = Main.Instance.GetSceneLiveController.PlayersHand;
        List<Card> _enemysHand = Main.Instance.GetSceneLiveController.EnemysHand;

        List<Card> _playersDeck = Main.Instance.GetSceneLiveController.PlaeyrsDeck;
        List<Card> _enemysDeck = Main.Instance.GetSceneLiveController.EnemysDeck;

        List<Card> _playersPlayedCards = Main.Instance.GetSceneLiveController.PlayersPlayedCards;
        List<Card> _enemysPlayedCards = Main.Instance.GetSceneLiveController.EnemysPlayedCards;

        List<Creature> _playersDeadCr = Main.Instance.GetSceneLiveController.PlayersDeadCr;
        List<Creature> _EnemysDeadCr = Main.Instance.GetSceneLiveController.EnemysDeadCr;

        /// <summary>
        /// Точки, где есть существа игрока
        /// </summary>
        List<Vector3> _playersExCr = Main.Instance.GetSpawnController.PlayersExCr;
        /// <summary>
        /// Точки где есть существа врага
        /// </summary>
        List<Vector3> _enemysExCr = Main.Instance.GetSpawnController.EnemysExCr;
        #endregion

        Inventory inv = Main.Instance.GetObjectManager.Player.GetComponent<Inventory>();

        public void XmlSaveCards(string saveName)
        {
            //ДОРАБОТАТЬ. Публичные поля и все такое. Возможно доделать взаимодействие с CardsData & BonusData
            XmlDocument doc = new XmlDocument();

            XmlNode plHand = doc.CreateElement("Player's hand");
            doc.AppendChild(plHand);

            XmlSerializer ser = new XmlSerializer(typeof(List<Card>));
            
            using(FileStream fs = new FileStream(pathCards + saveName, FileMode.Create))
            {
                ser.Serialize(fs, _playersHand);
            }

            using (FileStream fs = new FileStream(pathCards + saveName, FileMode.Append))
            {
                ser.Serialize(fs, _enemysHand);
                ser.Serialize(fs, _playersDeck);
                ser.Serialize(fs, _enemysDeck);
                ser.Serialize(fs, _playersPlayedCards);
                ser.Serialize(fs, _enemysPlayedCards);
            }
        }
    }
}