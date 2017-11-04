using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    AudioSource audioS;
   public AudioClip Tanaris;
   public AudioClip Elwynn;
   public AudioClip Forest;
   public AudioClip ThePortal;

    bool isInTanaris = false;
    bool isInForest = false;
    bool isInThePortal = false;
    bool isInElwynn = false;

    void Start () {
        audioS = GetComponent<AudioSource>();
	}
	
	
	void Update () {
        
	}

    void CamMovement()
    {
        if (Input.mousePosition.x < 0 + 1)
        {
            transform.position += -transform.right * 0.3f;
        }
        if (Input.mousePosition.x > Screen.width - 1)
        {
            transform.position += transform.right * 0.3f;

        }
        if (Input.mousePosition.y < 0 + 1)
        {
            transform.position += -transform.forward * 0.3f;
        }
        if (Input.mousePosition.y > Screen.height - 1)
        {
            transform.position += transform.forward * 0.3f;
        }
        
    }
     void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Tanaris")
        {
            isInTanaris = true;
            if(isInTanaris == true)
            {
                audioS.PlayOneShot(Tanaris);
            }
        }
        
         if (collision.gameObject.tag == "Elwynn")
        {
            isInElwynn = true;
            if (isInElwynn == true)
            {
                audioS.PlayOneShot(Elwynn); 
            }
        }
       
         if (collision.gameObject.tag == "Forest")
        {
            isInForest = true;
            if (isInForest == true)
            {
                audioS.PlayOneShot(Forest); 
            }
        }
       
     if (collision.gameObject.tag == "ThePortal")
        {
            isInThePortal = true;
            if (isInThePortal == true)
            {
                audioS.PlayOneShot(ThePortal); 
            }
        }
        
    }
}
