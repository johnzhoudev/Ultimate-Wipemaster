using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeoutScreenManager : MonoBehaviour
{
    public GameObject wipeoutScreen;
    bool isWipeoutScreenEnabled;

    void Start()
    {
        // Disable screen on start
        wipeoutScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isWipeoutScreenEnabled && Input.GetButton("Jump"))
        {
            closeWipeoutScreen();
        }
    }

    public void openWipeoutScreen()
    {
        wipeoutScreen.SetActive(true);
        isWipeoutScreenEnabled = true;
    }

    void closeWipeoutScreen()
    {
        wipeoutScreen.SetActive(false);
        isWipeoutScreenEnabled = false;
    }
}
