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
        mSpriteRenderer.sprite = mDestinationSprites[GameData.GetLevelIndex()];
    }

    public int GetNumDestinations() {  return mDestinationSprites.Count; }
}
