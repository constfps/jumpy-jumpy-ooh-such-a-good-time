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
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && !uiHandler.paused)
        {
            uiHandler.Pause();
        }

        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) && uiHandler.inSettings)
        {
            uiHandler.Backshots();
        }
    }
}
