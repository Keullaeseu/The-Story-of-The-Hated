using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;
using Ke.Inputs;
using UnityEngine.EventSystems;

public class PlayerInitialization : MonoBehaviour
{
    [SerializeField] private NetworkPlayerGame networkPlayerGame = null;
    //[SerializeField] private Transform playerGameObject = null;
    [SerializeField] private GameObject playerCamera = null;
    [SerializeField] private CameraController cameraController = null;
    [SerializeField] private UnitSelection unitSelection = null;
    [SerializeField] private PlayerUnitMove unitMovement = null;
    [SerializeField] private GameObject unitSelectionUI = null;

    // UI
    [SerializeField] private GameObject blackLoadScreenUI = null;
    [SerializeField] private InGameMenu inGameMenu = null;
    [SerializeField] private GameObject menuCanvas = null;
    [SerializeField] private GameObject eventSystem = null;

    [SerializeField] private GameObject miniMapCamera = null;
    [SerializeField] private GameObject miniMap = null;

    [HideInInspector] public bool isUISelected = false;

    private Controls controls;
    public Controls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new Controls();
        }
    }

    public void Initialization()
    {
        // Durty play with find and string
        GameObject spawnPointsGameObject = GameObject.FindGameObjectWithTag("SpawnPoints");
        // Be carful with player camera it sense to playerGameObject position
        playerCamera.transform.position = new Vector3 (spawnPointsGameObject.GetComponent<SpawnPoints>().SpawnPointsArray[networkPlayerGame.GetSlot()].transform.position.x, playerCamera.transform.position.y, spawnPointsGameObject.GetComponent<SpawnPoints>().SpawnPointsArray[networkPlayerGame.GetSlot()].transform.position.z);

        playerCamera.SetActive(true);
        unitSelection.enabled = true;
        unitMovement.enabled = (true);
        unitSelectionUI.SetActive(true);

        // UI        
        menuCanvas.SetActive(true);
        eventSystem.SetActive(true);
        inGameMenu.enabled = true;

        miniMapCamera.SetActive(true);
        miniMap.SetActive(true);

        cameraController.enabled = true;

        // finish loading
        blackLoadScreenUI.SetActive(false);
    }
        
    public void ControlsDisable(bool state)
    {
        if (state)
        {
            Controls.Enable();
        }
        else
        {
            Controls.Disable();
        }
    }
        
    private void OnEnable() => Controls.Enable();
        
    private void OnDisable() => Controls.Disable();
}
