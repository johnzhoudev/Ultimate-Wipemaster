using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingGloveController : MonoBehaviour
{
    public Vector3 punchDirection;

    Transform boxingGloveTransform;
    float punchDistance = 1.9f;
    float punchTime = 1f;
    float punchVelocity;
    float retractTime = 1f;
    float retractVelocity;

    bool isPunching = false;
    bool isRetracting = false;

    void Start()
    {
        boxingGloveTransform = GetComponent<Transform>();
        punchVelocity = punchDistance / punchTime;
        retractVelocity = -1f * punchDistance / retractTime;
        punch();
    }

    void Update()
    {
        if (isPunching && isWithinPunchDistance())
        {
            boxingGloveTransform.Translate(punchDirection * punchVelocity * Time.deltaTime, Space.Self);
        }
        else if (isPunching && !isWithinPunchDistance())
        {
            isPunching = false;
            retract();
        }
        else if (isRetracting && isWithinPunchDistance())
        {
            boxingGloveTransform.Translate(punchDirection * retractVelocity * Time.deltaTime, Space.Self);
        }
        else if (isRetracting && !isWithinPunchDistance())
        {
            isRetracting = false;
            punch();
        }
    }

    bool isWithinPunchDistance()
    {
        return (0 <= boxingGloveTransform.localPosition.x && boxingGloveTransform.localPosition.x < punchDistance);
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
