using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Color SelectedColor, NotSelectedColor; //cores de selecionado e n�o-selecionado
    public Image image; //imagem do slot do invent�rio


    public void Awake()
    {
        
        image = GetComponent<Image>();
    }

   
    public void OnDrop(PointerEventData eventData) //esse evento � chamado, caso algum objeto seja solto sob ele(utilizando as interfaces IBeginDragHandler, IDragHandler e IEndDragHandler
    {
        //esse evento � respons�vel por fazer a troca de slots do sistema Drag and Drop do invent�rio funcionar
        if (transform.childCount == 0) { // caso esse objeto n�o tenha nenhum filho
        GameObject dropped = eventData.pointerDrag; //objeto que estava sendo arrastado
        inventoryItem inventoryItem = dropped.GetComponent<inventoryItem>(); //o script "inventoryItem" que estava no item
        inventoryItem.parentAfterDrag = transform; //tornando o objeto filho desse objeto
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData) //caso o mouse entre no objeto
    {
        image.color = SelectedColor; //troco a cor para selecionado
        
    }
    public void OnPointerExit(PointerEventData eventData)//caso o mouse saia do objeto
    {
        image.color = NotSelectedColor; //troco a por para n�o-selecionado
       
    }

    private void OnDisable()
    {
        image.color = NotSelectedColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) // caso eu pressionar o bot�o direito do mouse
        {
            if (transform.childCount > 0) //caso eu tenha algum filho(serve para evitar o erro de "Out of bounds")
            {
                Transform item = transform.GetChild(0);
                if (item != null) //caso o item n�o seja nulo
                {
                    
                    inventoryItem inventoryItem = item.GetComponent<inventoryItem>(); //pego o script "inventoryItem" do item
                    inventoryItem.Use();//utilizo o item
                }
            }
        }
    }
}
