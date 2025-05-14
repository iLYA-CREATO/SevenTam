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
}
