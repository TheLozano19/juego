using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class corrutina : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject mon;
   
    void Start()
    { 
        StartCoroutine(monedita());
    }

    
    void Update()
    {


    }
    IEnumerator monedita()
    {
        body.AddForceY(200);
        yield return new WaitForSeconds(1f);
        mon.gameObject.SetActive(false);
        
    }

}