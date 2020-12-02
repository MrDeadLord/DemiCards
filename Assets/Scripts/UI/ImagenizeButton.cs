using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeadLords
{
    /// <summary>
    /// Присвоение картинки кнопке по имени gameObject
    /// </summary>
    public class ImagenizeButton : MonoBehaviour
    {
        Button _button;
        List<Sprite> _sprites = new List<Sprite>();

        private void Start()
        {
            _button = GetComponent<Button>();
            _sprites = Main.Instance.GetObjectManager.GetCardsSprites;
        }

        /// <summary>
        /// Активация заполнения картинки
        /// </summary>
        public void Imagine()
        {
            foreach(Sprite sp in _sprites)
            {
                if (sp.name == gameObject.name)
                    _button.image.sprite = sp;
            }
        }
    }
}