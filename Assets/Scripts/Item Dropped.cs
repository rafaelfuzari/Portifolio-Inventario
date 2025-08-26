using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public CircleCollider2D lootBody;
    public CircleCollider2D lootMagnetRadius;
    public bool playerNear;
    public InventoryManager inventoryManager;
    public Item item;
    public bool itemAdded;
    public int itemCount;
    public bool canLoot;
    public GameObject player;
    public bool lootFlag;

    void Start()
    {
        lootMagnetRadius = GetComponent<CircleCollider2D>();
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        player = GameObject.FindGameObjectWithTag("player");
    }

    
    void Update()
    {
        
        //Se o jogador estiver perto e ele estiver com espaço no inventário, ativar a lootFlag;
        if (playerNear)
            if (inventoryManager.checkFreeSlots(item))
            {
                lootFlag = true;
            } else lootFlag = false;

      
    }

    void FixedUpdate()
    {
        Collider2D lootRadius = Physics2D.OverlapCircle(transform.position, .5f); //Raio em que o item sera atraído para o jogador
        //Se o lootFlag ativar, vai atrair o item para o jogador, e ao chegar na posição do jogador, será destruido no mundo e adicionado ao inventário;
        if (lootFlag)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, .07f);
            if (lootRadius.gameObject.CompareTag("player"))
            {
                inventoryManager.AddNewItem(item, itemCount);
                Destroy(gameObject);
            }

        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.CompareTag("player"))
        {
           playerNear = true;
        }
        // Se o item estiver em uma parede, ativa o corpo fisico dele, sendo jogado para fora;
        if (other.CompareTag("InvisibleWall")){
            lootBody.isTrigger = false;
        }

    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("player"))
        {
            playerNear = false;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //Ao ser jogado para fora da parede, desativa o corpo físico, para que o jogador não colida com o objeto
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            lootBody.isTrigger = true;
        }
    }




}
