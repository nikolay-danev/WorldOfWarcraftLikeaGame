using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCast : MonoBehaviour {
    Animation anim;
    private bool isCastingSpell = false;
    public GameObject basicAttack;
    public GameObject[] spells;
    public GameObject Staff;
    float nextAutoSpell;
    float nextHeal;
    float coolDown = 0.8f;
    private bool isWalking = false;
    private int Health = 100;
    public GameObject healPoint;
    void Start () {
        anim = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        SpellCasting();
	}
    public void SpellCasting()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > nextAutoSpell && isWalking == false)
        {

            nextAutoSpell = Time.time + coolDown;
            var clone = Instantiate(basicAttack, Staff.transform.position, Staff.transform.rotation);
            clone.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
            isCastingSpell = true;
            anim.Stop("free");
            anim.Play("attack");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > nextHeal && isWalking == false)
        {

            nextHeal = Time.time + coolDown + 10;
            var clone = Instantiate(spells[0], healPoint.transform.position, Quaternion.identity);
            clone.GetComponent<Transform>().Rotate(-180, 0, 0);
            clone.transform.parent = transform;
            Destroy(clone, 5);
            isCastingSpell = true;
            anim.Stop("free");
            anim.Play("skill");
        }
    }
}
