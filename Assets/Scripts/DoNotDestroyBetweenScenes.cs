using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyBetweenScenes : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyObject(float lifeSpan = 0.0f)
    {
        Destroy(gameObject, lifeSpan);
    }


}
