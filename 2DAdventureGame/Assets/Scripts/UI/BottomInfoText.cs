using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BottomInfoText : MonoBehaviour
{

    static Text _bottomText;

    // Use this for initialization
    void Start()
    {
        _bottomText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ShowText(string text)
    {
        _bottomText.text = text;
    }

    public static void HideText()
    {
        _bottomText.text = "";
    }
}
