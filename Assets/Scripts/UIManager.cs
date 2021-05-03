using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject wipeoutScreen;

    public void openWipeoutScreen() { wipeoutScreen.SetActive(true); }

    public void closeWipeoutScreen() { wipeoutScreen.SetActive(false); }

}
