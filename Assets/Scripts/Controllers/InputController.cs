using System.Collections.Generic;
using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Смартфонное управление
    /// </summary>
    public class InputController : BaseController
    {
        private Camera _cam;
        private Gyroscope _gyro;
        private bool _gyroSupp;
        private List<Card> _cards;

        private void Start()
        {
            _cam = Camera.main;
            _gyroSupp = SystemInfo.supportsGyroscope;
            _gyro = Input.gyro;
        }

        private void Update()
        {
            
        }
    }
}