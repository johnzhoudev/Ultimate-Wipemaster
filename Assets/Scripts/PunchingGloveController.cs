using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchingGloveController : MonoBehaviour
{
    public Vector3 punchDirection;
    [Tooltip("Punch delay timings in seconds after fully retracted")]
    public float[] punchDelays;

    Transform boxingGloveTransform;
    float punchDistance = 1.9f;
    float punchThreshold =  2.5f;
    float punchTime = 0.1f;
    float punchSpeed;
    float retractTime = 1f;
    float retractSpeed;

    int punchDelayIndex = 0;
    float timeElapsed = 0f;


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
        // Update time elapsed
        timeElapsed += Time.deltaTime;

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
            timeElapsed = 0;
        }
        else if (punchDelays.Length == 0) { punch(); }
        else if (timeElapsed > punchDelays[punchDelayIndex % punchDelays.Length]) {
            punch();
            ++punchDelayIndex;
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

    void punch()
    {
        isPunching = true;
    } 

    void retract()
    {
        isRetracting = true;
    }

}
