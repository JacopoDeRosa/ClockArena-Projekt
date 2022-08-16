using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _heavyClick;
    [SerializeField] private AudioClip _lightClick;
    [SerializeField] private AudioSource _audioSource;


    private void Awake()
    {
        List<IClickAudio> clickables = FindInterfaces.Find<IClickAudio>();

        foreach (IClickAudio clickable in clickables)
        {
            if(clickable.ClickType == ClickTypes.Heavy)
            {
                clickable.onAudio += PlayHeavyClick;
            }
            else if(clickable.ClickType == ClickTypes.Light)
            {
                clickable.onAudio += PlayLightClick;
            }
        }

        Button[] buttons = FindObjectsOfType<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayLightClick);
        }
    }


    private void PlayHeavyClick()
    {
        _audioSource.PlayOneShot(_heavyClick);
    }
    private void PlayLightClick()
    {
        _audioSource.PlayOneShot(_lightClick);
    }
}
