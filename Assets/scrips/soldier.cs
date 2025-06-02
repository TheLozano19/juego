using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class soldier : MonoBehaviour
{
    public GameObject soldierPref;
    public GameObject player;
    public float Detection;
    public Transform target;
    public float speed;
    public float vida;
    public Animator anim;
    public Transform Controlgolpe;
    public float radio;
    public float daño;

    private Rigidbody2D rig;
    private Vector2 movement;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float distanceP = Vector2.Distance(transform.position, target.position);
        if (distanceP < Detection)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            movement = new Vector2(direction.x, 0.9f);
            anim.SetBool("Walk", true);
            Debug.Log("Camina");


        }
        else
        {
            movement = Vector2.zero;
            anim.SetBool("Walk",false);

        }
        rig.MovePosition(rig.position + movement * speed * Time.deltaTime);
        
        if (distanceP <= 0.2)
        {
            Hit();
            Debug.Log("madreando");
        }
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();
            Debug.Log("moridos");
        }

    }
    private void Muerte()
    {
        anim.SetBool("Dead", true);
        StartCoroutine(DeadSold());

    }
    IEnumerator DeadSold()
    {
       
        yield return new WaitForSeconds(3f);
        soldierPref.gameObject.SetActive(false);

    }
    private void Hit()
    {
       
        Collider2D[] objetos = Physics2D.OverlapCircleAll(Controlgolpe.position, radio);
        foreach (Collider2D collision in objetos)
        {
            if (collision.CompareTag("Player"))
            {
                anim.SetBool("Attack", true);
                collision.transform.GetComponent<Player>().TomarDaño(daño);
               
            }
            
        }
        anim.SetBool("Attack", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Controlgolpe.position, radio);
    }

}