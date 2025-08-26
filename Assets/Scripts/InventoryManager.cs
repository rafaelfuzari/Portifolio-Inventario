using Unity.VisualScripting;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public int maxStack = 99; //o m�ximo que eu posso stackar por slot
    public InventorySlot[] inventorySlot; //os slots do meu invent�rio
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
    

   
    public void AddNewItem(Item item, int itemCount) //adicionar um novo item no invent�rio
    {
        

        if (item.stackable)//checando se eu posso stackar o item
        {
            for (int i = 0; i < inventorySlot.Length; i++)// checando cada slot do invent�rio
            {
                InventorySlot slot = inventorySlot[i]; //atribuindo o slot do invent�rio a uma vari�vel
                inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>(); //pegando o script "inventoryItem" do item presente no invent�rio
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count <= maxStack) //caso houver item no slot e se ele for igual ao item que estou tentando adicionar e se ele n�o estiver com a quantidade m�xima
                {
                    itemInSlot.count += itemCount; //adiciona mais um
                    itemInSlot.RefreshCount(); //atualiza a contagem
                    return; //retorna o c�digo

                }

            }
        }

        for (int i = 0; i < inventorySlot.Length; i++) // checando cada slot do invent�rio
        {
            InventorySlot slot = inventorySlot[i];//atribuindo o slot do invent�rio a uma vari�vel
            inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>();//pegando o script "inventoryItem" do item presente no invent�rio
            if (itemInSlot == null) //se n�o houver nenhum item no slot
            {
                GetNewItem(item, slot, itemCount); //atribuo o item ao slot

                return; // retorno o c�digo
            }

        }
        return; //essa fun��o vai adicionar um item no primeiro slot desocupado do invent�rio, ou stackar com outro item, caso seja igual

        
    }

    public void GetNewItem(Item item, InventorySlot slot, int itemCount) //fun��o para pegar um novo item
    {
        GameObject newItem = Instantiate(itemPrefab, slot.transform); //criar o item na engine
        inventoryItem invItem = newItem.gameObject.GetComponent<inventoryItem>(); //pegar o script "inventoryItem" desse objeto
        invItem.CreateItem(item, itemCount); //usar a fun��o de criar item do "inventoryItem" para criar o objeto do item
        invItem.RefreshCount(); //atualizar a contagem dele
    }

    public bool checkFreeSlots(Item item)
    {
        if (item.stackable)//checando se eu posso stackar o item
        {
            for (int i = 0; i < inventorySlot.Length; i++)// checando cada slot do invent�rio
            {
                InventorySlot slot = inventorySlot[i]; //atribuindo o slot do invent�rio a uma vari�vel
                inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>(); //pegando o script "inventoryItem" do item presente no invent�rio
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count <= maxStack) //caso houver item no slot e se ele for igual ao item que estou tentando adicionar e se ele n�o estiver com a quantidade m�xima
                {

                    return true; //retorna o c�digo

                }
            }
        }

            for (int i = 0; i < inventorySlot.Length; i++) // checando cada slot do invent�rio
        {
            InventorySlot slot = inventorySlot[i];//atribuindo o slot do invent�rio a uma vari�vel
            inventoryItem itemInSlot = slot.GetComponentInChildren<inventoryItem>();//pegando o script "inventoryItem" do item presente no invent�rio
            if (itemInSlot == null) //se n�o houver nenhum item no slot
            {
                Debug.Log("o slot livre � o " + i);
                return true; // retorno o c�digo
            }

        }
        return false;
    }
    public void GenerateItem(int id) 
    {
        //fun��o usada para gerar o item no invent�rio, totalmente voltada para o prop�sito de teste, e n�o deve ser mantida no jogo final
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


    
