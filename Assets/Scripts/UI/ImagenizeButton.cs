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
        List<Sprite> _images = new List<Sprite>();

        private void Start()
        {
            _button = GetComponent<Button>();
            _images = Main.Instance.GetObjectManager.GetCardsImages;
        }

        /// <summary>
        /// Активация заполнения картинки
        /// </summary>
        public void Imagine()
        {
            foreach (Sprite sp in _images)
            {
                if (sp.name == gameObject.name)
                {
                    _button.image.sprite = sp;
                }
            }
        }
    }
}