using UnityEngine;

public class LV_Button : MonoBehaviour
{
    private bool doorOpen = false;
    [SerializeField] private LV_Door relatedDoor;
     public void PressButton()
    {
        doorOpen = !doorOpen;
        if (!doorOpen)
        {
            relatedDoor.OpenDoor();
        }
        else
        {
            relatedDoor.CloseDoor();
        }
        Debug.Log("Botão pressionado!");
    }
}
