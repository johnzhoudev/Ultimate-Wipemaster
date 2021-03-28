using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeoutScreenManager : MonoBehaviour
{
    public GameObject wipeoutScreen;

    public void openWipeoutScreen() { wipeoutScreen.SetActive(true); }

    public void closeWipeoutScreen() { wipeoutScreen.SetActive(false); }
}

