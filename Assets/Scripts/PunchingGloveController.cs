using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingGloveController : MonoBehaviour
{
    public Vector3 punchDirection;

    Transform boxingGloveTransform;
    float punchDistance = 1.9f;
    float punchThreshold =  2.5f;
    float punchTime = 0.1f;
    float punchSpeed;
    float retractTime = 1f;
    float retractSpeed;

    bool isPunching = false;
    bool isRetracting = false;

    void Start()
    {
        boxingGloveTransform = GetComponent<Transform>();
        punchSpeed = punchDistance / punchTime;
        retractSpeed = punchDistance / retractTime;
        punch();
    }

    void Update()
    {
        // Handle Punching
        if (isPunching && isWithinPunchDistance())
        {
            // check if punch will go way out of zone...happens on start
            if (boxingGloveTransform.localPosition.x + punchSpeed * Time.deltaTime > punchThreshold) { return; }
            boxingGloveTransform.Translate(punchDirection * punchSpeed * Time.deltaTime, Space.Self);
        }
        else if (isPunching && !isWithinPunchDistance())
        {
            isPunching = false;
            retract();
        }
        else if (isRetracting && isWithinRetractDistance())
        {
            boxingGloveTransform.Translate(punchDirection * -1f * retractSpeed * Time.deltaTime, Space.Self);
        }
        else if (isRetracting && !isWithinRetractDistance())
        {
            isRetracting = false;
            punch();
        }
    }

    bool isWithinPunchDistance()
    {
        return boxingGloveTransform.localPosition.x < punchDistance;
    }

    bool isWithinRetractDistance()
    {
        return boxingGloveTransform.localPosition.x > 0;
    }

    public void punch()
    {
        isPunching = true;
    } 

    public void retract()
    {
        isRetracting = true;
    }

}
