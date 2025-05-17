using System;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class BarItem : MonoBehaviour
{
    public static event Action OnWinGame;
    public static event Action OnFailGame;

    [SerializeField]
    private Transform BaseParent;

    [SerializeField]
    private List<Slots> slots;

    public int identity = 0;
    private ItemComponent itemComponentBuffer = null;
    private List<ItemComponent> itemDestroyed = new List<ItemComponent>();
    public int nextIndex = 0; // 

    /// <summary>
    /// расортировываются предметы 
    /// </summary>
    public List<ItemAllBar> typeItemAll = new List<ItemAllBar>();
    // Максимальное кол-во предметов в слотах
    [SerializeField]
    private int maxItemValue;
    private void OnEnable()
    {
        DragDrop.OnMoveItemToSlot += MoveToSlot;
    }

    private void OnDisable()
    {
        DragDrop.OnMoveItemToSlot -= MoveToSlot;
    }


    private void  MoveToSlot(GameObject item)
    {
        ItemComponent itemComponent = null;
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].slot.childCount != 0)
            {

            }
            else
            {
                item.transform.SetParent(slots[i].slot);
                item.transform.localPosition = Vector3.zero;
                item.transform.localRotation = Quaternion.Euler(0, 0, 0);
                itemComponent = item.GetComponent<ItemComponent>();
                slots[i].itemComponent = itemComponent;
                itemComponent.rb.gravityScale = 0f;
                itemComponent.rb.bodyType = RigidbodyType2D.Static;
                break;
            }
        }
        

        for (int j = 0; j < typeItemAll.Count; j++)
        {
            if (typeItemAll[j].typeItem == itemComponent.typeItem)
            {
                typeItemAll[j].itemGame.Add(itemComponent);
            }

           
        }

        for (int j = 0; j < typeItemAll.Count; j++)
        {
            if (typeItemAll[j].itemGame.Count > 2)
            {
                for (int s = 0; s < typeItemAll[j].itemGame.Count; s++)
                {
                    Destroy(typeItemAll[j].itemGame[s].gameObject);
                    continue;
                }
                typeItemAll[j].itemGame.Clear();
            }
        }
        if (BaseParent.childCount == 0)
            OnWinGame?.Invoke();

        CheckThree();
        CheckFailGame();
    }

    private void CheckFailGame()
    {
        int itemValue = 0;
        for(int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemComponent != null)
            {
                itemValue++;
            }
            else
                return;
        }

        if(itemValue == maxItemValue)
        {
            OnFailGame?.Invoke();
        }
    }

    /// <summary>
    /// Вернём предметы из бара в игру 
    /// </summary>
    public void RetrnItemToGame()
    {
        for (int j = 0; j < slots.Count; j++)
        {
            if(slots[j].itemComponent != null)
            {
                slots[j].itemComponent.rb.bodyType = RigidbodyType2D.Dynamic;
                slots[j].itemComponent.rb.gravityScale = 100f;
                slots[j].itemComponent = null;
            }
        }

        for (int j = 0; j < itemDestroyed.Count; j++)
        {
            itemDestroyed[j].transform.SetParent(BaseParent);
        }

        itemDestroyed = null;
    }
    /// <summary>
    /// Метод который будет проверять 3 одинаковых предмета в баре
    /// </summary>
    private void CheckThree()
    {
        itemComponentBuffer = null;
        identity = 0;
        itemDestroyed.Clear();

        for (int i = 0; i < slots.Count; i++)
        {
            if (identity == 0)
            {
                itemComponentBuffer = slots[i].itemComponent;
                identity = 1;
                itemDestroyed.Add(itemComponentBuffer);
            }
            else
            {
                if(slots[i].itemComponent != null)
                {
                    if (itemComponentBuffer.typeItem == slots[i].itemComponent.typeItem)
                    {
                        identity++;
                        itemDestroyed.Add(slots[i].itemComponent);
                    }
                    else
                    {
                        nextIndex = i;
                    }
                }
            }
        }
    }
}

[Serializable]
public class Slots
{
    public Transform slot;
    public ItemComponent itemComponent;
}

[Serializable]
public class ItemAllBar
{
    public TypeItem typeItem;
    public List<ItemComponent> itemGame;
}
