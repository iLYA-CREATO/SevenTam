using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemComponent : MonoBehaviour
{
    protected GameObject item;
    public TypeItem typeItem;
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider2D;

    public Image imageAnimal;
    public Image imageFullRound;
    public Image imageRound;

    private void Start()
    {
        item = this.gameObject;
    }
    protected void SetImageAnimal(Sprite spriteAnimal)
    {
        imageAnimal.sprite = spriteAnimal;
    }
    protected void SetImageFullRound()
    {

    }
    protected void SetImageRound()
    {

    }

    protected void SetTypeAnimal(TypeItem typeItem)
    {
        this.typeItem = typeItem;
    }

    protected IEnumerator TriggerActivate()
    {
        circleCollider2D.isTrigger = true;
        yield return new WaitForSeconds(1f);
        circleCollider2D.isTrigger = false;
        Debug.Log("Конец карутины");
    }
}
