using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTurnGraphic : MonoBehaviour
{
    [SerializeField] private Fader _fader;

    private void OnEnable()
    {
        StartCoroutine(ShowGraphicRoutine());
    }

    private IEnumerator ShowGraphicRoutine()
    {
        yield return _fader.FadeOutRoutine();
        yield return new WaitForSeconds(1);
        yield return _fader.FadeInRoutine();
       
        gameObject.SetActive(false);
    }

}
