using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeadLords
{
    public class ImagenizeButton : MonoBehaviour
    {
        Button _button;
        Card _card;
        List<Sprite> _images = new List<Sprite>();

        bool done = false;

        private void Start()
        {
            _button = GetComponent<Button>();
            _card = GetComponent<Card>();
            _images = Main.Instance.GetObjectManager.GetCardsImages;
        }

        private void Update()
        {
            if (_button.enabled && !done)
            {
                foreach(Sprite sp in _images)
                {
                    if(_card.CardsData.cardName == sp.name)
                    {
                        _button.image.sprite = sp;
                        done = true;
                        break;
                    }
                }
            }
        }
    }
}