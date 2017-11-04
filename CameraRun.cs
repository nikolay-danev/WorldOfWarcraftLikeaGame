using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRun : MonoBehaviour
{
    public GameObject target;
    public GameObject target2;
    public bool isThere = false;
    bool finalDestination = false;
    Button playBTN;
    private void Start()
    {
        playBTN = GameObject.Find("PlayBTN").GetComponent<Button>();
        playBTN.gameObject.SetActive(true);
    }
    void Update()
    {
        if (finalDestination == false)
        {
            if (isThere == false)
            {

                transform.LookAt(target.transform);

                transform.Translate(Vector3.forward * Time.deltaTime * 0.9f);
            }
            if (isThere == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target2.transform.position - transform.position), 0.5f * Time.deltaTime);
                transform.Translate(Vector3.forward * Time.deltaTime * 0.9f);

            }
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Cube")
        {
            isThere = true;
        }
        if (other.gameObject.tag == "Cube1")
        {
            finalDestination = true;
            playBTN.gameObject.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("LoadingSceen");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
