using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public Animation anim;

    private bool isCastingSpell = false;
    public  bool isDead = false;
    private bool isWalking = false;
    bool isTextOn = false;

    public GameObject basicAttack;
    public GameObject[] spells;
    public GameObject Staff;
    float nextAutoSpell;
    float nextHeal;
    float nextDeadlyBreath;
    float coolDown = 0.8f;
    public int PlayerHealth = 100;

    public GameObject healPoint;
    public GameObject basicAttackEffect;
    public GameObject deadlyBreath;

    HealthScript health;
    HealthScript mana;

    public GameObject FireTotem;
    public GameObject FireTotemSpawn;
    public GameObject EarthTotem;
    public GameObject EarthTotemSpawn;
    public GameObject WaterTotem;
    public GameObject WaterTotemSpawn;
    public GameObject AirTotem;
    public GameObject AirTotemSpawn;
    public GameObject MagicRune;

    Text warningText;

    AudioSource audioS;
    public AudioClip HealSound;
    public AudioClip BasicAttackSound;
    public AudioClip DeadlyBreathSound;
    public AudioClip TotemSpawnSound;
    public AudioClip PlayerDeath;

    public Text LocationText;
    Button respawnBtn;
    ExperienceScript PlayerLevel;
    public GameObject DeadlyBreath;
    GameMasterScript attack;
    void Start()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthScript>();
        mana = GetComponent<HealthScript>();
        warningText = GameObject.Find("WarningText").GetComponent<Text>();
        audioS = GetComponent<AudioSource>();
        respawnBtn = GameObject.Find("RespawnBTN").GetComponent<Button>();
        respawnBtn.gameObject.SetActive(false);
        PlayerLevel = GetComponent<ExperienceScript>();
        attack = GameObject.Find("GM").GetComponent<GameMasterScript>();
    }

    void Update()
    {
            Moving();
            SpellCast();
        
       if(MauntScript.isMaunted)
        {
            speed = 10f;
            transform.position = new Vector3(transform.position.x,7,transform.position.z);
        }

        if(isTextOn == true)
        {
            StartCoroutine("TextTurnOff",1);
        }

    }
   
    public void SpellCast()
    {
        if(isDead==false && mana.mana > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time > nextAutoSpell && isWalking == false)
            {
                audioS.PlayOneShot(BasicAttackSound, 0.1f);

                mana.TakeMana(1);
                nextAutoSpell = Time.time + coolDown;
                var cloneEffect = Instantiate(basicAttackEffect, Staff.transform.position, Staff.transform.rotation);
                var clone = Instantiate(basicAttack, Staff.transform.position, Staff.transform.rotation);
                clone.GetComponent<Rigidbody>().velocity = transform.forward * 25f;
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("attack");
                Destroy(clone, 0.5f);
                Destroy(cloneEffect, 1.5f);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time > nextHeal && isWalking == false && ExperienceScript.isHealTrained == true)
            {
                audioS.PlayOneShot(HealSound);

                mana.TakeMana(15);
                health.Heal(10, 20);
                nextHeal = Time.time + coolDown + 10;
                var clone = Instantiate(spells[0], healPoint.transform.position, Quaternion.identity);
                clone.GetComponent<Transform>().Rotate(-180, 0, 0);
                clone.transform.parent = transform;
                Destroy(clone, 4);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }          
            if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time > nextDeadlyBreath && isWalking == false && PlayerLevel.level >= 10)
            {
                mana.TakeMana(25);
                 nextDeadlyBreath = Time.time + coolDown + 20;
                var clone = Instantiate(DeadlyBreath, new Vector3(transform.position.x,transform.position.y + 1.5f,transform.position.z), transform.rotation);
                clone.GetComponent<Transform>().Rotate(90, 0, 0);
                clone.transform.parent = transform;
                Destroy(clone, 2.5f);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && isWalking == false && PlayerLevel.level >= 15)
            {
                audioS.PlayOneShot(TotemSpawnSound);
                var clone = Instantiate(AirTotem, AirTotemSpawn.transform.position, AirTotemSpawn.transform.rotation);
                Destroy(clone, 60);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && isWalking == false && PlayerLevel.level >= 16)
            {
                audioS.PlayOneShot(TotemSpawnSound);

                var clone = Instantiate(WaterTotem, WaterTotemSpawn.transform.position, WaterTotemSpawn.transform.rotation);
                Destroy(clone, 60);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) && isWalking == false && PlayerLevel.level >= 17)
            {
                audioS.PlayOneShot(TotemSpawnSound);

                var clone = Instantiate(EarthTotem, EarthTotemSpawn.transform.position, EarthTotemSpawn.transform.rotation);
                Destroy(clone, 60);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && isWalking == false && PlayerLevel.level >= 18)
            {
                audioS.PlayOneShot(TotemSpawnSound);

                var clone = Instantiate(FireTotem, FireTotemSpawn.transform.position, FireTotemSpawn.transform.rotation);
                Destroy(clone, 60);
                isCastingSpell = true;
                anim.Stop("free");
                anim.Play("skill");
            }

            if (PlayerLevel.level < 3 && Input.GetKeyDown(KeyCode.Alpha2) && isTextOn==false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 3 required for this magic!";
            }
            if (PlayerLevel.level < 10 && Input.GetKeyDown(KeyCode.Alpha3) && isTextOn == false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 10 required for this magic!";
            }
            if (PlayerLevel.level < 15 && Input.GetKeyDown(KeyCode.Alpha4) && isTextOn == false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 15 required for this magic!";
            }
            if (PlayerLevel.level < 16 && Input.GetKeyDown(KeyCode.Alpha5) && isTextOn == false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 16 required for this magic!";
            }
            if (PlayerLevel.level < 17 && Input.GetKeyDown(KeyCode.Alpha6) && isTextOn == false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 17 required for this magic!";
            }
            if (PlayerLevel.level < 18 && Input.GetKeyDown(KeyCode.Alpha7) && isTextOn == false)
            {
                isTextOn = true;
                warningText.enabled = true;
                warningText.text = "Level 18 required for this magic!";
            }
        }
        
    }

    void Moving()
    {
        if(isDead == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                isWalking = true;
                isCastingSpell = false;
                rb.velocity = transform.forward * speed;
                if(MauntScript.isMaunted == false)
                {
                    anim.Play("walk");
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                isWalking = true;
                isCastingSpell = false;
                if (MauntScript.isMaunted == false)
                {
                    anim.Play("walk");
                }
                rb.velocity = -transform.forward * 2;
            }

            else
            {
                if (isCastingSpell == false)
                {
                    anim.Play("free");
                }
                isWalking = false;
                rb.velocity = Vector3.zero;
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0, -3, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, 3, 0);
            }
        }
        

    }
    public Image ComingSoon;
     void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Mob")
        {
            health.TakeDamage(attack.AttackDamageMin, attack.AttackDamageMax);
            if (health.health <= 0)
            {
                audioS.PlayOneShot(PlayerDeath, 3);
                isDead = true;
                anim.Stop("free");
                anim.Stop("walk");
                anim.Play("death");
                respawnBtn.gameObject.SetActive(true);
                MagicRune.SetActive(false);
            }
        }
        if(collision.gameObject.tag == "EndOfTheGame" && PlayerLevel.level == 20)
        {
            ComingSoon.gameObject.SetActive(true);
        }
    }
    private IEnumerator TextTurnOff()
    {
        yield return new WaitForSeconds(1);
        isTextOn = false;
        warningText.enabled = false;
    }

    public GameObject DeadSpawn;
    public void RespawnBTN()
    {
        isDead = false;
        anim.Stop("death");
        anim.Play("free");
        MagicRune.SetActive(true);
        health.health = 100;
        health.healthSlider.value = health.health;
        mana.mana = 100;
        mana.manaSlider.value = mana.mana;
        transform.position = DeadSpawn.transform.position;
        respawnBtn.gameObject.SetActive(false);
    }

     void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Elwynn")
        {
            LocationText.text = "Silent Woods";
            LocationText.color = new Color(0, 1, 0, 1);

        }
        if (other.gameObject.tag == "Tanaris")
        {
            LocationText.text = "The Desert";
            LocationText.color = new Color(255, 255, 0, 255);
        }
        if (other.gameObject.tag == "ThePortal")
        {
            LocationText.text = "Dead Islands";
            LocationText.color = new Color(255, 0, 0, 255);

        }
        if (other.gameObject.tag == "Forest")
        {
            LocationText.text = "Qu'il Rain Forest";
            LocationText.color = new Color(255, 255, 0, 255);

        }
    }
}
