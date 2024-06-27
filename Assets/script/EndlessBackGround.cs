using System.Collections.Generic;
using System.Drawing.Printing;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class EndlessBackGround : MonoBehaviour
{
    private GameObject Camera;
    private Vector3 CameraPosition;
    private List<float> XPositions;
    private List<float> Length;
    [SerializeField] private float[] ParallelEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        CameraPosition = Camera.transform.position;
        XPositions = GetXPostionInChildren(transform);
        Length = GetLengthInChildren(transform);
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (Transform trans in transform)
        {
            float XPosition = XPositions[i];
            float L = Length[i];
            float BackGroundMovement = (Camera.transform.position.x - CameraPosition.x) * ParallelEffect[i];
            float RelativeMovement = (Camera.transform.position.x - CameraPosition.x) * (1 - ParallelEffect[i]);
            trans.position = new Vector3(XPosition + BackGroundMovement, trans.position.y);
            if (RelativeMovement > XPosition + L)
            {
                XPositions[i] += L;
            }
            else if (RelativeMovement < XPosition - L)
            {
                XPositions[i] -= L;
            }
            i++;
        }
    }

    private static List<float> GetXPostionInChildren(Transform transform)
    {
        List<float> XPositionInChildren = new();
        foreach (Transform child in transform)
        {
            XPositionInChildren.Add(child.localPosition.x);
        }
        return XPositionInChildren;
    }
    private static List<float> GetLengthInChildren(Transform transform)
    {
        List<float> LengthInChildren = new();
        foreach (Transform child in transform)
        {
            LengthInChildren.Add(child.GetComponent<SpriteRenderer>().bounds.size.x);
        }
        return LengthInChildren;
    }

}
