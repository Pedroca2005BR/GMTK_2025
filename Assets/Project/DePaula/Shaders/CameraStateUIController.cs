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

    //IEnumerator BlinkCoroutine()
    //{
    //    while (true)
    //    {
    //        chosenAsset.SetActive(true);
    //        yield return new WaitForSeconds(blinkTime);
    //        chosenAsset.SetActive(false);
    //        yield return new WaitForSeconds(blinkTime);
    //    }
    //}
}
