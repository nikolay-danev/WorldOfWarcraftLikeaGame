using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public GameObject DeadSpawn;
    public void RespawnBTN()
    {
        transform.position = DeadSpawn.transform.position;
    }

}
