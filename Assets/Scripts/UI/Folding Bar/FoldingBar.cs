using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldingBar : MonoBehaviour
{
    [SerializeField] private float _sinkAmount;
    [SerializeField] private float _sinkSpeed = 1;

    private bool _busy;
    private bool _open;


    private void Awake()
    {
        _open = true;
    }
    public void Toggle(bool status)
    {
        if (_busy) return;
        if(status == true && _open == false)
        {
            _open = true;
          StartCoroutine(MoveToPosition(0));

        }
        else if(status == false && _open)
        {
            _open = false;
            StartCoroutine(MoveToPosition(_sinkAmount));
        }
    }

    private IEnumerator MoveToPosition(float position)
    {
        _busy = true;

        float t = 0;

        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x, position, start.z);

        var wait = new WaitForFixedUpdate();
     

        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, end, t);
            t += Time.fixedDeltaTime * _sinkSpeed;
            yield return wait;
        }

        _busy = false;
    }



}
