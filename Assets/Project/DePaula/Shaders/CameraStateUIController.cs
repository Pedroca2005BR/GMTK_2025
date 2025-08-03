using System.Collections;
using UnityEngine;

public class CameraStateUIController : MonoBehaviour
{
    #region Singleton

    public static CameraStateUIController instance;

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



    [SerializeField] GameObject recordAsset;
    [SerializeField] GameObject replayAsset;
    [SerializeField] GameObject rewindAsset;
    [SerializeField] GameObject pauseAsset;

    [SerializeField] float blinkTime = 1f;
    float timer = 0f;

    GameObject chosenAsset;


    public void RecordMode()
    {
        chosenAsset = recordAsset;
        replayAsset.SetActive(false);
        rewindAsset.SetActive(false);
        pauseAsset.SetActive(false);
    }

    public void RewindMode()
    {
        chosenAsset = rewindAsset;
        replayAsset.SetActive(false);
        recordAsset.SetActive(false);
        pauseAsset.SetActive(false);
    }

    public void ReplayMode()
    {
        chosenAsset = replayAsset;
        recordAsset.SetActive(false);
        rewindAsset.SetActive(false);
        pauseAsset.SetActive(false);
    }

    public void PauseMode()
    {
        chosenAsset = pauseAsset;
        recordAsset.SetActive(false);
        rewindAsset.SetActive(false);
        replayAsset.SetActive(false);
    }

    private void Update()
    {
        if (chosenAsset != null)
        {
            timer -= Time.deltaTime;
            
            if (timer < 0f)
            {
                Toggle();
            }
        }
    }

    private void Toggle()
    {
        chosenAsset.SetActive(!chosenAsset.activeInHierarchy);
        timer = blinkTime;
    }


    private void OnEnable()
    {
        EventManager.Subscribe("Record", PrepareRecord);
        EventManager.Subscribe("Pause", PreparePause);
        EventManager.Subscribe("Replay", PrepareReplay);
        EventManager.Subscribe("Rewind", PrepareRewind);
        EventManager.Subscribe("AtTimeMachine", PreparePause);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe("Record", PrepareRecord);
        EventManager.Unsubscribe("Pause", PreparePause);
        EventManager.Unsubscribe("Replay", PrepareReplay);
        EventManager.Unsubscribe("Rewind", PrepareRewind);
        EventManager.Unsubscribe("AtTimeMachine", PreparePause);
    }


    private void PrepareRecord(object parameter)
    {
        RecordMode();
    }

    private void PrepareRewind(object parameter)
    {
        RewindMode();
    }

    private void PreparePause(object parameter)
    {
        PauseMode();
    }

    private void PrepareReplay(object parameter)
    {
        ReplayMode();
    }
}
