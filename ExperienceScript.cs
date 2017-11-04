using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

  public class ExperienceScript : MonoBehaviour {
    public float experience = 0;
    public int level = 1;
    public Slider experienceBarSlider;
    public GameObject levelUpAnim;
    public AudioClip levelUpSound;
    Text levelText;
    AudioSource audioS;

    bool IsSpellbookOpen = false;
    HealthScript playerHealthScript;
    Image healImage;
    Image deadlyBreath;
    Image FireTotem;
    Image AirTotem;
    Image WaterTotem;
    Image EarthTotem;
   public GameObject SpellBook;
    Text goldText;

    public bool isLevelIncreased = false;

    void Start () {
        experienceBarSlider = GameObject.Find("XPBar").GetComponentInChildren<Slider>();
        experienceBarSlider.value = experience;
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        audioS = GetComponent<AudioSource>();
        playerHealthScript = GetComponent<HealthScript>();
        healImage = GameObject.Find("HealSpell").GetComponent<Image>();
        deadlyBreath = GameObject.Find("FireSpell").GetComponent<Image>();
        FireTotem = GameObject.Find("FireTotem").GetComponent<Image>();
        WaterTotem = GameObject.Find("WaterTotem").GetComponent<Image>();
        EarthTotem = GameObject.Find("EarthTotem").GetComponent<Image>();
        AirTotem = GameObject.Find("AirTotem").GetComponent<Image>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        SpellBook.SetActive(false);
    }
	
	void Update () {
        SpellImageColor();
        SpellbookOpen();
        goldText.text = gold.ToString();
       
    }
    public void GetXp(int xp)
    {
        experience += xp;
        experienceBarSlider.value = experience;
        if(experience >= 100)
        {
            isLevelIncreased = true;
            experience = 0;
            experienceBarSlider.value = experience;
            level++;
            var clone = Instantiate(levelUpAnim, transform.position, Quaternion.identity);
            clone.GetComponent<Transform>().Rotate(0, 0, 0);
            clone.transform.parent = transform;
            Destroy(clone, 4);
            audioS.PlayOneShot(levelUpSound,0.5f);
            levelText.text = level.ToString();
            playerHealthScript.health = 100;
            playerHealthScript.healthSlider.value = playerHealthScript.health;
            playerHealthScript.mana = 100;
            playerHealthScript.manaSlider.value = playerHealthScript.mana;
        }
        
    }

    public static bool isHealTrained = false;
    public static bool isDeadlyBreathTrained = false;
    public static bool isAirTotemTrained = false;
    public static bool isWaterTotemTrained = false;
    public static bool isEarthTotemTrained = false;
    public static bool isFireTotemTrained = false;
    public float gold = 10;
    void SpellImageColor()
    {
        if (isHealTrained == false)
        {
            healImage.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            healImage.color = new Color32(255, 255, 255, 255);
        }
        if (isDeadlyBreathTrained == false)
        {
            deadlyBreath.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            deadlyBreath.color = new Color32(255, 255, 255, 255);
        }
        if(isAirTotemTrained == false)
        {
            AirTotem.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            AirTotem.color = new Color32(255, 255, 255, 255);
        }
        if (isWaterTotemTrained == false)
        {
            WaterTotem.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            WaterTotem.color = new Color32(255, 255, 255, 255);
        }
        if (isEarthTotemTrained == false)
        {
            EarthTotem.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            EarthTotem.color = new Color32(255, 255, 255, 255);
        }
        if (isFireTotemTrained == false)
        {
            FireTotem.color = new Color32(60, 60, 60, 255);
        }
        else
        {
            FireTotem.color = new Color32(255, 255, 255, 255);
        }
    }

    public void TrainMagic()
    {
        if(level >= 3 && gold >= 10)
        {
            isHealTrained = true;
            gold -= 10;
        }
        if (level >= 10 && gold >= 20)
        {
            isDeadlyBreathTrained = true;
            gold -= 20;
        }
        if (level >= 15 && gold >= 30)
        {
            isAirTotemTrained = true;
            gold -= 30;
        }
        if (level >= 16 && gold >= 40)
        {
            isWaterTotemTrained = true;
            gold -= 40;
        }
        if (level >= 17 && gold >= 50)
        {
            isEarthTotemTrained = true;
            gold -= 50;
        }
        if (level >= 18 && gold >= 60)
        {
            isFireTotemTrained = true;
            gold -= 60;
        }
    }

    void SpellbookOpen()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SpellBook.active = !SpellBook.active;
        }
        
    }
}
