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
        //checando se eu estou em movimento
        moving = (rb.linearVelocityX > 1 || rb.linearVelocityX < -1 ) || (rb.linearVelocityY > 1 || rb.linearVelocityY < -1)? true : false;
        //alterando a direção do sprite do player de acordo com a direção da velocidade
        if (rb.linearVelocityX > 1) this.transform.localScale = new Vector3(1, 1, 1);
        else if (rb.linearVelocityX < -1) this.transform.localScale = new Vector3(-1, 1, 1);
        //Passando a informação de movimento para o Animator
        anim.SetBool("Moving", moving);

    }

    private void FixedUpdate()
    {
        WASDMove();
    }

    public void WASDMove()
    {
       //pegando a informação do controle e passando para a velocidade do RigidBody
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
