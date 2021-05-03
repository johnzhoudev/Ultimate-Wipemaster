using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Collider buttonTrigger;
    public Transform cameraTransform;
    public float maxViewDistance;

    public bool isButtonTriggerInView()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit raycastHit;
        return buttonTrigger.Raycast(ray, out raycastHit, maxViewDistance);
    }
}
