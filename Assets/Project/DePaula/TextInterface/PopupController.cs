using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    Animator animator;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    string textToShow;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PopIn(string text)
    {
        textToShow = text;
        animator.SetBool("PopIn", true);
    }

    public void PopOut()
    {
        animator.SetBool("PopIn", false);
    }

    public void ShowText()
    {
        textMeshProUGUI.text = textToShow;
    }

    public void HideText()
    {
        textMeshProUGUI.text = "";
    }
}
