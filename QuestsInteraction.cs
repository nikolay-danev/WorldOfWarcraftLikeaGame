using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestsInteraction : MonoBehaviour
{
    [SerializeField] private Image QuestBar;
    [SerializeField] public Text taskText;
    [SerializeField] private Button completeQuest;
    [SerializeField] private Button Decline;
    public int counter = 0;
    public static bool isQuestAccepted = false;
    ExperienceScript experience;
    public bool isQuestCompleted = false;
    AudioSource audioS;
    public AudioClip completeQaudio;
    public AudioClip acceptQaudio;
    SpriteRenderer QuestToTakeMark;
    SpriteRenderer QuestCompletedMark;
    TalkingToPlayer sounds;
    public bool firstQuestFinished = false;
    void Start()
    {
    
        
        taskText = GameObject.Find("TaskText").GetComponent<Text>();
        taskText.gameObject.SetActive(false);
        experience = GameObject.Find("Player").GetComponent<ExperienceScript>();

        completeQuest.gameObject.SetActive(false);

        QuestToTakeMark = GameObject.Find("Quest").GetComponent<SpriteRenderer>();
        QuestCompletedMark = GameObject.Find("QuestDone").GetComponent<SpriteRenderer>();
        QuestCompletedMark.gameObject.SetActive(false);
        audioS = GetComponent<AudioSource>();
        sounds = GameObject.Find("QuestNPC").GetComponent<TalkingToPlayer>();       
    }

    void Update()
    {
        if(isQuestAccepted)
        {
            taskText.text = "Green spider slayed: " + counter;
        }
        if (counter == 10 && isQuestCompleted == true)
        {
            completeQuest.gameObject.SetActive(true);
            Decline.gameObject.SetActive(false);
            QuestToTakeMark.gameObject.SetActive(false);
            QuestCompletedMark.gameObject.SetActive(true);
        }
        if (counter == 10)
        {
            isQuestCompleted = true;
        }
    }

    public void AcceptQuest()
    {
        if (isQuestAccepted == false)
        {
            isQuestAccepted = true;
            QuestBar.gameObject.SetActive(false);
            taskText.gameObject.SetActive(true);
            sounds.audioS.PlayOneShot(sounds.Goodbye,0.5f);
            sounds.isInteracting = false;
            audioS.PlayOneShot(acceptQaudio, 0.5f);

        }
    }

    public void DeclineQuest()
    {
        QuestBar.gameObject.SetActive(false);
        sounds.audioS.PlayOneShot(sounds.Goodbye, 0.5f);
        sounds.isInteracting = false;

    }

    public void CompleteQuest()
    {
        if (isQuestAccepted)
        {
            audioS.PlayOneShot(completeQaudio, 0.5f);
            counter = 0;
            isQuestCompleted = false;
            completeQuest.gameObject.SetActive(false);
            Decline.gameObject.SetActive(true);
            QuestToTakeMark.gameObject.SetActive(false);
            QuestCompletedMark.gameObject.SetActive(false);
            isQuestAccepted = false;
            experience.experience += 45;
            experience.experienceBarSlider.value = experience.experience;
            experience.gold += 20;
            QuestBar.gameObject.SetActive(false);
            taskText.gameObject.SetActive(false);
            sounds.isInteracting = false;
            firstQuestFinished = true;
            sounds.audioS.PlayOneShot(sounds.Goodbye, 0.5f);

        }
    }
}
