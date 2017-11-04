using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseRot : MonoBehaviour
{
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public Transform originalRotationValue;
    public Transform playerTrans;

    AudioSource audioS;
    public AudioClip Elwynn;
    public AudioClip ThePortal;
    public AudioClip Forest;
    public AudioClip Desert;
    float Scroll;
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        Scroll = Input.GetAxis("Mouse ScrollWheel");
    }
    void Update()
    {
        RotateCamera();
        Zoom();
    }
    void RotateCamera()
    {
        if (Input.GetMouseButton(1))
        {
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");
            transform.Rotate(-v, h, 0);
            float z = transform.eulerAngles.z;
            transform.Rotate(0, 0, -z);
        }
        if (Input.GetMouseButtonUp(1))
        {
            transform.rotation = originalRotationValue.transform.rotation;
            transform.position = originalRotationValue.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Elwynn")
        {
            audioS.clip = Elwynn;
            audioS.Play();

        }
        if (other.gameObject.tag == "Tanaris")
        {
            audioS.clip = Desert;
            audioS.Play();
        }
        if (other.gameObject.tag == "ThePortal")
        {
            audioS.clip = ThePortal;
            audioS.Play();
        }
        if (other.gameObject.tag == "Forest")
        {
            audioS.clip = Forest;
            audioS.Play();
        }
    }

    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            transform.position -= transform.forward * 1f;

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            transform.position += transform.forward * 1f;

        }
    }
}
