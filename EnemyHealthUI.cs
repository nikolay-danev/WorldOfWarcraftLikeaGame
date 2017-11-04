using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour {

    public Transform cameraTrans;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(cameraTrans);
	}
}
