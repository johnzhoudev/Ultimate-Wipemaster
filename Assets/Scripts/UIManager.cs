using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject wipeoutScreen;
    public GameObject buttonText;

    public void openWipeoutScreen() { wipeoutScreen.SetActive(true); }

    public void closeWipeoutScreen() { wipeoutScreen.SetActive(false); }

    public bool isButtonTextActive() { return buttonText.activeInHierarchy;  }

    public void openButtonText() { buttonText.SetActive(true); }

    public void closeButtonText() { buttonText.SetActive(false); }

}
