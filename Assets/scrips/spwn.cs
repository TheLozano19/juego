using UnityEngine;

public class spwn : MonoBehaviour
{
  public GameObject [] soldierPref;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            int random = Random.Range(0, soldierPref.Length);//spawn de asteroides
            Vector3 ramdomps = new Vector3(Random.Range(-6, -5), Random.Range(-1, -2), Random.Range(0, 0)); //rango del spawn 

            Instantiate(soldierPref[random], ramdomps, Quaternion.identity);
        }
    }
}
