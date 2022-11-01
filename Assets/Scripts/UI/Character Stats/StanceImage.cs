using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StanceImage : MonoBehaviour
{
    [SerializeField] private Sprite _crouchSprite, _standSprite;
    [SerializeField] private Image _image;

    public void SetStance(Stance stance)
    {
        if(stance == Stance.Prone)
        {
            _image.sprite = _crouchSprite;
        }
        else if(stance == Stance.Standing)
        {
            _image.sprite = _standSprite;
        }
    }

}
