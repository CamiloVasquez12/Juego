using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundbetweenscenes : MonoBehaviour
{
    private soundbetweenscenes instance;
    public soundbetweenscenes Instance
    {
        get 
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if(instance!= null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
}
