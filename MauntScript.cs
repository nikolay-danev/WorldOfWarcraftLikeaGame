using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauntScript : MonoBehaviour
{
    public static bool isMaunted = false;
    public GameObject Maunt;
    Animation MauntAnim;
    AudioSource audioS;
    PlayerScript playerScript;
    public AudioClip whistle;
    GameObject MauntClone;
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        playerScript = GetComponent<PlayerScript>();

    }
    private void Update()
    {
        MauntSpawn();
        if (Input.GetKey(KeyCode.W) && isMaunted == true)
        {
            MauntAnim.Play("Run");
        }
        else if (Input.GetKeyUp(KeyCode.W) && isMaunted == true)
        {
            MauntAnim.Play("Idle");
        }
        if (Input.GetKey(KeyCode.S) && isMaunted == true)
        {
            MauntAnim.Play("Run");
        }
        else if (Input.GetKeyUp(KeyCode.S) && isMaunted == true)
        {
            MauntAnim.Play("Idle");
        }
        if (playerScript.isDead == true)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
            isMaunted = false;
            playerScript.speed = 5f;
            playerScript.MagicRune.SetActive(false); 
            Destroy(MauntClone); 
        }
        if (playerScript.isDead == false && Input.GetKeyDown(KeyCode.Alpha0) && isMaunted == true)
        {
            isMaunted = false;
            audioS.PlayOneShot(whistle, 0.1f);
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
            playerScript.speed = 5f;
            playerScript.MagicRune.SetActive(true);
            Destroy(MauntClone);
        }
    }
    public void MauntSpawn()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9) && isMaunted == false)
        {
            audioS.PlayOneShot(whistle, 0.1f);
            isMaunted = true;
            playerScript.anim.Stop("free");
            playerScript.anim.Play("skill");
            MauntClone = Instantiate(Maunt, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), transform.rotation);
            MauntClone.transform.parent = transform;
            MauntAnim = MauntClone.GetComponent<Animation>();
            playerScript.MagicRune.SetActive(false);
            
        }
    }
}
