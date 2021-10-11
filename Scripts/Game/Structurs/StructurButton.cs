using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Ke.Inputs;


public class StructurButton : MonoBehaviour
{
    [SerializeField] private UnitSelection unitSelection = null;

    // Get structurs list from player
    [SerializeField] private NetworkPlayerGame networkPlayerGame = null;

    // UI
    [SerializeField] private GameObject controlPanelUI = null;
    [SerializeField] private GameObject buildPanelUI = null;
    [SerializeField] private GameObject defensePanelUI = null;
    [SerializeField] private GameObject structureInfoPanelUI = null;
    

    // Get controls from player initialization
    [SerializeField] private PlayerInitialization playerInitialization = null;
    private Controls controls = null;

    //    [SerializeField] private Image iconImage = null;
    //    [SerializeField] private TMP_Text priceText = null;
    [SerializeField] private LayerMask TerrainMask = new LayerMask();

    [SerializeField] private Camera mainCamera;
    private BoxCollider structureCollider;
    private NetworkPlayerGame player;
    private GameObject structurePreviewInstance;
    private Renderer structureRendererInstance;
    private int structureID = -1;

    public void PlaceStructure(int structureId)
    {
        // Only 1 hall per team, return it
        if (structureId == 0 && networkPlayerGame.GetPlayerHall() != null) { return; }

        structureID = structureId;
        structureCollider = networkPlayerGame.structures[structureId].GetComponent<BoxCollider>();

        structurePreviewInstance = Instantiate(networkPlayerGame.structures[structureId].GetUnitPreview());
        structureRendererInstance = structurePreviewInstance.GetComponentInChildren<Renderer>();

        structurePreviewInstance.SetActive(false);
    }

    private void Start()
    {
        // Take "in game" controls
        controls = playerInitialization.Controls;

        // Disable all opened UI
        controls.RealTimeStrategy.Select.started += ctx => disableOpenedUI();

        // Destroy by click and check dor bux fix
        controls.RealTimeStrategy.UnitMovement.canceled += ctx => destroyStructurePreview();
        controls.RealTimeStrategy.UnitMovement.canceled += ctx => unitSelection.IsMultiPlacement = false;
        controls.RealTimeStrategy.UnitMovement.canceled += ctx => unitSelection.isBuild = false;

        //        iconImage.sprite = building.GetIcon();
        //        priceText.text = building.GetPrice().ToString();

        // Take network connection
        player = NetworkClient.connection.identity.GetComponent<NetworkPlayerGame>();

        //        buildingCollider = building.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (structurePreviewInstance == null) { return; }

        structurePreview();
    }

    private void structurePreview()
    {
        // Only 1 hall per team, return it. (Need here for multiple build fix)
        if (structureID == 0 && networkPlayerGame.GetPlayerHall() != null) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, TerrainMask)) { return; }

        structurePreviewInstance.transform.position = hit.point;

        if (!structurePreviewInstance.activeSelf)
        {
            structurePreviewInstance.SetActive(true);
        }

        // Colors for build
        Color color = player.CanPlaceBuilding(structureCollider, hit.point) ? Color.green : Color.red;

        structureRendererInstance.material.SetColor("_BaseColor", color);

        // Place structure for click
        if (controls.RealTimeStrategy.Select.triggered) { stuructureBuild(); }
    }

    private void stuructureBuild()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, TerrainMask))
        {
            // Request for place structure
            player.CmdPlaceStructure(networkPlayerGame.structures[structureID].GetId(), hit.point, networkPlayerGame.GetTeam());
        }

        // Destroy preview instance
        Destroy(structurePreviewInstance);

        // If multiple structure building
        if (unitSelection.MultipleSelectButton.isPressed)
        {
            unitSelection.IsMultiPlacement = true;
            PlaceStructure(structureID);
        }
        else
        {
            unitSelection.IsMultiPlacement = false;
        }
    }

    private void destroyStructurePreview()
    {
        if (structurePreviewInstance != null)
        {
            // Destroy preview instance
            Destroy(structurePreviewInstance);
        }
    }

    private void disableOpenedUI()
    {
        // If pointer on UI - return
        if (playerInitialization.isUISelected) { return; }

        if (controlPanelUI.activeSelf) { controlPanelUI.SetActive(false); }
        if (buildPanelUI.activeSelf) { buildPanelUI.SetActive(false); }
        if (defensePanelUI.activeSelf) { defensePanelUI.SetActive(false); }
        if (structureInfoPanelUI.activeSelf) { structureInfoPanelUI.SetActive(false); }
    }
}