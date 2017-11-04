using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesertSpider : MonoBehaviour {

    public float enemyHealth;
    ExperienceScript experience;
    Animation anim;
    Transform PlayerTrans;
    bool isHitted = false;
    Slider healthSlider;
    PlayerScript PlayerScript;
    AudioSource audioS;

    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioClip hittedSound;
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
        Attack = GameObject.Find("GM").GetComponent<GameMasterScript>();

    }

    void Update()
    {

        if (isHitted == true)
        {
            if (Vector3.Distance(PlayerTrans.position, transform.position) < 4)
            {
                anim.Stop("run");
                anim.Play("attack2");

            }
            else
            {
                transform.LookAt(PlayerTrans);
                transform.Translate(Vector3.forward * 4.5f * Time.deltaTime);
                anim.Play("run");
            }
        }
        if (PlayerScript.isDead == true && enemyHealth > 0)
        {
            isHitted = false;
            anim.Play("idle");
        }
    }

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
                healthSlider.gameObject.SetActive(false);
                isHitted = false;
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                audioS.PlayOneShot(deathSound, 0.1f);
                anim.Play("death1");
                experience.GetXp(50);
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
                healthSlider.gameObject.SetActive(false);
                isHitted = false;
                gameObject.GetComponent<Collider>().enabled = false;
                anim.Play("death1");

                audioS.PlayOneShot(deathSound, 0.1f);
                experience.GetXp(50);
                Destroy(gameObject, 15f);
            }
        }
    }
}
