using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Player : MonoBehaviour
{
    public static Player singleton;
    public State state = State.Idle;
    public Rigidbody2D body;
    private SpriteRenderer sprite;
    public float jumpForce;
    Vector2 direction;
    public Animator anim;
    public GameObject Enemy;
    public Transform Controlgolpe;
    public float radio;
    public float daño;
    public float vida;
    private bool mirandoD;

    public enum State
    {
        Idle,
        Walking,
        Jumping,
        Attacking,
        Deadd



    }
    void Awake()
    {
        anim = GetComponent<Animator>();
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log($"State:{state}");
        switch (state)
        {

            case State.Idle:
                OnIdle();
                break;
            case State.Walking:
                OnWalking();
                break;
            case State.Jumping:
                OnJumping();
                break;
            case State.Attacking:
                OnAttacking();
                break;
            case State.Deadd:
                OnDead();
                break;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "plataforma")
        {
            Debug.Log("Walking");
            state = State.Walking;
        }
    }


    void OnAttacking()
    {


        if (!Attack())
        {
            state = State.Idle;
        }
        if (HorizontalMove())
        {
            Debug.Log("Walking");
            state = State.Walking;
        }
        if (jump())
        {
            Debug.Log("Jumping");
            state = State.Jumping;
        }
    }
    void OnWalking()
    {
        if (!HorizontalMove())
        {

            state = State.Idle;
        }
        if (jump())
        {
            state = State.Jumping;

        }
        if (Attack())
        {
            Debug.Log("Attaking");
            state = State.Attacking;
        }
        transform.Translate(direction * Time.deltaTime);
    }
    void OnIdle()
    {
        if (HorizontalMove())
        {
            Debug.Log("Walking");
            state = State.Walking;
        }
        if (jump())
        {
            Debug.Log("Jumping");
            state = State.Jumping;
        }
        if (Attack())
        {
            Debug.Log("Attaking");
            state = State.Attacking;
        }
    }
    void OnJumping()
    {

        if (HorizontalMove())
        {
            transform.Translate(direction * Time.deltaTime);
        }
        if (Attack())
        {
            Debug.Log("Attaking");
            state = State.Attacking;
        }
    }
    void OnDead()
    {
        if (!dead())
        {
            state = State.Walking;
        }
        if (!dead())
        {
            state = State.Deadd;
        }

    }


    bool HorizontalMove()
    {
       
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector2.left;
            anim.SetBool("walk", true);
            sprite.flipX = true;
            // transform.localScale = new Vector2(-1, -1);
            return true;

        }


        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector2.right;
            anim.SetBool("walk", true);
            sprite.flipX = false;
            // Orientacion(inputmovimiento);
            return true;

        }
        anim.SetBool("walk", false);
        return false;

    }
    bool jump()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForceY(jumpForce);
            return true;
        }
        return false;

    }
    bool Attack()
    {

        if (Input.GetMouseButtonDown(0))
        {

            Collider2D[] objetos = Physics2D.OverlapCircleAll(Controlgolpe.position, radio);
            foreach (Collider2D collision in objetos)
            {
                if (collision.CompareTag("Enemy"))
                {
                    collision.transform.GetComponent<soldier>().TomarDaño(daño);
                }
            }

            anim.SetBool("Attack", true);
            return true;
        }
        anim.SetBool("Attack", false);
        return false;
    }
    bool dead()
    {
        vida -= daño;
        if (vida <= 0)
        {

            anim.SetBool("Dead", true);
            return true;
        }

        return false;
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
        }

    }
    private void Muerte()
    {
        anim.SetBool("Dead", true);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Controlgolpe.position, radio);
    }
    void Orientacion(float inputmovimiento)
    {
        if (Input.GetKey(KeyCode.A) || (mirandoD == false && inputmovimiento > 0))
        {
            mirandoD = !mirandoD;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
   
}
