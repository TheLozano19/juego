using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{

    public static int amount;
    public bool yersorno;
    void Start()
    {
        amount++;
        Player.singleton = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
