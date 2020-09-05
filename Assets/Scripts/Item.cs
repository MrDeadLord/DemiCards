using UnityEngine;

namespace DeadLords
{
    public class Item : MonoBehaviour
    {
        private string _name;
        [SerializeField] private enum _type { head, chest, shoulders, gloves, pants, shoes };

        private int _cost;        
    }
}
