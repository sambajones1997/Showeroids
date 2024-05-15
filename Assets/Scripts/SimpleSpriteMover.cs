using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleSpriteMover : MonoBehaviour
{
    public enum EMoveBehaviour { Move, Loop, Destroy }
    public enum ESimpleMoverState { Idle, Delay, Moving };

    [Header("Movement Behaviour")]
    [SerializeField] float mDelay = 0.0f;
    [SerializeField] float mSpriteSpeed = 2.0f;
    [SerializeField] float mMoveTimer = 2.0f;
    [SerializeField] Vector3 mMoveDirection = Vector3.down;
    [SerializeField] GameObject mMoveTarget;
    [SerializeField] EMoveBehaviour mMoveBehaviour = EMoveBehaviour.Move;
    ESimpleMoverState mSimpleMoverState = ESimpleMoverState.Delay;

    [Header("Post Movement Behaviour")]
    [SerializeField] List<MonoBehaviour> mComponentsToActivate = new List<MonoBehaviour>();
    [SerializeField] List<GameObject> mObjectsToActivate = new List<GameObject>();
    [SerializeField] List<MonoBehaviour> mComponentsToDeActivate = new List<MonoBehaviour>();
    [SerializeField] List<GameObject> mObjectsToDeActivate = new List<GameObject>();
    [SerializeField] public UnityEvent SpriteMovementCompleted = new UnityEvent();

    float mMoveTimeRemaining = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        mMoveTimeRemaining = mMoveTimer;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessSimpleMoverState();
    }

    void ProcessSimpleMoverState()
    {
        switch (mSimpleMoverState)
        {
            case ESimpleMoverState.Idle:
            default:
                break;
            case ESimpleMoverState.Delay:
                DelayState();
                break;
            case ESimpleMoverState.Moving:
                MovingState();
            break;
        }
    }

    void DelayState()
    {
        mDelay -= Time.deltaTime;
        if (mDelay <= 0.0f)
        {
            mSimpleMoverState = ESimpleMoverState.Moving;
        }
    }

    void MovingState()
    {
        if (mDelay <= 0.0f && mMoveTimeRemaining > 0.0f)
        {
            if (mMoveTarget)
            {
                mMoveDirection = (mMoveTarget.transform.position - transform.position).normalized;
            }

            transform.Translate(mMoveDirection * mSpriteSpeed * Time.deltaTime);

            mMoveTimeRemaining -= Time.deltaTime;
            if (mMoveTimeRemaining <= 0.0f)
            {
                ActivateDeactivateComponents();
                switch (mMoveBehaviour)
                {
                    case EMoveBehaviour.Destroy:
                        Destroy(gameObject);
                        break;
                    case EMoveBehaviour.Move:
                        Destroy(this);
                        break;
                    case EMoveBehaviour.Loop:
                        mSpriteSpeed *= -1.0f;
                        mMoveTimeRemaining = mMoveTimer;
                        break;
                }
            }
        }
    }

    void ActivateDeactivateComponents()
    {
        SpriteMovementCompleted.Invoke();

        foreach (MonoBehaviour comp in mComponentsToActivate)
        {
            if (comp)
            {
                comp.enabled = true;
            }
        }
    }
}
