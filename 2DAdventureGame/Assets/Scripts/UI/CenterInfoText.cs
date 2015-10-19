using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CenterInfoText : MonoBehaviour
{

    static Text _centredText;

    // Use this for initialization
    void Start()
    {
        _centredText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void ShowText(string text)
    {
        _centredText.text = text;
    }

    public static void HideText()
    {
        _centredText.text = "";
    }
}
