using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSelector : MonoBehaviour
{
    [SerializeField] SpriteRenderer mSpriteRenderer;
    [SerializeField] List<Sprite> mDestinationSprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        mSpriteRenderer.sprite = mDestinationSprites[Random.Range(0, mDestinationSprites.Count - 1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
