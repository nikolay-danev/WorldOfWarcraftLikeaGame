using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScript : MonoBehaviour
{
    private Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Mob")
        {
            Destroy(gameObject);
           
        }
    }
   
}
