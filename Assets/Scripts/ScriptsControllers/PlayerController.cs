using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] private bool lockCursor = true;

    [SerializeField] private bool canNew = true;

    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private CharacterController controller = null;

    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;

    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private GameObject lastGunUsed;
    private GameObject currentGun;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentGun)
            {
                currentGun.GetComponent<GunsManager>().ShootNow();
            }
        }
        
        UpdateMouseLook();
        UpdateMovement();
    }

    private void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    private void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if(controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;
		
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Gun"))
        {
            ClearOtherGuns();
            SetGunInPlayer(other.gameObject);
            if (canNew)
            {
            }
        }
    }

    private void ClearOtherGuns()
    {
        if (lastGunUsed)
        {
            Debug.Log("entro 1");
            lastGunUsed.transform.position = lastGunUsed.transform.GetComponent<GunsManager>().posInitial;
            lastGunUsed.transform.parent = null;
            currentGun.transform.GetComponent<BoxCollider>().enabled = true;
            lastGunUsed = null;
            Debug.Log("entro 2");
        }
    }

    private void SetGunInPlayer(GameObject gun)
    {
        currentGun = gun;
        currentGun.transform.position = transform.GetChild(1).transform.position;
        currentGun.transform.rotation = transform.GetChild(1).transform.rotation;
        currentGun.transform.GetComponent<BoxCollider>().enabled = false;
        currentGun.transform.parent = transform;
        lastGunUsed = currentGun;
    }
}
