using UnityEngine;

namespace DeadLords.Controllers
{
    /// <summary>
    /// Интерфейс выделения и выбора персонажа/существа
    /// </summary>
    public class Selector : BaseController
    {
        Animator _anim;
        Renderer _rend;
        Camera _cam;
        Collider _coll;

        private void Start()
        {
            _anim = GetComponentInChildren<Animator>();
            _rend = GetComponentInChildren<Renderer>();
            _cam = Camera.main;
            _coll = GetComponentInParent<Collider>();

            Off();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Если анимация не включена, то при косании пальцем цели - запускает эту анимацию
            if (!_anim.GetBool("Selected"))
            {
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == _coll)
                    {
                        _anim.SetBool("Selected", true);

                        return;
                    }
                }
            }
            
            if (_anim.GetBool("Selected"))
                Main.Instance.GetTargetSelector.targetSet = true;
        }

        public override void On()
        {
            Enabled = true;

            _rend.enabled = true;

            _anim.SetBool("Selected", false);
        }

        public override void Off()
        {
            Enabled = false;

            _anim.SetBool("Selected", false);

            _rend.enabled = false;            
        }

        /// <summary>
        /// Аниматор селектора
        /// </summary>
        public Animator Animator { get { return _anim; } }
    }
}