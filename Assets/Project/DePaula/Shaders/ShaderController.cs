using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

public class ShaderController : MonoBehaviour
{
    #region Singleton

    public static ShaderController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    [SerializeField] private Volume volume;
    private AnalogGlitchVolume analogGlitchVolume;


    private void Start()
    {
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
    }

    public void TurnOn()
    {
        analogGlitchVolume.active = true;
    }

    public void TurnOff()
    {
        analogGlitchVolume.active = false;
    }


    private void OnEnable()
    {
        EventManager.Subscribe("Replay", PrepareTurnOn);
        EventManager.Subscribe("Rewind", PrepareTurnOn);
        EventManager.Subscribe("TimeBackToNormal", PrepareTurnOff);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("Replay", PrepareTurnOn);
        EventManager.Unsubscribe("Rewind", PrepareTurnOn);
        EventManager.Unsubscribe("TimeBackToNormal", PrepareTurnOff);
    }


    private void PrepareTurnOn(object parameter)
    {
        TurnOn();
    }

    private void PrepareTurnOff(object parameter)
    {
        TurnOff();
    }
}
