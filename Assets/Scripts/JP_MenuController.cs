using UnityEngine;

public class JP_MenuController : MonoBehaviour
{
    public GameObject menuInicio;
    public GameObject menuOpcoes;

    private bool jaClicou = false;

    void Update()
    {
        if (!jaClicou && Input.anyKeyDown)
        {
            jaClicou = true;
            menuInicio.SetActive(false);
            menuOpcoes.SetActive(true);
        }
    }
}
