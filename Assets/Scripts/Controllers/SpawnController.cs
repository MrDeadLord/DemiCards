using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class SpawnController : BaseController
    {
        /// <summary>
        /// Точки спауна существ игрока
        /// </summary>
        private Transform[] _spawnPlayer;
        /// <summary>
        /// Точки спауна существ врага
        /// </summary>
        private Transform[] _spawnEnemy;

        /// <summary>
        /// Точка, где появится призываемое существо
        /// </summary>
        private Vector3 _spawnPoint;
        private int _indexP = 0, _indexE = 0;

        /// <summary>
        /// Точки, где есть существа игрока
        /// </summary>
        private List<Vector3> _exPlPoints = new List<Vector3>();
        /// <summary>
        /// Точки где есть существа врага
        /// </summary>
        private List<Vector3> _exEnemPoints = new List<Vector3>();

        private void Start()
        {
            _spawnPlayer = Main.Instance.GetObjectManager.PlayerSpawnPoints.GetComponentsInChildren<Transform>();
            _spawnEnemy = Main.Instance.GetObjectManager.EnemySpawnPoints.GetComponentsInChildren<Transform>();
        }

        /// <summary>
        /// Проверка можно ли спаунить существо
        /// </summary>
        /// <param name="summoner">Тэг призывателя(Player/Enemy)</param>
        /// <returns></returns>
        public bool CanSpawn(string summoner)
        {
            if (summoner == "Player")
            {
                if (_exPlPoints.Count == _spawnPlayer.Length)
                    return false;
                else
                    return true;
            }
            else
            {
                if (_exEnemPoints.Count == _spawnEnemy.Length)
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
                    if (_indexP < _spawnPlayer.Length)
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
                while (_exEnemPoints.Contains(_spawnEnemy[_indexE].position))
                {
                    if (_indexE < _spawnEnemy.Length)
                        _indexE++;
                    else
                        _indexE = 0;
                }

                _spawnPoint = _spawnEnemy[_indexE].transform.position;

                GameObject newCr = Instantiate(spawnObj, _spawnPoint, Quaternion.identity);
                newCr.tag = "CreatureEnemy";
                _exEnemPoints.Add(newCr.transform.position);
            }
        }

        /// <summary>
        /// Удаление существа с поля боя
        /// </summary>
        /// <param name="target">Умирающее существо</param>
        public void Death(GameObject target)
        {
            if (target.tag == "CreaturePlayer")
                _exPlPoints.Remove(target.transform.position);
            else
                _exEnemPoints.Remove(target.transform.position);
        }
    }
}