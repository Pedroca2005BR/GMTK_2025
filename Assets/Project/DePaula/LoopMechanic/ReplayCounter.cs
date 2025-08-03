using TMPro;
using UnityEngine;

public class ReplayCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    private void OnEnable()
    {
        EventManager.Subscribe("ReplayTimeIncrease", SetText);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("ReplayTimeIncrease", SetText);

    }

    void SetText(object parameter)
    {
        float value = (float)parameter;

        _text.text = $"{value:0.00} seconds";
    }
}
