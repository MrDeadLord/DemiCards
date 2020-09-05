using System;
using System.Collections;
using UnityEngine;

namespace DeadLords.Controllers
{
    public class DayLightController : BaseController
    {
        [SerializeField] [Tooltip("Глобальный источник света(Солнце)")] Transform _sun;

        private Light _light;
        private float _nowTime;
        //НЕОБХОДИМО УЗНАТЬ КАК ПОЛУЧИТЬ НАЧАЛЬНОЕ ПОЛОЖЕНИЕ СОЛНЦА
        private void Start()
        {
            _light = _sun.GetComponent<Light>();

            _nowTime = float.Parse(DateTime.Now.ToString("HH,mm"));

            int _timer = Mathf.RoundToInt(_nowTime);

            StartCoroutine(TimesMoving());
        }

        private void FixedUpdate()
        {

        }

        private IEnumerator TimesMoving()
        {
            
            yield return new WaitForSeconds(300);
        }
    }
}