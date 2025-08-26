using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    public CircleCollider2D lootMagnetRadius;
    public CircleCollider2D lootBody;
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
        

        if (playerNear)
            if (inventoryManager.checkFreeSlots(item))
            {
                lootFlag = true;
            } else lootFlag = false;

      
    }

    void FixedUpdate()
    {
        Collider2D lootRadius = Physics2D.OverlapCircle(transform.position, .5f);
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
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            lootBody.isTrigger = true;
        }
    }




}
