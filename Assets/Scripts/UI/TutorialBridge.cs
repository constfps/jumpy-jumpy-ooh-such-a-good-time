using UnityEngine;

public class TutorialBridge : MonoBehaviour
{
    private UIHandler ui;

    private void Start()
    {
        ui = FindObjectOfType<UIHandler>();
    }

    public void aaaaaaa()
    {
        ui.toggleTutShown();
    }
}
