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

        private void Start()
        {
            _anim = GetComponentInChildren<Animator>();
            _rend = GetComponentInChildren<Renderer>();

            Off();
        }

        private void Update()
        {
            if (!Enabled)
                return;

            //Ниженаписаное описать в инпут сонтролере
            /*Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == GetComponentInParent<Collider>())
                {
                    _anim.SetBool("Selected", true);
                    return;
                }                    
            }

            _anim.SetBool("Selected", false);*/
        }

        public override void On()
        {
            Enabled = true;

            _rend.enabled = true;
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