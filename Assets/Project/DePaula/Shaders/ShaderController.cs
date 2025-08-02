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
}
