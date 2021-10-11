using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System;

public class NetworkPlayerGame : NetworkBehaviour
{
    [SerializeField] private GameObject playerManager = null;
    [SerializeField] private PlayerInitialization playerInitialization = null;

    // Array of structures
    public Unit[] structures = new Unit[0];
    // Array of units
    public Unit[] units = new Unit[0];
    [SerializeField] private LayerMask structurBlockLayer = new LayerMask();

    [SerializeField] private UnitSelection unitSelection = null;

    [SyncVar]
    [SerializeField] private string displayName = "Loading...";
    [SyncVar]
    [SerializeField] private int team = -1;
    [SyncVar]
    [SerializeField] private bool teamLeader = false;
    [SyncVar]
    [SerializeField] private int slot = -1;

    // Money UI
    [SerializeField] private TMP_Text moneyUI = null;
    private int money = 0;

    [Header("Player side - Hall")]
    [SerializeField] private GameObject playerHall = null;

    [Header("Production unit list")]
    public List<GameObject> BarracksList = new List<GameObject>();

    private NetworkManagerGame lobby;
    private NetworkManagerGame Lobby
    {
        get
        {
            if (lobby != null) { return lobby; }
            return lobby = NetworkManager.singleton as NetworkManagerGame;
        }
    }

    public void Initialization()
    {
        if (isLocalPlayer)
        {
            playerManager.SetActive(true);
            playerInitialization.enabled = true;
            playerInitialization.Initialization();

            // Money UI
            moneyUI.text = money.ToString();
        }
    }

    #region Set

    [Server]
    public void SetDisplayName(string displayNameValue)
    {
        displayName = displayNameValue;
    }

    [Server]
    public void SetTeam(int teamValue)
    {
        team = teamValue;
    }

    [Server]
    public void SetTeamLeader(bool teamLeaderValue)
    {
        teamLeader = teamLeaderValue;
    }

    [Server]
    public void SetSlot(int slotValue)
    {
        slot = slotValue;
    }

    public void SetMoney(int value)
    {
        money = value;

        // Money UI
        moneyUI.text = money.ToString();
    }
        
    public void SetPlayerHall(GameObject value)
    {
        playerHall = value;
    }

    #endregion

    #region Get

    public int GetTeam()
    {
        return team;
    }

    public int GetSlot()
    {
        return slot;
    }

    public GameObject GetPlayerHall()
    {
        return playerHall;
    }

    #endregion

    #region Server

    [Server]
    public void SpawnUnitOnStart(int unitId, Vector3 point, int team, GameObject unitSpawnOwner)
    {
        Unit unitToSpawn = null;

        // Find wich one unit need to spawn by it id
        foreach (Unit unit in units)
        {
            if (unit.GetId() == unitId)
            {
                unitToSpawn = unit;
                break;
            }
        }

        if (unitToSpawn == null) { return; }

        // Cache
        GameObject unitInstance = Instantiate(unitToSpawn.gameObject, point, unitToSpawn.transform.rotation);
        Unit unitInstanceUnitComponent = unitInstance.GetComponent<Unit>();
        UnitSpawnOnStartControl unitSpawnOnStartControlComponent = unitSpawnOwner.GetComponent<UnitSpawnOnStartControl>();

        // Find team leader and give autority to its unit
        foreach (var player in Lobby.GamePlayers)
        {
            if (player.teamLeader && player.team == team)
            {
                // Spawn it and give it's unit team and born sturcture unit
                NetworkServer.Spawn(unitInstance, player.connectionToClient);
                unitInstanceUnitComponent.SetTeam(team);
                unitInstanceUnitComponent.gameObject.GetComponent<UnitSpawnOnStart>().SetUnitSpawnedOnStartControl(unitSpawnOnStartControlComponent);

                // Add unit to unit production list
                unitSpawnOnStartControlComponent.AddUnitSpawned(unitInstanceUnitComponent);

                break;
            }
        }
    }

    #endregion

    public bool CanPlaceBuilding(BoxCollider buildingCollider, Vector3 point)
    {
        return !Physics.CheckBox(point + buildingCollider.center, buildingCollider.size / 2, Quaternion.identity, structurBlockLayer);
    }

    #region Client

    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);

        Lobby.GamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Lobby.GamePlayers.Remove(this);
    }

    #region Command

    [Command]
    public void CmdPlaceStructure(int structureId, Vector3 point, int team)
    {
        Unit structureToPlace = null;

        // Find wich one structure need to build by it id
        foreach (Unit structure in structures)
        {
            if (structure.GetId() == structureId)
            {
                structureToPlace = structure;
                break;
            }
        }

        if (structureToPlace == null) { return; }

        //        if (resources < buildingToPlace.GetPrice()) { return; }

        BoxCollider structureCollider = structureToPlace.GetComponent<BoxCollider>();

        if (!CanPlaceBuilding(structureCollider, point)) { return; }

        // After build we won't select this structure
        unitSelection.isBuild = true;

        GameObject structureInstance = Instantiate(structureToPlace.gameObject, point, structureToPlace.transform.rotation);

        // Find team leader and give autority to its structure
        foreach (var player in Lobby.GamePlayers)
        {
            if (player.teamLeader && player.team == team)
            {
                // Spawn it and give it's stucture team
                NetworkServer.Spawn(structureInstance, player.connectionToClient);
                structureInstance.GetComponent<Unit>().SetTeam(team);

                break;
            }
        }

        //        SetResources(resources - buildingToPlace.GetPrice());
    }

    [Command]
    public void CmdSpawnProductionUnit(int unitId, Vector3 point, int team, GameObject unitProduction)
    {
        Unit unitToSpawn = null;

        // Find wich one unit need to spawn by it id
        foreach (Unit unit in units)
        {
            if (unit.GetId() == unitId)
            {
                unitToSpawn = unit;
                break;
            }
        }

        if (unitToSpawn == null) { return; }

        // Cache
        GameObject unitInstance = Instantiate(unitToSpawn.gameObject, point, unitToSpawn.transform.rotation);
        Unit unitInstanceUnitComponent = unitInstance.GetComponent<Unit>();
        StructureUnitProduction unitProductionComponent = unitProduction.GetComponent<StructureUnitProduction>();

        // Find team leader and give autority to its unit
        foreach (var player in Lobby.GamePlayers)
        {
            if (player.teamLeader && player.team == team)
            {
                // Spawn it and give it's unit team and born unit production
                NetworkServer.Spawn(unitInstance, player.connectionToClient);
                unitInstanceUnitComponent.SetTeam(team);
                unitInstanceUnitComponent.gameObject.GetComponent<UnitProduction>().SetUnitProductioStructure(unitProductionComponent);

                // Add unit to unit production list
                unitProductionComponent.AddUnitSpawned(unitInstanceUnitComponent);

                break;
            }
        }
    }

    #endregion

    #endregion

}