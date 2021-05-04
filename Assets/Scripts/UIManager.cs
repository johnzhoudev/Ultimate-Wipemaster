using UnityEngine;
public enum ScreenState
{
    Normal,
    WipeoutScreen,
    EndGameScreen
}
public class UIManager : MonoBehaviour
{
    public GameObject wipeoutScreen;
    public GameObject buttonText;
    public GameObject endGameScreen;

    ScreenState screenState = ScreenState.Normal;

    public void openWipeoutScreen()
    {
        wipeoutScreen.SetActive(true);
        screenState = ScreenState.WipeoutScreen;
    }

    public void closeWipeoutScreen()
    {
        wipeoutScreen.SetActive(false);
        screenState = ScreenState.Normal;
    }

    public void openEndGameScreen() { 
        endGameScreen.SetActive(true);
        screenState = ScreenState.EndGameScreen;
    }

    public void closeEndGameScreen() { 
        endGameScreen.SetActive(false);
        screenState = ScreenState.Normal;
    }

    public ScreenState getScreenState() { return screenState; }
    public void setScreenState(ScreenState state) { screenState = state; }

    public bool isButtonTextActive() { return buttonText.activeInHierarchy;  }

    public void openButtonText() { buttonText.SetActive(true); }

    public void closeButtonText() { buttonText.SetActive(false); }

}
