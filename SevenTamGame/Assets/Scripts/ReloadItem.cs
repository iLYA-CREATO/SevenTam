using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadItem : MonoBehaviour
{
    [SerializeField]
    private ItemGenerator itemGenerator;
    [SerializeField]
    private BarItem barItem;

    public void MoveToPool()
    {
        if(itemGenerator.isGenerate == false)
        {
            for(int i = 0; i < itemGenerator.typeItemAll.Count; i++)
            {
                for (int j = 0; j < itemGenerator.typeItemAll[i].itemGame.Count; j++)
                {
                    if(itemGenerator.typeItemAll[i].itemGame[j] != null)
                    {
                        itemGenerator.typeItemAll[i].itemGame[j].DizactivItem(false);
                    }
                }
            }
            StartCoroutine(MoveToSpawn());
        }
    }

    
    public IEnumerator MoveToSpawn()
    {
        for (int i = 0; i < itemGenerator.typeItemAll.Count; i++)
        {
            for (int j = 0; j < itemGenerator.typeItemAll[i].itemGame.Count; j++)
            {
                yield return new WaitForSecondsRealtime(0.4f);
                itemGenerator.typeItemAll[i].itemGame[j].MoveToPosition(itemGenerator.spawnPosition);
                itemGenerator.typeItemAll[i].itemGame[j].DizactivItem(true);
            }
        }
        barItem.identity = 0;
        barItem.RetrnItemToGame();
        
    }
}
