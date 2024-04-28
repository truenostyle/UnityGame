using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraAnchor; // точка камери для FPV
    private Vector3 cameraAngles; // кути Ейлера повороту камери
    private Vector3 cameraOffset; // зміщення камери від персонажу
    private Vector3 initialAngles; // кути на момент старту гри
    private Vector3 initialOffset;

    void Start()
    {
        initialAngles = cameraAngles = this.transform.eulerAngles;
        initialOffset = cameraOffset = this.transform.position - cameraAnchor.transform.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        cameraAngles.y += Input.GetAxis("Mouse X");  
        cameraAngles.x -= Input.GetAxis("Mouse Y");
        if (Input.GetKeyUp(KeyCode.V))
        {
            cameraOffset = (cameraOffset == Vector3.zero) 
                ? initialOffset 
                : Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        this.transform.position = cameraAnchor.transform.position + Quaternion.Euler(0, cameraAngles.y - initialAngles.y, 0) * cameraOffset;
        this.transform.eulerAngles = cameraAngles;
    }
}
