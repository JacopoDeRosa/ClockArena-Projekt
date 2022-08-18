using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

        TMP_InputField[] inputFields = FindObjectsOfType<TMP_InputField>(true);

        foreach (TMP_InputField inputField in inputFields)
        {
            inputField.onSelect.AddListener(PlayLightClick);
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
    private void PlayLightClick(string useless)
    {
        _audioSource.PlayOneShot(_lightClick);
    }
}
