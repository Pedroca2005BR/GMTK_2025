using TMPro;
using UnityEngine;

public class TextInterface : MonoBehaviour
{
    [SerializeField]
    [TextArea] string text;
    [SerializeField] PopupController tabletOrPostit;

    public void ShowText()
    {
        tabletOrPostit.PopIn(text);
    }
}
