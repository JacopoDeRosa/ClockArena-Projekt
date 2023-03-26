using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExtendedUI
{
    public class MiniRadarMarker : UIElement
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private Sprite _defaultSprite;

        [SerializeField] private MiniRadar _parent;
         private MiniRadarItem _item;


        public void UpdatePosition()
        {
            Vector3 itemPosition = _item.transform.position / _parent.Scale;
            Vector3 position = new Vector3(itemPosition.x, itemPosition.z, 0);
            transform.localPosition = position;
        }

        public void Init(MiniRadarItem item)
        {
            if (item == null) return;
            _item = item;
            if(item.MarkerSprite != null) _image.sprite = item.MarkerSprite;
            _item.onMove += UpdatePosition;
            _item.onDisable += ResetMarker;
            UpdatePosition();
        }

        public void SetParent(MiniRadar radar)
        {
            _parent = radar;
        }

        

        private void ResetMarker()
        {
            if (_image != null) _image.sprite = _defaultSprite;
            _item = null;
            _parent.ResetMarker(this);
        }
    }
}
