using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteRotation : MonoBehaviour
{

    [SerializeField] float mSpriteRotationSpeed = 30.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, mSpriteRotationSpeed * Time.deltaTime);
    }
}
