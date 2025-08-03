using UnityEngine;

public class Gerador : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TurnOn()
    {
        animator.SetBool("On", true);
    }

    public void TurnOff()
    {
        animator.SetBool("On", false);
    }
}
