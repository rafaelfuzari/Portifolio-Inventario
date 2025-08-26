using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public Sprite image; //imagem do item
    public itemType Type; //tipo do item;

    public bool stackable; //se eu posso ter mais de um mesmo item no slot

    public string generalDesc; //descrição geral (de onde o item veio, o que ele faz, etc)
    public string techDesc; //descrição técnica(dano, defesa, cura, etc)
    public int defense; //atributo defesa do item
    public int damage; //atributo dano do item

    
    public GameObject droppedItemPrefab; //prefab que ele vai assumir, se dropado

}


public enum itemType //tipo do item
{
    Equipment,
    Consumable

}

