using UnityEngine;

public class UIManager : MonoBehaviour
{

    public WipeoutScreenManager wipeoutScreenManager;

    void Start()
    {
        wipeoutScreenManager = gameObject.GetComponent<WipeoutScreenManager>();    
    }
    public void openWipeoutScreen()
    {
        wipeoutScreenManager.openWipeoutScreen();
    }

}
