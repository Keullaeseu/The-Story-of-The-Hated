using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Mirror;
using Ke.Inputs;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInitialization playerInitialization = null;
    [SerializeField] private Controls controls = null;

    [SerializeField] private float screenEdgeBorder = 25f;
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float movementAccelerated = 30f;
    [SerializeField] private float screenEdgeMovementSpeed = 5f;
    public bool edgeZoneMovment = false;

    [SerializeField] private Transform playerTransform = null;

    [Header("Rotate")]
    [SerializeField] private float minY = 50f;
    [SerializeField] private float maxY = 90f;

    [SerializeField] private float sensX = 10f;
    [SerializeField] private float sensY = 10f;

    private float rotationY = 0f;
    private float rotationX = 0f;

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = .5f;

    // Button's event's
    private bool rotateButtonPressed = false;
    private bool movementAcceleratePressed = false;

    private void Start()
    {
        controls = playerInitialization.Controls;

        // Check perform for accelerate button
        controls.RealTimeStrategy.MovementAccelerate.performed += ctx => movementAcceleratePressed = true;
        controls.RealTimeStrategy.MovementAccelerate.canceled += ctx => movementAcceleratePressed = false;

        // Check perform for button
        controls.RealTimeStrategy.RotateButton.performed += ctx => rotateButtonPressed = true;
        controls.RealTimeStrategy.RotateButton.canceled += ctx => rotateButtonPressed = false;
    }

    private void Update()
    {
        Move();
        Rotation();
        Zoom();
    }

    private void Move()
    {
        Vector3 directionMove = new Vector3(controls.RealTimeStrategy.Movement.ReadValue<Vector2>().x, 0, controls.RealTimeStrategy.Movement.ReadValue<Vector2>().y);

        if (movementAcceleratePressed)
        {
            directionMove *= movementAccelerated;
        }
        else
        {
            directionMove *= movementSpeed;
        }

        directionMove *= Time.deltaTime;
        directionMove = Quaternion.Euler(new Vector3(0f, playerTransform.eulerAngles.y, 0f)) * directionMove;
        directionMove = playerTransform.InverseTransformDirection(directionMove);

        playerTransform.Translate(directionMove, Space.Self);

        if (!edgeZoneMovment) { return; }

        // Create React zone's for mouse
        Rect leftRect = new Rect(0, 0, screenEdgeBorder, Screen.height);
        Rect rightRect = new Rect(Screen.width - screenEdgeBorder, 0, screenEdgeBorder, Screen.height);
        Rect upRect = new Rect(0, Screen.height - screenEdgeBorder, Screen.width, screenEdgeBorder);
        Rect downRect = new Rect(0, 0, Screen.width, screenEdgeBorder);

        directionMove.x = leftRect.Contains(Mouse.current.position.ReadValue()) ? -1 : rightRect.Contains(Mouse.current.position.ReadValue()) ? 1 : 0;
        directionMove.z = upRect.Contains(Mouse.current.position.ReadValue()) ? 1 : downRect.Contains(Mouse.current.position.ReadValue()) ? -1 : 0;

        directionMove *= screenEdgeMovementSpeed;
        directionMove *= Time.deltaTime;
        directionMove = Quaternion.Euler(new Vector3(0f, playerTransform.eulerAngles.y, 0f)) * directionMove;
        directionMove = playerTransform.InverseTransformDirection(directionMove);

        playerTransform.Translate(directionMove, Space.Self);
    }

    private void Rotation()
    {
        if (rotateButtonPressed)
        {
            // Read value from input's and apply sens for each
            rotationX += controls.RealTimeStrategy.Rotate.ReadValue<Vector2>().x * sensX * Time.deltaTime;
            rotationY += controls.RealTimeStrategy.Rotate.ReadValue<Vector2>().y * sensY * Time.deltaTime;

            // Lock rotation angle
            rotationY = Mathf.Clamp(rotationY, minY, maxY);

            playerTransform.localEulerAngles = new Vector3(rotationY, rotationX, 0);
        }
    }

    private void Zoom()
    {
        playerTransform.position += new Vector3(0f, controls.RealTimeStrategy.Zoom.ReadValue<float>() * zoomSpeed * Time.deltaTime, 0f);
    }
}
