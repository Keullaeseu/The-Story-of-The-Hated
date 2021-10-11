using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Transform mainCameraTransform = null;

    [SerializeField] private UnitHealth unitHealth = null;
    [SerializeField] private GameObject healthUI = null;
    [SerializeField] private Image healthImage = null;

    private void Awake()
    {
        unitHealth.ClientOnHealthUIUpdate += HealthUIUpdate;
    }

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void OnDestroy()
    {
        unitHealth.ClientOnHealthUIUpdate -= HealthUIUpdate;
    }

    // Old input system, need new one
    private void OnMouseEnter()
    {
        healthUI.SetActive(true);
    }

    // Old input system, need new one
    private void OnMouseOver()
    {
        if (mainCameraTransform == null) { return; }

        // Face UI to player camera
        healthUI.transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }

    // Old input system, need new one
    private void OnMouseExit()
    {
        healthUI.SetActive(false);
    }

    private void HealthUIUpdate(int currentHealth, int maxHealth)
    {
        // Float need for float value not 0 or 1, but float for fill
        healthImage.fillAmount = (float)currentHealth / maxHealth;
    }
}