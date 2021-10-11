using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    //[SerializeField] private Transform mainCameraTransform = null;

    //[SerializeField] private GameObject textUI = null;
    [SerializeField] public TMP_Text text = null;


    private void Awake()
    {
        // Transform to camera
        //textUI.transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);        
    }
}
