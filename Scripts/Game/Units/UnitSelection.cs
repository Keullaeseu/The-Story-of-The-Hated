using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using Ke.Inputs;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitSelection : MonoBehaviour
{
    #region Var

    // Do good thing https://docs.microsoft.com/en-us/windows/mixed-reality/develop/unity/performance-recommendations-for-unity

    [SerializeField] private Camera mainCamera;

    // Controls
    [SerializeField] private PlayerInitialization playerInitialization = null;
    private Controls controls = null;

    [SerializeField] private NetworkPlayerGame networkPlayerGame = null;

    // UI
    [SerializeField] private RectTransform unitSelectionUI = null;
    [SerializeField] private GameObject targetUI = null;
    [SerializeField] private Image targetAvatarUI = null;
    [SerializeField] private Unit targetUnit = null;
    [SerializeField] private int targetUnitInt = -1;    // Uses for target in multiple selection UI
    [SerializeField] private GameObject[] selectedUnitAvatarGameObjectUI = new GameObject[31];
    [SerializeField] private Image[] selectedUnitImageUI = new Image[31];
    [SerializeField] private GameObject[] selectedUnitSelectedGameObjectUI = new GameObject[31];
    private bool isClickedOnUnit = false;

    private Color neturalUnitColor = Color.yellow;
    private Color alliedUnitColor = Color.green;
    private Color enemyUnitColor = Color.red;

    // Layer
    [SerializeField] private LayerMask selectionLayer = new LayerMask();

    // Button mapping
    private ButtonControl selectButton = null;
    [SerializeField] private bool selectDouble = false;
    [HideInInspector] public ButtonControl MultipleSelectButton = null;
    private ButtonControl multipleDeselectButton = null;
    public bool isBuild = false;
    private bool isSelectedBefore = false;
    private bool isUIFirstSelected = false;

    // Start mouse read position
    private Vector2 startMousePosition = Vector2.zero;

    // Multiplacement in strucuture button
    public bool IsMultiPlacement = false;

    // Selected units lists
    public List<Unit> SelectedUnits = new List<Unit>();
    private string doubleSelectedUnit = null;
    private List<Unit> SelectedUnitsMesh = new List<Unit>();

    // Mesh
    private MeshCollider meshCollider = null;
    private Mesh mesh = null;

    #endregion

    private void Start()
    {
        // Take "in game" controls
        controls = playerInitialization.Controls;

        controls.RealTimeStrategy.Select.started += ctx => isUIFirstSelected = playerInitialization.isUISelected;

        // Assign button as ButtonControl
        controls.RealTimeStrategy.Select.performed += ctx => selectButton = controls.RealTimeStrategy.Select.activeControl as ButtonControl;
        controls.RealTimeStrategy.SelectDouble.performed += ctx => selectDouble = true;
        controls.RealTimeStrategy.SelectDouble.canceled += ctx => selectDouble = false;
        controls.RealTimeStrategy.MultipleSelect.performed += ctx => MultipleSelectButton = controls.RealTimeStrategy.MultipleSelect.activeControl as ButtonControl;
        controls.RealTimeStrategy.Deselect.performed += ctx => multipleDeselectButton = controls.RealTimeStrategy.Deselect.activeControl as ButtonControl;

        // Need game manager for correct alloc input, do in next updates
        selectButton = Mouse.current.leftButton;
        MultipleSelectButton = Keyboard.current.leftShiftKey;
        multipleDeselectButton = Keyboard.current.leftCtrlKey;
    }

    private void Update()
    {
        if (IsMultiPlacement) { startMousePosition = Mouse.current.position.ReadValue(); return; }

        if (selectButton.wasPressedThisFrame && !isUIFirstSelected)
        {
            StartSelectionArea();
        }
        else if (selectButton.wasReleasedThisFrame && !isUIFirstSelected)
        {
            ClearSelectionArea();
        }
        else if (selectButton.isPressed && !isUIFirstSelected)
        {
            UpdateSelectionArea();
        }
    }

    private void StartSelectionArea()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        // If our layer
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer))
        {
            bool unitHit = hit.collider.TryGetComponent(out Unit unit);

            // if unit component
            if (SelectedUnits.Count == 0 && unitHit)    // For unit select whithout tagetUI on\off switch
            {
                isClickedOnUnit = true;
            }
            else if (SelectedUnits.Count == 1 && unitHit && SelectedUnits.Contains(unit))   // Fix for double click on selected unit (reverse selection)
            {
                isSelectedBefore = true;

                // Won't do double click on already selected unit
                selectDouble = false;
            }
            else if (SelectedUnits.Count > 1 && unitHit && SelectedUnits.Contains(unit) && !MultipleSelectButton.isPressed && !multipleDeselectButton.isPressed && !isSelectedBefore)   // For selection one unit wich is part of selected list
            {
                isSelectedBefore = true;

                targetUnit = unit;
                targetUnitInt = SelectedUnits.IndexOf(unit);

                selectTargetUnit(targetUnitInt);
            }
            else  // in other cases set false
            {
                isSelectedBefore = false;
            }
        }
        else  // If hit somthing else
        {
            isClickedOnUnit = false;
            isSelectedBefore = false;
        }

        // Clear all selection before new one
        if (!MultipleSelectButton.isPressed && !multipleDeselectButton.isPressed && !isSelectedBefore)
        {
            foreach (Unit unit in SelectedUnits)
            {
                unit.gameObject.GetComponent<UnitUI>().Deselect();
            }

            // Clear arrays for multiple selection
            for (int i = 0; i < SelectedUnits.Count; i++)
            {
                // Break if selected unit more than UI slots
                if (i >= selectedUnitImageUI.Length) { break; }

                selectedUnitImageUI[i].sprite = null;
                selectedUnitAvatarGameObjectUI[i].SetActive(false);
            }

            // If unit were selected before
            if (targetUnitInt >= 0)
            {
                selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);

                targetUnit = null;
                targetUnitInt = -1;
            }

            SelectedUnits.Clear();
        }

        // If SelectedUnits is 0
        if (SelectedUnits.Count == 0 && !isSelectedBefore && !isClickedOnUnit)
        {
            // Disable UI
            targetUI.SetActive(false);

            // Null for avatarUI
            targetAvatarUI.sprite = null;
        }

        // Activate UI from start point
        unitSelectionUI.gameObject.SetActive(true);
        startMousePosition = Mouse.current.position.ReadValue();

        // Drag UI
        UpdateSelectionArea();
    }

    private void UpdateSelectionArea()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float width = mousePosition.x - startMousePosition.x;
        float height = mousePosition.y - startMousePosition.y;

        unitSelectionUI.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        unitSelectionUI.anchoredPosition = startMousePosition + new Vector2(width / 2f, height / 2f);
    }

    private void ClearSelectionArea()
    {
        // Disable UI
        unitSelectionUI.gameObject.SetActive(false);

        // On click magnitude 0 means not drug (Single selction)
        if (unitSelectionUI.sizeDelta.magnitude == 0 && !isBuild)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            // Return if not our layer
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectionLayer)) { return; }
            // Return if not unit component
            if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; }
            // Get unitUI component from gameobject
            UnitUI unitUI = unit.gameObject.GetComponent<UnitUI>();

            // If it's not selected and it's team equals is first unit in list
            if (!SelectedUnits.Contains(unit))
            {
                if (SelectedUnits.Count > 0 && unit.GetTeam() == SelectedUnits[0].GetTeam())
                {
                    // Add to selected list
                    SelectedUnits.Add(unit);
                    // highlight selected UnitUI
                    setColorToUnitUI(unit, unitUI, true);
                    // Send by whom was selected
                    unit.SetPlayerUnitSelection(this);

                    // Add to last multiselected unit
                    selectedUnitImageUI[SelectedUnits.Count - 1].sprite = unit.GetAvatar();
                    selectedUnitAvatarGameObjectUI[SelectedUnits.Count - 1].SetActive(true);

                    // Activate UI for first player
                    if (!selectedUnitAvatarGameObjectUI[0].activeSelf)
                    {
                        selectedUnitAvatarGameObjectUI[0].SetActive(true);
                        selectedUnitSelectedGameObjectUI[0].SetActive(true);
                    }
                }
                else if (SelectedUnits.Count == 0)  // For first unit in list
                {
                    // Add to selected list
                    SelectedUnits.Add(unit);
                    // highlight selected UnitUI
                    setColorToUnitUI(unit, unitUI, true);
                    // Send by whom was selected
                    unit.SetPlayerUnitSelection(this);

                    // Set target
                    targetUnit = unit;
                    targetUnitInt = 0;

                    // Get avatar from unit
                    targetAvatarUI.sprite = targetUnit.GetAvatar();
                    // Set avatar for first player, but not activate UI
                    selectedUnitImageUI[0].sprite = targetUnit.GetAvatar();

                    // Activate UI
                    targetUI.SetActive(true);
                }
            }
            else
            {
                // If double click on same unit
                if (selectDouble)
                {
                    doubleSelectedUnit = unit.name;

                    meshInputValues(1);
                    // Wait fixed update
                    StartCoroutine(WaitForFixedUpdate(unit.GetTeam()));

                    return;
                }

                // If multiple selection btton pressed
                if (MultipleSelectButton.isPressed)
                {
                    multipleSelectButtonVoid(unit);
                }

                // If deselect unit
                if (multipleDeselectButton.isPressed)
                {
                    multipleDeselectButtonVoid(unit);
                }
                else
                {
                    if (!isSelectedBefore)
                    {
                        // Remove selected unit from list
                        SelectedUnits.Remove(unit);
                        // Off highlight
                        unitUI.Deselect();
                    }

                    return;
                }
            }

            return;
        }
        else if (unitSelectionUI.sizeDelta.magnitude > 10)   // Bugfix for "too small mesh" (Multiple selction)
        {
            meshInputValues(0);

            // Wait fixed update
            StartCoroutine(WaitForFixedUpdate(-2));

            return;
        }

        // Change to false, after build structure
        isBuild = false;
    }

    private Mesh MeshSelectionGenerate(Vector3[] edges, Vector3[] sideVertices)
    {
        Vector3[] vertices = new Vector3[8];

        // Array containing all triangles in the mesh
        int[] triangles = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 };

        for (int i = 0; i < 4; i++)
        {
            vertices[i] = edges[i];
        }

        for (int j = 4; j < 8; j++)
        {
            vertices[j] = edges[j - 4] + sideVertices[j - 4];
        }

        // Thank you IDE0017 https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0017
        Mesh selectionMesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles
        };

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If not our selectionLayer return
        if ((selectionLayer.value & (0 << other.gameObject.layer)) > 1) { return; }

        // Get unit component from gameobject
        Unit unit = other.GetComponent<Unit>();
        // Need only gameobject with unit script
        if (unit == null) { return; }
        // Unit selected UI
        UnitUI unitUI = other.gameObject.GetComponent<UnitUI>();

        // If SelectedUnits is empty and we not deselecting
        if (SelectedUnits.Count == 0 && !multipleDeselectButton.isPressed || selectDouble || isSelectedBefore)
        {
            // if SelectDouble firs unit alredy there, just skip him
            if (!SelectedUnits.Contains(unit))
            {
                // Add to selected list
                SelectedUnitsMesh.Add(unit);
            }
        }
        else if (!SelectedUnits.Contains(unit) && MultipleSelectButton.isPressed && unit.GetTeam() == SelectedUnits[0].GetTeam())   // If its unit not in list and multipleSelectButton pressed and unit team and first selectet unit is same
        {
            // Add to selected list
            SelectedUnitsMesh.Add(unit);
            // highlight selected unit
            setColorToUnitUI(unit, unitUI, true);
        }
        else if (SelectedUnits.Contains(unit) && multipleDeselectButton.isPressed && unit.GetTeam() == SelectedUnits[0].GetTeam())  // If unit in list and deselectButton pressed and unit team and first selectet unit is same
        {
            // Remove selected unit from list
            SelectedUnits.Remove(unit);
            // Off highlight
            unitUI.Deselect();
        }
    }

    IEnumerator WaitForFixedUpdate(int team)
    {
        // Wait fixed update (It's necessary because of OnTriggerEnter call)
        yield return new WaitForFixedUpdate();

        // Temp list for enemy and friend units
        List<Unit> friendlyUnits = new List<Unit>();
        List<Unit> enemyUnits = new List<Unit>();

        // If selected unit more than 0
        if (SelectedUnitsMesh.Count > 0)
        {
            // Find friend and enemy and add them to its lists
            foreach (Unit unit in SelectedUnitsMesh)
            {
                // Get unitUI form unit
                UnitUI unitUI = unit.GetComponent<UnitUI>();

                // Set color to unit
                setColorToUnitUI(unit, unitUI, false);

                if (unit.GetTeam() == networkPlayerGame.GetTeam())
                {
                    // Select only same unit type (by name)
                    if (!selectDouble)
                    {
                        // Add to friendlyUnits list
                        friendlyUnits.Add(unit);
                    }
                    else if (selectDouble && doubleSelectedUnit == unit.name && team == unit.GetTeam())
                    {
                        // Add to friendlyUnits list
                        friendlyUnits.Add(unit);
                    }
                }
                else
                {
                    // Select only same unit type (by name)
                    if (!selectDouble)
                    {
                        // Add to enemyUnits list
                        enemyUnits.Add(unit);
                    }
                    else if (selectDouble && doubleSelectedUnit == unit.name && team == unit.GetTeam())
                    {
                        // Add to enemyUnits list
                        enemyUnits.Add(unit);
                    }
                }
            }

            // If friendly units more than 0
            if (friendlyUnits.Count > 0)
            {
                foreach (Unit unit in friendlyUnits)
                {
                    // highlight selected unit
                    unit.gameObject.GetComponent<UnitUI>().Select();
                    // Send by whom was selected
                    unit.SetPlayerUnitSelection(this);
                }

                // Copy list to selected units
                friendlyUnits.CopyTo(SelectedUnits);
            }
            else if (enemyUnits.Count > 0) // If enemy units more than 0
            {
                foreach (Unit unit in enemyUnits)
                {
                    // highlight selected unit
                    unit.gameObject.GetComponent<UnitUI>().Select();
                    // Send by whom was selected
                    unit.SetPlayerUnitSelection(this);
                }

                // Copy list to selected units
                enemyUnits.CopyTo(SelectedUnits);
            }

            // Do only if target unit null
            if (targetUnit == null && targetUnitInt == -1)
            {
                // Set target
                targetUnit = SelectedUnits[0];
                targetUnitInt = 0;

                // Get avatar from unit
                targetAvatarUI.sprite = targetUnit.GetAvatar();
                // Set avatar for first player
                selectedUnitImageUI[0].sprite = targetUnit.GetAvatar();

                // Activate UI
                targetUI.SetActive(true);
            }

            // Activate UI if more than 1 unit and target not selected
            if (SelectedUnits.Count > 1 && targetUnitInt == 0)
            {
                selectedUnitSelectedGameObjectUI[0].SetActive(true);
            }

            // Fill selected uint arrays (Will be error if SelectedUnits.Count above than array, fix in next updates)
            for (int i = 0; i < SelectedUnits.Count; i++)
            {
                // Break if selected unit more than UI slots
                if (i >= selectedUnitImageUI.Length) { break; }

                selectedUnitImageUI[i].sprite = SelectedUnits[i].GetAvatar();

                // If in multiple selection list more than 1 unity enable multiple UI
                if (SelectedUnits.Count > 1)
                {
                    selectedUnitAvatarGameObjectUI[i].SetActive(true);
                }
            }
        }

        // If deselect
        if (multipleDeselectButton.isPressed)
        {
            // Fill selected uint arrays (Will be error if SelectedUnits.Count above than array, fix in next updates)
            for (int i = 0; i < selectedUnitImageUI.Length; i++)
            {
                // Don't break game pls
                if (i > selectedUnitImageUI.Length) { break; }

                // Compare with selected unit list and add\active avatar form unit to UI
                if (i < SelectedUnits.Count)
                {
                    selectedUnitImageUI[i].sprite = SelectedUnits[i].GetAvatar();
                    selectedUnitAvatarGameObjectUI[i].SetActive(true);
                }
                else   // For other clear UI
                {
                    selectedUnitImageUI[i].sprite = null;
                    selectedUnitAvatarGameObjectUI[i].SetActive(false);
                }
            }
        }

        int index = SelectedUnits.IndexOf(targetUnit);

        // If target unit was removed
        if (SelectedUnits.Count != 0 && !SelectedUnits.Contains(targetUnit) || SelectedUnits.Count != 0 && index != targetUnitInt)
        {
            targetUpdate(index);
        }

        // Deactivate targetUI if 0 unit selected
        if (SelectedUnits.Count == 0 && targetUnitInt != -1)
        {
            selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);

            targetDeactivate();
        }
        else if (SelectedUnits.Count == 1 && selectedUnitAvatarGameObjectUI[0].activeSelf)  // Won't show multiple selectedUI
        {
            targetSetZero();
        }

        // Clear mesh selected units
        SelectedUnitsMesh.Clear();

        // Double click off
        selectDouble = false;

        // Destroy after end of frame
        Destroy(meshCollider);
    }

    #region Voids

    // 0 for UI, 1 for double click selection https://cc.davelozinski.com/c-sharp/what-is-the-fastest-conditional-statement
    private void meshInputValues(int value)
    {
        Vector3[] vertices = new Vector3[4];
        Vector3[] sideVertices = new Vector3[4];

        Vector3[] transformEdge = new Vector3[4];
        Ray[] ray = new Ray[4];

        // Debug cube
        //GameObject[] cube = new GameObject[4];

        // 
        if (value == 0)
        {
            unitSelectionUI.GetWorldCorners(transformEdge);
        }
        else if (value == 1)
        {
            transformEdge[0] = new Vector3(0, 0, 0);
            transformEdge[1] = new Vector3(0, Screen.height, 0);
            transformEdge[2] = new Vector3(Screen.width, Screen.height, 0);
            transformEdge[3] = new Vector3(Screen.width, 0, 0);
        }

        for (var i = 0; i < 4; i++)
        {
            ray[i] = mainCamera.ScreenPointToRay(transformEdge[i]);
            if (!Physics.Raycast(ray[i], out RaycastHit hit, Mathf.Infinity)) { return; }

            vertices[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            sideVertices[i] = ray[i].origin - hit.point;

            // Debug cube
            //cube[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube[i].transform.position = hit.point;
        }

        // Mesh generate
        mesh = MeshSelectionGenerate(vertices, sideVertices);

        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // [Physics.PhysX] QuickHullConvexHullLib::findSimplex: Simplex input points appers to be coplanar. (Catcher)
        try
        {
            meshCollider.convex = true;
        }
        catch (System.Exception myException)
        {
            Debug.Log(myException);
            return;
        }

        meshCollider.isTrigger = true;
    }

    private void setColorToUnitUI(Unit unit, UnitUI unitUI, bool select)
    {
        // Cache var
        int unitTeam = unit.GetTeam();
        int playerTeam = networkPlayerGame.GetTeam();

        // Allied unit
        if (unitTeam == playerTeam)
        {
            unitUI.SetColor(alliedUnitColor);
        }
        else if (unitTeam == -1)    // Netural unit
        {
            unitUI.SetColor(neturalUnitColor);
        }
        else  // Enemy unit (Need updates with allied teams)
        {
            unitUI.SetColor(enemyUnitColor);
        }

        // Select after set color to it's team?
        if (select)
        {
            unitUI.Select();
        }
    }

    private void multipleSelectButtonVoid(Unit unit)
    {
        // Get unitUI component from gameobject
        UnitUI unitUI = unit.gameObject.GetComponent<UnitUI>();

        // If unit alredy in selected list
        if (SelectedUnits.Contains(unit))
        {
            // Remove selected unit from list
            SelectedUnits.Remove(unit);
            // Off highlight
            unitUI.Deselect();

            int index = SelectedUnits.IndexOf(targetUnit);

            // If diselected
            if (SelectedUnits.Count != 0 && unit == targetUnit || SelectedUnits.Count != 0 && index != targetUnitInt)
            {
                targetUpdate(index);
            }

            // Fill selected uint arrays (Will be error if SelectedUnits.Count above than array, fix in next updates)
            for (int i = 0; i < selectedUnitImageUI.Length; i++)
            {
                // Don't break game pls
                if (i > selectedUnitImageUI.Length) { break; }

                // Compare with selected unit list and add\active avatar form unit to UI
                if (i < SelectedUnits.Count)
                {
                    selectedUnitImageUI[i].sprite = SelectedUnits[i].GetAvatar();
                    selectedUnitAvatarGameObjectUI[i].SetActive(true);
                }
                else   // For other clear UI
                {
                    selectedUnitImageUI[i].sprite = null;
                    selectedUnitAvatarGameObjectUI[i].SetActive(false);
                }
            }

            // If 0 unit in selected list 
            if (SelectedUnits.Count == 0)
            {
                targetDeactivate();
            }
            else if (SelectedUnits.Count == 1 && selectedUnitAvatarGameObjectUI[0].activeSelf)  // Won't show multiple selectedUI
            {
                targetSetZero();
            }

            return;
        }
        else
        {
            // Add to selected list
            SelectedUnits.Add(unit);
            // highlight selected UnitUI
            setColorToUnitUI(unit, unitUI, true);
            // Send by whom was selected
            unit.SetPlayerUnitSelection(this);

            // Add to last multiselected unit
            selectedUnitImageUI[SelectedUnits.Count - 1].sprite = unit.GetAvatar();
            selectedUnitAvatarGameObjectUI[SelectedUnits.Count - 1].SetActive(true);

            if (!selectedUnitAvatarGameObjectUI[0].activeSelf)
            {
                selectedUnitAvatarGameObjectUI[0].SetActive(true);
            }

            return;
        }
    }

    private void multipleDeselectButtonVoid(Unit unit)
    {
        // Do deselection of the same unit type that clicked
        for (int i = SelectedUnits.Count - 1; i >= 0; i--)
        {
            // Check name in list for remove
            if (unit.name == SelectedUnits[i].name)
            {
                // Off highlight
                SelectedUnits[i].GetComponent<UnitUI>().Deselect();

                // Remove by it's index
                SelectedUnits.RemoveAt(i);
            }
        }

        // Fill selected uint arrays (Will be error if SelectedUnits.Count above than array, fix in next updates)
        for (int i = 0; i < selectedUnitImageUI.Length; i++)
        {
            // Don't break game pls
            if (i > selectedUnitImageUI.Length) { break; }

            // Compare with selecte unit list and add\active avatar form unit to UI
            if (i < SelectedUnits.Count)
            {
                selectedUnitImageUI[i].sprite = SelectedUnits[i].GetAvatar();
                selectedUnitAvatarGameObjectUI[i].SetActive(true);
            }
            else   // For other clear UI
            {
                selectedUnitImageUI[i].sprite = null;
                selectedUnitAvatarGameObjectUI[i].SetActive(false);
            }
        }

        int index = SelectedUnits.IndexOf(targetUnit);

        // If target unit was removed
        if (SelectedUnits.Count != 0 && !SelectedUnits.Contains(targetUnit) || SelectedUnits.Count != 0 && index != targetUnitInt)
        {
            targetUpdate(index);
        }

        // Deactivate targetUI if 0 unit selected
        if (SelectedUnits.Count == 0)
        {
            selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);

            targetDeactivate();
        }
        else if (SelectedUnits.Count == 1 && selectedUnitAvatarGameObjectUI[0].activeSelf)  // Won't show multiple selectedUI
        {
            targetSetZero();
        }

        return;
    }

    private void targetUpdate(int index)
    {
        // Disable selection for prevous target
        selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);

        targetUnitInt = index;

        // Player diselected out target unit
        if (index == -1)
        {
            targetUnitInt = 0;
        }

        // Set unit
        targetUnit = SelectedUnits[targetUnitInt];
        // Get avatar from target unit
        targetAvatarUI.sprite = targetUnit.GetAvatar();

        selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(true);
    }

    private void targetDeactivate()
    {
        // Deactivate UI
        targetUI.SetActive(false);
        // Set null for target avatar
        targetAvatarUI.sprite = null;
        selectedUnitSelectedGameObjectUI[0].SetActive(false);

        // Set unit
        targetUnit = null;
        targetUnitInt = -1;
    }

    private void targetSetZero()
    {
        selectedUnitAvatarGameObjectUI[0].SetActive(false);
        selectedUnitSelectedGameObjectUI[0].SetActive(false);

        // Set unit
        targetUnit = SelectedUnits[0];
        targetUnitInt = 0;

        // Get avatar from first unit
        targetAvatarUI.sprite = targetUnit.GetAvatar();
    }

    #region Button

    private void selectTargetUnit(int unitInt)
    {
        // If clicked second time on selected unit
        if (unitInt == targetUnitInt)
        {
            // Deactivate prevous UI
            selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);
            SelectedUnits[0].GetComponent<UnitUI>().Deselect();

            // Get avatar from target unit
            targetAvatarUI.sprite = targetUnit.GetAvatar();
            // Set to first slot
            SelectedUnits[0] = targetUnit;
            targetUnitInt = 0;

            // Deactivate selection UI
            selectedUnitAvatarGameObjectUI[0].SetActive(false);
            selectedUnitSelectedGameObjectUI[0].SetActive(false);

            for (int i = SelectedUnits.Count - 1; i > 0; i--)
            {
                // Break if selected unit more than UI slots
                //if (i >= selectedUnitImageUI.Length) { break; }

                // If not target unit
                if (i != targetUnitInt)
                {
                    // If selected unit in size of unit avatar count
                    if (i < selectedUnitImageUI.Length)
                    {
                        // Deactivate UI
                        selectedUnitAvatarGameObjectUI[i].SetActive(false);
                    }

                    // Off highlight
                    SelectedUnits[i].GetComponent<UnitUI>().Deselect();
                    // Remove it
                    SelectedUnits.RemoveAt(i);
                }
            }

            // Activate selected UI for target unit
            SelectedUnits[0].GetComponent<UnitUI>().Select();

            return;
        }

        // Deselect prevous UI
        if (targetUnit != null)
        {
            selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(false);
        }

        targetUnit = SelectedUnits[unitInt];
        targetUnitInt = unitInt;

        // Get avatar from target unit
        targetAvatarUI.sprite = targetUnit.GetAvatar();
        // Activate selection UI
        selectedUnitSelectedGameObjectUI[unitInt].SetActive(true);
    }

    public void SelectTargetUI(int unitInt)
    {
        if (MultipleSelectButton.isPressed)
        {
            multipleSelectButtonVoid(SelectedUnits[unitInt]);
        }
        else if (multipleDeselectButton.isPressed)
        {
            multipleDeselectButtonVoid(SelectedUnits[unitInt]);
        }
        else
        {
            selectTargetUnit(unitInt);
        }
    }

    #endregion

    #endregion


    #region Set



    #endregion

    #region Get

    public int GetSelectedUnitImageUICount()
    {
        return selectedUnitImageUI.Length;
    }

    public int GetSelectedUnitsCount()
    {
        return SelectedUnits.Count;
    }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    // Need check how to work
    public void GetTargetUpdate(Unit unitValue)
    {
        if (targetUnit == unitValue)
        {
            // Cache
            int unitValueIndex = SelectedUnits.IndexOf(unitValue);

            // If not first unit in selectionUI
            if (unitValueIndex != 0)
            {
                // Update to upper unit
                targetUpdate(unitValueIndex - 1);
            }

            // Remove from selected list
            SelectedUnits.Remove(unitValue);
        }
        else
        {
            // Remove from selected list
            SelectedUnits.Remove(unitValue);

            // Update target unitInt for new position of target unit
            targetUnitInt = SelectedUnits.IndexOf(targetUnit);
            // Enable selectedUI for new place in UI
            selectedUnitSelectedGameObjectUI[targetUnitInt].SetActive(true);
        }

        // Recalculate UI for selected units
        for (int i = 0; i < SelectedUnits.Count; i++)
        {
            // Break if selected unit more than UI slots
            if (i >= selectedUnitImageUI.Length) { break; }

            selectedUnitImageUI[i].sprite = SelectedUnits[i].GetAvatar();
        }

        // Deactivate units UI
        for (int i = selectedUnitImageUI.Length - 1; i > SelectedUnits.Count - 1; i--)
        {
            // Deactivate UI
            selectedUnitAvatarGameObjectUI[i].SetActive(false);
            selectedUnitImageUI[i].sprite = null;
            selectedUnitSelectedGameObjectUI[i].SetActive(false);
        }

        // If only one unit in selected list, deactivate multi UI
        if (SelectedUnits.Count == 1)
        {
            selectedUnitAvatarGameObjectUI[0].SetActive(false);
            selectedUnitSelectedGameObjectUI[0].SetActive(false);
        }
    }

    public void GetTargetDeactivate()
    {
        targetDeactivate();
    }

    #endregion

}