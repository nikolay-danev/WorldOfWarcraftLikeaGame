using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float enemyHealth;
    ExperienceScript experience;
    Animation anim;
    Transform PlayerTrans;
    bool isHitted = false;
    Slider healthSlider;
    PlayerScript PlayerScript;
    AudioSource audioS;
    public QuestsInteraction killerCounter;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip hittedSound;
    public bool isDead = false;
    private bool isRespawned;
    public GameObject goldDropPlace;
    public GameObject gold;
    GameMasterScript Attack;
    void Start()
    {
        PlayerTrans = GameObject.Find("Player").GetComponent<Transform>();
        experience = GameObject.Find("Player").GetComponent<ExperienceScript>();
        anim = GetComponent<Animation>();
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = enemyHealth;
        healthSlider.value = enemyHealth;
        PlayerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        audioS = GetComponent<AudioSource>();
        killerCounter = GameObject.Find("QuestManager").GetComponent<QuestsInteraction>();
        Attack = GameObject.Find("GM").GetComponent<GameMasterScript>();
    }
    
    void Update()
    {
        if (isHitted == true)
        {
            if (Vector3.Distance(PlayerTrans.position, transform.position) < 4)
            {
                anim.Stop("Run");
                anim.Play("Attack");

            }
            else
            {
                transform.LookAt(PlayerTrans);
                transform.Translate(Vector3.forward * 4.5f * Time.deltaTime);
                anim.Play("Run");
            }
        }
        if (PlayerScript.isDead == true && enemyHealth > 0)
        {
            isHitted = false;
            anim.Play("Idle");
        }
       
    }
    public static GameObject goldClone;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BasicAttack")
        {
            isHitted = true;
            enemyHealth -= Random.Range(Attack.AttackDamageMin, Attack.AttackDamageMax);
            healthSlider.value = enemyHealth;
            audioS.PlayOneShot(hittedSound, 0.01f);

            if (enemyHealth <= 0)
            {
                if (killerCounter.counter < 10 && QuestsInteraction.isQuestAccepted == true)
                {
                    killerCounter.counter += 1;
                }
                healthSlider.gameObject.SetActive(false);
                isHitted = false;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                audioS.PlayOneShot(deathSound, 0.1f);
                anim.Play("Death");
                experience.GetXp(50);
                goldClone = Instantiate(gold, goldDropPlace.transform.position, goldDropPlace.transform.rotation);
                Destroy(goldClone, 15);
                Destroy(gameObject, 15f);
            }
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "DeadlyBreath")
        {
            isHitted = true;
            enemyHealth -= Random.Range(1, 2);
            healthSlider.value = enemyHealth;
            audioS.PlayOneShot(hittedSound, 0.01f);
            if (enemyHealth <= 0)
            {
                if (killerCounter.counter < 10 && QuestsInteraction.isQuestAccepted == true)
                {
                    killerCounter.counter += 1;
                }
                healthSlider.gameObject.SetActive(false);
                isHitted = false;
                gameObject.GetComponent<Collider>().enabled = false;
                anim.Play("Death");
                goldClone = Instantiate(gold, goldDropPlace.transform.position, goldDropPlace.transform.rotation);
                Destroy(goldClone, 15);
                audioS.PlayOneShot(deathSound, 0.1f);
                experience.GetXp(50);
                Destroy(gameObject, 15f);

            }
        }
    }
    void Respawn()
    {
        gameObject.SetActive(true);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
