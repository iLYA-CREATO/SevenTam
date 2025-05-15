using UnityEngine;

public class WinGame : MonoBehaviour
{

    [SerializeField]
    private GameObject panelWin;
    private void OnEnable()
    {
        BarItem.OnWinGame += Win;
    }

    private void OnDisable()
    {
        BarItem.OnWinGame -= Win;
    }

    public void Win()
    {
        Debug.Log("Вы выйграли");
        panelWin.SetActive(true);
    }
}
