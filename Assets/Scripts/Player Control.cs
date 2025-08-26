using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{

    public InputActionReference move;
    public float moveSpeed;
    public Vector2 moveDirection;
    public Rigidbody2D rb;
    public bool nearChest;
    public Animator anim;
    public bool moving;
    

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

 
    void Update()
    {
        moving = (rb.linearVelocityX > 1 || rb.linearVelocityX < -1 ) || (rb.linearVelocityY > 1 || rb.linearVelocityY < -1)? true : false;
        if (rb.linearVelocityX > 1) this.transform.localScale = new Vector3(1, 1, 1);
        else if (rb.linearVelocityX < -1) this.transform.localScale = new Vector3(-1, 1, 1);
        if (nearChest && Input.GetKeyDown(KeyCode.Space)) Debug.Log("abri o bau");
        anim.SetBool("Moving", moving);

    }

    private void FixedUpdate()
    {
        WASDMove();
    }

    public void WASDMove()
    {
        moveDirection = move.action.ReadValue<Vector2>();
        rb.linearVelocity = moveDirection.normalized * moveSpeed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            nearChest = true;
          
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            nearChest = false;
        }
    }
}
