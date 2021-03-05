using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousRotation : MonoBehaviour
{
    //public Transform objTransform;
    public float yRotationDegrees;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * yRotationDegrees * Time.deltaTime);
    }
}
