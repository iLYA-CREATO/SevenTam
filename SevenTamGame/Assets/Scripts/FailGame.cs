using UnityEngine;

public class FailGame : MonoBehaviour
{
    [SerializeField]
    private GameObject panelFailGame;
    private void OnEnable()
    {
        BarItem.OnFailGame += OnFailGame;
    }

    private void OnDisable()
    {
        BarItem.OnFailGame -= OnFailGame;
    }

    public void OnFailGame()
    {
        Debug.Log("Вы выйграли");
        panelFailGame.SetActive(true);
    }
}
