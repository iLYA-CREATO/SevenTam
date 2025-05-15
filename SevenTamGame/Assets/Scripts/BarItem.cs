using System;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class BarItem : MonoBehaviour
{
    public static event Action OnWinGame;

    [SerializeField]
    private Transform BaseParent;

    [SerializeField]
    private List<Slots> slots;

    public int identity = 0;
    public ItemComponent itemComponentBuffer = null;
    public List<ItemComponent> itemDestroyed = new List<ItemComponent>();
    public int nextIndex = 0; // 
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
        ItemComponent itemComponent;
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
                itemComponent = null;
                break;
            }
        }
        CheckThree();


        if (identity == 3)
        {
            for (int j = 0; j < itemDestroyed.Count; j++)
            {
                Debug.Log($"Ты уничтожил: {itemDestroyed[j].typeItem}");
                Destroy(itemDestroyed[j].gameObject);
            }

            itemDestroyed.Clear();
            identity = 0;
        }

        if (BaseParent.childCount == 0)
            OnWinGame?.Invoke();
    }



  
    /// <summary>
    /// Метод который будет проверять 3 одинаковых предмета в баре
    /// </summary>
    private void CheckThree()
    {
        if (slots.Count < 3) return;

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
