using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyBetweenScenes : MonoBehaviour
{
    private void Awake()
    {
        DoNotDestroyBetweenScenes[] doNotDestroyBetweenScenesObjects = FindObjectsOfType<DoNotDestroyBetweenScenes>();

        foreach (DoNotDestroyBetweenScenes doNotDestroyObj in doNotDestroyBetweenScenesObjects)
        {
            if (doNotDestroyObj != this && doNotDestroyObj.gameObject.name == gameObject.name)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void DestroyObject(float lifeSpan = 0.0f)
    {
        Destroy(gameObject, lifeSpan);
    }


}
