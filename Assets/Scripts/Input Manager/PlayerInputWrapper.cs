using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputWrapper : MonoBehaviour
{


    private void Awake()
    {
        if(FindObjectsOfType<PlayerInputWrapper>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
