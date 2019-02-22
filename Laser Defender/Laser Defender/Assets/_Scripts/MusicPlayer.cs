using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private static GameObject instance;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = gameObject;
        }
        else {
            Destroy(gameObject);
        }

    }

}
