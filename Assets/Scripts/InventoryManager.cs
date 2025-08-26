using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public int maxStack = 99; //o máximo que eu posso stackar por slot
    public InventorySlot[] inventorySlot; //os slots do meu inventário
    public GameObject itemPrefab; //prefab do item
    public bool dragging; //se estou arrastando um objeto

    public static InventoryManager instance; //criando um singleton, para que eu possa acessar esse script de qualquer lugar

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    

   
    public void AddNewItem(Item item, int itemCount) //adicionar um novo item no inventário
    {
        

        if (item.stackable)//checando se eu posso stackar o item
        {
            for (int i = 0; i < inventorySlot.Length; i++)// checando cada slot do inventário
            {
                InventorySlot slot = inventorySlot[i]; //atribuindo o slot do inventário a uma variável
                inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>(); //pegando o script "inventoryItem" do item presente no inventário
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count <= maxStack) //caso houver item no slot e se ele for igual ao item que estou tentando adicionar e se ele não estiver com a quantidade máxima
                {
                    itemInSlot.count += itemCount; //adiciona mais um
                    itemInSlot.RefreshCount(); //atualiza a contagem
                    return; //retorna o código

                }

            }
        }

        for (int i = 0; i < inventorySlot.Length; i++) // checando cada slot do inventário
        {
            InventorySlot slot = inventorySlot[i];//atribuindo o slot do inventário a uma variável
            inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>();//pegando o script "inventoryItem" do item presente no inventário
            if (itemInSlot == null) //se não houver nenhum item no slot
            {
                GetNewItem(item, slot, itemCount); //atribuo o item ao slot

                return; // retorno o código
            }

        }
        return; //essa função vai adicionar um item no primeiro slot desocupado do inventário, ou stackar com outro item, caso seja igual

        
    }

    public void GetNewItem(Item item, InventorySlot slot, int itemCount) //função para pegar um novo item
    {
        GameObject newItem = Instantiate(itemPrefab, slot.transform); //criar o item na engine
        inventoryItem invItem = newItem.gameObject.GetComponent<inventoryItem>(); //pegar o script "inventoryItem" desse objeto
        invItem.CreateItem(item, itemCount); //usar a função de criar item do "inventoryItem" para criar o objeto do item
        invItem.RefreshCount(); //atualizar a contagem dele
    }

    public bool checkFreeSlots(Item item)
    {
        if (item.stackable)//checando se eu posso stackar o item
        {
            for (int i = 0; i < inventorySlot.Length; i++)// checando cada slot do inventário
            {
                InventorySlot slot = inventorySlot[i]; //atribuindo o slot do inventário a uma variável
                inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>(); //pegando o script "inventoryItem" do item presente no inventário
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count <= maxStack) //caso houver item no slot e se ele for igual ao item que estou tentando adicionar e se ele não estiver com a quantidade máxima
                {

                    return true; //retorna o código

                }
            }
        }

            for (int i = 0; i < inventorySlot.Length; i++) // checando cada slot do inventário
        {
            InventorySlot slot = inventorySlot[i];//atribuindo o slot do inventário a uma variável
            inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>();//pegando o script "inventoryItem" do item presente no inventário
            if (itemInSlot == null) //se não houver nenhum item no slot
            {
                Debug.Log("o slot livre é o " + i);
                return true; // retorno o código
            }

        }
        return false;
    }
    public void GenerateItem(int id) 
    {
        //função usada para gerar o item no inventário, totalmente voltada para o propósito de teste, e não deve ser mantida no jogo final
        switch (id)
        {
            case 0:
                AddNewItem(Resources.Load<Item>("Sword"), 1);
                break;
            case 1:
                AddNewItem(Resources.Load<Item>("Potion"), 1);
                break;
            case 2:
                AddNewItem(Resources.Load<Item>("Shield"), 1);
                break;
        }
    }
}


    
