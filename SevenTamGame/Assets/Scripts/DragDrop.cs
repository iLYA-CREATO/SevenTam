using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler
{
    public static event Action<GameObject> OnMoveItemToSlot;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // �������� �� ����� ������ ����
        {
            Debug.Log($"���� �����{gameObject.name}");

            OnMoveItemToSlot?.Invoke(gameObject); 
        }
    }
}
