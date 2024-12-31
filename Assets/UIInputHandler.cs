using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    private UIHandler uiHandler;

    private void Start()
    {
        uiHandler = GetComponent<UIHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiHandler.inSettings)
        {
            uiHandler.Backshots();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && uiHandler.paused)
        {
            uiHandler.Resume();
        }
    }
}
