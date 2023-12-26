using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomiserLineUI : MonoBehaviour
{
    [SerializeField] RectTransform mLineEndPoint;

    LineRenderer mLineRenderer;

    private void Start()
    {
        mLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (mLineRenderer)
        {
            Vector3[] Positions = new Vector3[2];

            Positions[0] = Vector3.zero;
            if (mLineEndPoint)
            {
                Positions[1] = (mLineEndPoint.localPosition - mLineRenderer.transform.localPosition);
            }

            mLineRenderer.SetPositions(Positions);
        }
    }
}
