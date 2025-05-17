using UnityEngine;

public class AnimalItemComponent : ItemComponent
{
    public void _SetSpriteAnimal(Sprite spriteAnimal)
    {
        SetImageAnimal(spriteAnimal);
    }
    public void _SetTypeAnimal(TypeItem typeItem)
    {
        SetTypeAnimal(typeItem);
    }

    public void ActivatorTrigger()
    {
        StartCoroutine(TriggerActivate());
    }

    public void DizactivItem(bool isActiv)
    {
        if(item != null)
        {
            item.SetActive(isActiv);
        }
    }

    public void MoveToPosition(Transform position)
    {
        item.transform.position = position.position;
        item.transform.SetParent(position);
    }
}
