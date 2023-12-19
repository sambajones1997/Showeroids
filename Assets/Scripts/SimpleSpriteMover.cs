using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpriteMover : MonoBehaviour
{
    public enum EMoveBehaviour { Move, Loop, Destroy }
    public enum ESimpleMoverState { Idle, Delay, Moving, LoopingForward, LoopingBackward };
    [SerializeField] float mDelay = 0.0f;
    [SerializeField] float mSpriteSpeed = 2.0f;
    [SerializeField] float mMoveTimer = 2.0f;
    [SerializeField] Vector3 mMoveDirection = Vector3.down;
    [SerializeField] EMoveBehaviour mMoveBehaviour = EMoveBehaviour.Move;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        mDelay -= Time.deltaTime;
        if (mDelay <= 0.0f)
        {
            transform.Translate(mMoveDirection * mSpriteSpeed * Time.deltaTime);

            mMoveTimer -= Time.deltaTime;
            if (mMoveTimer <= 0.0f)
            {
                Destroy(gameObject);
            }
        }

    }

    void ProcessState()
    {

    }

    void DelayState()
    {
        mDelay -= Time.deltaTime;
        if (mDelay <= 0.0f)
        {
            
        }
    }
}
