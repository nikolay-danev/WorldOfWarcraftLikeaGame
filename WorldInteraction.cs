using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorldInteraction : MonoBehaviour {
    public static WorldInteraction instance;
    
    NavMeshAgent interactedGameObject;
    public Transform mobTrans;
    public bool selectedObject = false;
    public bool isMoving = false;
    public bool isMobSelected = false;
    public Camera myCam;
    Image QuestBar;
    public GameObject questNpc;
    TalkingToPlayer sounds;
    ExperienceScript gold;
    EnemyScript goldClone;
    QuestsInteraction questsInteraction;
    void Start()
    {
        myCam = GetComponent<Camera>();
        QuestBar = GameObject.Find("QuestBar").GetComponent<Image>();
        QuestBar.gameObject.SetActive(false);
        sounds = GameObject.Find("QuestNPC").GetComponent<TalkingToPlayer>();
        gold = GameObject.Find("Player").GetComponent<ExperienceScript>();
        questsInteraction = GameObject.Find("QuestManager").GetComponent<QuestsInteraction>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GetInteraction();
        }
       
    }

    void GetInteraction()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if(interactedObject.gameObject.tag == "QuestNPC" && 
                Vector3.Distance(questNpc.transform.position,transform.position) < 20 &&
                questsInteraction.firstQuestFinished == false)
            {
                questsInteraction.taskText.text = "Green spiders slayed: ";
                QuestBar.gameObject.SetActive(true);
                sounds.audioS.PlayOneShot(sounds.Greetings,0.5f);
                sounds.isInteracting = true;
            }
            if(interactedObject.gameObject.tag == "Gold")
            {
                gold.gold += Random.Range(30, 50);
                Destroy(EnemyScript.goldClone);
            }

        }
       
    }
    
}
