using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class inventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;// imagem do item
    public Item item; // item que vai dar as propriedades ao slot
    public Text countText; //texto do contador que vai aparecer abaixo do item
    public int count = 1; // contador do item
    public Transform tooltipPanel; //transform do painel de descri��o
    public Text generalDesc; //descri��o geral do item
    public Text techDesc; // descri��o t�cnica do item
    public RectTransform tooltipRectTransform; //rect transform do painel de descri��o;
    public string itemState;
    public GameObject itemPrefab;
    private float[] possibleDropRadius = { 1, 1.2f, 1.4f, 1.5f, -1, -1.2f, -1.4f, -1.5f };
    public float dropRadius1;
    public float dropRadius2;

    [HideInInspector] public Transform parentAfterDrag; //salvando o pai pra devolver o item ao local de origem

    private Vector2 panelSize; //tamanho do painel de descri��o
    private Vector2 newPos; //coordenada para ajustar o painel de descri��o para caber na tela, se necess�rio

    public void Start()
    {
        dropRadius1 = possibleDropRadius[Random.Range(0,possibleDropRadius.Length)];
        dropRadius2 = possibleDropRadius[Random.Range(0, possibleDropRadius.Length)];
        tooltipPanel = transform.GetChild(1);
        generalDesc.text = item.generalDesc;
        techDesc.text = item.techDesc;
        tooltipRectTransform = tooltipPanel.gameObject.GetComponent<RectTransform>();
        itemPrefab = item.droppedItemPrefab;
    }

    public void Update()
    {
        if (count <= 0) //destruir o objeto e o painel de descri��o, caso o contador chegue a zero
        {
            Destroy(gameObject);
            Destroy(tooltipPanel.gameObject);
        }     
       
    }

    public void OnDisable()
    {
        tooltipPanel.gameObject.SetActive(false);
    }
    public void OnBeginDrag(PointerEventData eventData) //quando eu come�ar a arrastar
    {
        parentAfterDrag = transform.parent; //atribuindo o pai desse objeto a vari�vel
        transform.SetParent(transform.root); //atribuindo o av� desse objeto como pai dele
        transform.SetAsLastSibling(); //colocando ele em ultimo lugar na hierarquia, para aparecer acima de tudo
        tooltipPanel.SetParent(transform); //atribuindo o painel de descri��o como filho desse objeto
        tooltipPanel.gameObject.SetActive(false); //desativando o painel de descri��o durante o arrasto do objeto
        image.raycastTarget = false; //desativando o raycast do item
        InventoryManager.instance.dragging = true; //estou carregando um item
    }

    public void OnDrag(PointerEventData eventData) //enquanto eu estiver arrastando
    {
        transform.position = Input.mousePosition; //movimentando o item de acordo com a posi��o do mouse;
    }
    public void OnEndDrag(PointerEventData eventData) //quando eu acabar de arrastar
    {
        transform.SetParent(parentAfterDrag); //reatribuindo o pai do objeto, caso seja colocado fora de algum slot(caso seja colocado dentro de algum slot, o c�digo est� no script "InventorySlot")
        tooltipPanel.SetParent(transform); //definindo o pai do painel de descri��o como esse objeto
        image.raycastTarget = true; //reativando o raycast da imagem
        InventoryManager.instance.dragging = false; //n�o estou mais arrastando o objeto
        if (eventData.pointerEnter.gameObject.name.Equals("DarkBackground")){
            DropItem(itemPrefab);
        }
    }

   
    public void OnPointerEnter(PointerEventData eventData) //quando o mouse fica sob o objeto
    {
        panelSize = tooltipRectTransform.sizeDelta * tooltipPanel.lossyScale;//pegando o tamanho do painel
        newPos = tooltipPanel.position; //atribuindo a posi��o do painel a vari�vel
        if (newPos.x - panelSize.x < 0) newPos.x += panelSize.x - newPos.x; //se o painel for atravessar a tela do lado esquerdo, o atributo "x" da vari�vel "newPos" vai receber um valor a mais que equivale a diferen�a entre o tamanho "x" do painel e "x" da posi��o dele, garantindo que ele esteja inteiramente na tela
        if (newPos.x - panelSize.x < 0) newPos.x -= panelSize.x - newPos.x; //se o painel for atravessar a tela do lado direito, o atributo "x" da vari�vel "newPos" vai receber um valor a menos que equivale a diferen�a entre o tamanho "x" do painel e "x" da posi��o dele, garantindo que ele esteja inteiramente na tela
        tooltipPanel.position = newPos; //atribuindo a nova posi��o ao painel
        if (!InventoryManager.instance.dragging) tooltipPanel.gameObject.SetActive(true); //se eu n�o estiver carregando um item o painel de descri��o pode ser exibido, essa condi��o serve para que n�o exiba a descri��o de outros itens enquanto um item carregado passa por cima deles
        tooltipPanel.SetParent(transform.root); //o pai do painel de descri��o passa a ser o canvas, ficando assim acima de todos os slots e sendo vis�vel
    }
    public void OnPointerExit(PointerEventData eventData)//quando o mouse sai do objeto
    {
        tooltipPanel.gameObject.SetActive(false); //o painel de descri��o desativa
        tooltipPanel.SetParent(transform); //o painel de descri��o volta ao a ser filho do item
    }
    public void CreateItem(Item newItem, int itemCount) //criar um item;
    {
        image.sprite = newItem.image; //pegando a imagem do item
        item = newItem; // atribuindo o item a vari�vel
        count = itemCount;
    }

    public void RefreshCount() //atualizar a contagem
    {
        countText.text = count.ToString(); //passo o valor da contagem para o texto do item
        countText.enabled = item.Type == itemType.Consumable ? true : false; //  a contagem s� vai aparecer o item for um consum�vel
    }
   
    public void Use() //usar o item
    {
        if (item.Type == itemType.Consumable && count > 0) //se o item for consum�vel e a contagem for maior do que 0
        {

            count--; //diminuo 1 da contagem
            RefreshCount(); //atualizo a contagem
            
        }
        
    }

    public void DropItem(GameObject item)
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Vector2 playerPos = player.transform.position;
        Destroy(this.gameObject);
        
        var itemDropped = Instantiate(item, new Vector2(playerPos.x + dropRadius1, playerPos.y - dropRadius2), Quaternion.identity);
        itemDropped.GetComponent<ItemDropped>().itemCount = count;
        
    }

   
}
