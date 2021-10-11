using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManagerMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerGame networkManagerGame = null;
    [SerializeField] private MenuInputManager menuInputManager = null;

    [SerializeField] public GameObject MainCanvasBackground = null;
    [SerializeField] public GameObject MainMenuUI = null;

    [SerializeField] public GameObject ConnectMenuUI = null;
    [SerializeField] public GameObject LobbyMenuUI = null;
    [SerializeField] public GameObject LobbyPlayersMenuUI = null;

    [SerializeField] public GameObject CampainMap = null;
    [SerializeField] public GameObject CampainButton = null;
    [SerializeField] public GameObject LobbyBackButton = null;
    [SerializeField] public GameObject LobbyBackText = null;
    [SerializeField] public GameObject PlayCoopButton = null;
    [SerializeField] public GameObject PlaySinglePlayerButton = null;
    [SerializeField] public GameObject PlayBackButton = null;
    [SerializeField] public GameObject PlayCancelCoopButton = null;
    [SerializeField] public GameObject ReadyButton = null;
    [SerializeField] public GameObject StartGameButton = null;
    [SerializeField] public EventSystem eventSystem = null;

    [SerializeField] public List<GameObject> MissionList = new List<GameObject>();

    [HideInInspector] public bool isUISelected = false;

    // UI check
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private LayerMask selectionLayer = new LayerMask();

    [SerializeField] private GameObject MissionMenuUI = null;
    [SerializeField] private Image MissionImage = null;

    [SerializeField] private GameObject SelectedMission = null;

    private void Awake()
    {
        menuInputManager.Controls.RealTimeStrategy.Select.performed += ctx => clickCheck();
    }

    private void Start()
    {
        // Fix while ofline scene
        networkManagerGame = NetworkManager.singleton.GetComponent<NetworkManagerGame>();
    }

    public void ClientInitialization()
    {
        LobbyMenuUI.SetActive(true);
        LobbyPlayersMenuUI.SetActive(true);

        PlayCoopButton.SetActive(false);
        PlaySinglePlayerButton.SetActive(false);
        PlayBackButton.SetActive(false);
        PlayCancelCoopButton.SetActive(true);
        ReadyButton.SetActive(true);
    }

    private void clickCheck()
    {
        if (isUISelected) { return; }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        // Return if not our layer
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer)) { return; }

        RaycastHit raycastHit = hit;
        // Return if not mission component
        if (!raycastHit.collider.TryGetComponent<Mission>(out Mission mission)) { return; }
        raycastHit.collider.TryGetComponent<TextUI>(out TextUI textUI);

        // Set mission
        networkManagerGame.MissionScene = mission.GetMissionScene();
        // Set preview of mission
        MissionImage.sprite = mission.GetPreview();
        textUI.text.color = Color.red;

        // Set mission
        SelectedMission = mission.gameObject;
        LobbyBackButton.SetActive(false);

        // Activate preview card
        if (!MissionMenuUI.activeSelf)
        {
            MissionMenuUI.SetActive(true);
        }

        eventSystem.SetSelectedGameObject(PlaySinglePlayerButton);
    }

    // Client call this while connected to server
    public void SetMission(int index)
    {
        Mission mission = MissionList[index].GetComponent<Mission>();
        TextUI textUI = MissionList[index].GetComponent<TextUI>();

        // Set preview of mission
        MissionImage.sprite = mission.GetPreview();
        textUI.text.color = Color.red;

        MissionMenuUI.SetActive(true);

        eventSystem.SetSelectedGameObject(ReadyButton);
    }

    public void BackToCampainMap()
    {
        SelectedMission.GetComponent<TextUI>().text.color = Color.white;
        eventSystem.SetSelectedGameObject(LobbyBackButton);
        MissionMenuUI.SetActive(false);
    }

    public void StartGame()
    {
        NetworkManager.singleton.GetComponent<NetworkManagerGame>().StartGame();
    }
}