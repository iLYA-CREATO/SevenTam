using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private int maxItemValue;

    [SerializeField]
    private GameObject prefabItem;

    /// <summary>
    /// Данные каждого зверя
    /// </summary>
    [SerializeField]
    private List<ItemData> itemData;

    /// <summary>
    /// расортировываются предметы 
    /// </summary>
    [SerializeField]
    private List<TypeItemAll> typeItemAll = new List<TypeItemAll>();

    TypeItemAll newItem = new TypeItemAll(); // Создаем новый элемент списка
    AnimalItemComponent animalItemComponent = new AnimalItemComponent();


    [SerializeField]
    private GameObject bufferInstantiate;
    [SerializeField]
    private Transform spawnPosition1;
    [SerializeField]
    private Transform spawnPosition2;


    int f = 0;

    [SerializeField]
    private float force;
    private void Start()
    {
        StartCoroutine(SpawnAllItems());
    }

    private void CheckItem()
    {
        int valueDop = 0;
        for(int i = 0; i < typeItemAll.Count; i++)
        {
            if (typeItemAll[i].itemGame.Count % 3 == 0)
            {

            }
            else
            {
                valueDop = typeItemAll[i].itemGame.Count % 3;
                valueDop = 3 - valueDop;

                DopItem(i, valueDop);
                continue;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"> Индекс предмета</param>
    /// <param name="valueDop"> Количество которое нужно заспавнить</param>
    private void DopItem(int index, int valueDop)
    {
        SpawnDopItems(index, valueDop);
    }
    private IEnumerator SpawnAllItems()
    {
        for (int i = 0; i < maxItemValue; i++)
        {
            yield return new WaitForSeconds(0.4f); 
            StartCoroutine(Spawner(i, false));
        }
        //CheckItem();
    }

    private void SpawnDopItems(int i, int valueDop)
    {
        for(int j = 0; j < valueDop; j++)
        {
            StartCoroutine(Spawner(i, true));
        }
    }
    private IEnumerator Spawner(int i, bool dopSpawn)
    {
        if (i % 2 == 0)
            bufferInstantiate = Instantiate(prefabItem, spawnPosition1);
        else
            bufferInstantiate = Instantiate(prefabItem, spawnPosition2);



        animalItemComponent = bufferInstantiate.GetComponent<AnimalItemComponent>();
        //animalItemComponent.ActivatorTrigger();
        if(dopSpawn)
        {
            animalItemComponent._SetTypeAnimal(itemData[i].typeItem);
            animalItemComponent._SetSpriteAnimal(itemData[i].spriteItem);
        }
        else
        {
            animalItemComponent._SetTypeAnimal(itemData[f].typeItem);
            animalItemComponent._SetSpriteAnimal(itemData[f].spriteItem);
        }
       


        int rnd = UnityEngine.Random.Range(0, 2);
        if (rnd == 0)
        {
            animalItemComponent.rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
        }
        else if (rnd == 1)
        {
            animalItemComponent.rb.AddForce(Vector2.down * force, ForceMode2D.Impulse);
        }
        else if (rnd == 2)
        {
            animalItemComponent.rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        }

        f++;
        if (f >= 6)
        {
            f = 0;
        }

        bool found = false;

        if (typeItemAll.Count == 0)
        {
            newItem = new TypeItemAll();

            // Заполняем данные
            newItem.itemGame = new List<AnimalItemComponent>();
            newItem.itemGame.Add(animalItemComponent);
            newItem.typeItem = animalItemComponent.typeItem;

            typeItemAll.Add(newItem);
            newItem = null;
        }
        else if (typeItemAll.Count > 0)
        {
            for (int j = 0; j < typeItemAll.Count; j++)
            {
                if (typeItemAll[j].typeItem == animalItemComponent.typeItem)
                {
                    // Заполняем данные
                    typeItemAll[j].itemGame.Add(animalItemComponent);
                    found = true; // Устанавливаем флаг в true, так как элемент найден
                    continue;
                }
            }

            if (!found)
            {
                newItem = new TypeItemAll();
                // Заполняем данные
                newItem.itemGame = new List<AnimalItemComponent>();
                newItem.itemGame.Add(animalItemComponent);
                newItem.typeItem = animalItemComponent.typeItem;
                typeItemAll.Add(newItem);
                newItem = null;
            }
        }


        yield return null;
    }
}

[Serializable]
public class ItemData
{
    public TypeItem typeItem;
    public Sprite spriteItem;
}
[Serializable]
public class TypeItemAll
{
    public TypeItem typeItem;
    public List<AnimalItemComponent> itemGame;
}
