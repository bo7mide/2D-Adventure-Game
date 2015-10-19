using UnityEngine;
using System.Collections;

public class ColliderShowHideText : MonoBehaviour
{

    public enum Action
    {
        show, hide
    }
    public enum TextToUse
    {
        center, bottom
    }
    public TextToUse textToUse = TextToUse.center;
    public Action action = Action.hide;
    public string TextToShow;

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (action == Action.hide)
            {
                if (textToUse == TextToUse.center)
                {
                    CenterInfoText.HideText();
                }
                else
                {
                    BottomInfoText.HideText();

                }
            }
            else
            {
                if (textToUse == TextToUse.center)
                {
                    CenterInfoText.ShowText(TextToShow);
                }
                else
                {
                    BottomInfoText.ShowText(TextToShow);
                }
            }
        }
    }
}
