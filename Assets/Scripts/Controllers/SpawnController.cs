using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class SpawnController : BaseController
    {
        #region Переменные

        /// <summary>
        /// Точки спауна существ игрока
        /// </summary>
        private List<Transform> _spawnPlayer = new List<Transform>();

        /// <summary>
        /// Точки спауна существ врага
        /// </summary>
        private List<Transform> _spawnEnemy = new List<Transform>();

        /// <summary>
        /// Точка, где появится призываемое существо
        /// </summary>
        private Vector3 _spawnPoint;

        /// <summary>
        /// Индексы для расставления существ
        /// </summary>
        private int _indexP = 0, _indexE = 0;

        /// <summary>
        /// Точки, где есть существа игрока
        /// </summary>
        private List<Vector3> _exPlPoints = new List<Vector3>();

        /// <summary>
        /// Точки где есть существа врага
        /// </summary>
        private List<Vector3> _exEnemysPoints = new List<Vector3>();
        #endregion

        #region Unity-time
        private void Start()
        {
            foreach (Transform tr in Main.Instance.GetObjectManager.PlayerSpawnPoints.GetComponentsInChildren<Transform>())
                _spawnPlayer.Add(tr);
            _spawnPlayer.RemoveAt(0);   //Почему-то точка родителя считается нулевой

            foreach (Transform trans in Main.Instance.GetObjectManager.EnemySpawnPoints.GetComponentsInChildren<Transform>())
                _spawnEnemy.Add(trans);
            _spawnEnemy.RemoveAt(0);
        }
        #endregion

        /// <summary>
        /// Проверка можно ли спаунить существо
        /// </summary>
        /// <param name="summoner">Тэг призывателя(Player/Enemy)</param>
        /// <returns></returns>
        public bool CanSpawn(string summoner)
        {
            if (summoner == "Player")
            {
                if (_exPlPoints.Count == _spawnPlayer.Count)
                    return false;
                else
                    return true;
            }
            else
            {
                if (_exEnemysPoints.Count == _spawnEnemy.Count)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Спаун существ
        /// </summary>
        /// <param name="spawnObj">Призываемое существо</param>
        /// <param name="summoner">Тэг призывателя(Player/Enemy)</param>
        public void Spawn(GameObject spawnObj, string summoner)
        {
            if (summoner == "Player")
            {
                //Проверяем точки по порядку, пока не найдем свободную
                while (_exPlPoints.Contains(_spawnPlayer[_indexP].position))
                {
                    if (_indexP < _spawnPlayer.Count)
                        _indexP++;
                    else
                        _indexP = 0;
                }

                _spawnPoint = _spawnPlayer[_indexP].transform.position;

                GameObject newCr = Instantiate(spawnObj, _spawnPoint, Quaternion.identity);
                newCr.tag = "CreaturePlayer";
                _exPlPoints.Add(newCr.transform.position);
            }
            else
            {
                while (_exEnemysPoints.Contains(_spawnEnemy[_indexE].position))
                {
                    if (_indexE < _spawnEnemy.Count)
                        _indexE++;
                    else
                        _indexE = 0;
                }

                _spawnPoint = _spawnEnemy[_indexE].transform.position;

                GameObject newCr = Instantiate(spawnObj, _spawnPoint, Quaternion.identity);
                newCr.tag = "CreatureEnemy";
                _exEnemysPoints.Add(newCr.transform.position);
            }
        }

        #region Получение перменных

        /// <summary>
        /// Список позиций живых существ игрока
        /// </summary>
        public List<Vector3> PlayersExCr { get { return _exPlPoints; } }

        /// <summary>
        /// Список позиций живых существ врага
        /// </summary>
        public List<Vector3> EnemysExCr { get { return _exEnemysPoints; } }
        #endregion
    }
}