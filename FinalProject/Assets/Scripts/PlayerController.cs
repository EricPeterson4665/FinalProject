using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptModels;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;
    private Vector2 inputMovement;
    private Vector2 inputView;

    private Vector2 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;
    public Transform feetTransform;
    private float stanceCheckErrorMargin = 0.05f;

    [Header("Settings")]
    public PlayerSettingsModel playerSettings;
    public LayerMask playerMask;

    [Header("Gravity")]
    public float gravity;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    [Header("Stance")]
    public bool isCrouched;
    public float playerStanceSmothing;
    public float cameraStandHeight;
    public CapsuleCollider standCollider;
    public float cameraCrouchHeight;
    public CapsuleCollider crouchCollider;

    private float cameraHeight;
    private float cameraHeightVelocity;

    private Vector3 stanceCapsuleCenterVelocity;
    private float stanceCapsuleHeightVelocity;

    private bool isSprinting;

    [Header("Weapon")]
    public GameObject weapon;
    private bool isFiring;
    public GameObject flash;
    public Camera aimCam;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        defaultInput.Player.Movement.performed += e => inputMovement = e.ReadValue<Vector2>();
        defaultInput.Player.View.performed += e => inputView = e.ReadValue<Vector2>();
        defaultInput.Player.Jump.performed += e => Jump();
        defaultInput.Player.Crouch.performed += e => Crouch();
        defaultInput.Player.Sprint.performed += e => Sprint();
        defaultInput.Player.SprintReleased.performed += e => StopSprint();
        defaultInput.Player.Fire.performed += e => Fire();
        defaultInput.Player.Exit.performed += e => escExit();
        defaultInput.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();
        cameraHeight = cameraHolder.localPosition.y;
    }

    private void Update()
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
        CalculateStance();

    }

    private void CalculateView()
    {
        newCharacterRotation.y += playerSettings.ViewXSensitivity * inputView.x * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);
        
        newCameraRotation.x += playerSettings.ViewYSensitivity * -inputView.y * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, playerSettings.viewClampYMin, playerSettings.viewClampYMax);
        
        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        if(inputMovement.y <= 0.1f)
        {
            isSprinting = false;
        }
        
        var verticalSpeed = playerSettings.MoveForwardSpeed * inputMovement.y * Time.deltaTime;
        var horizontalSpeed = playerSettings.MoveStrafeSpeed * inputMovement.x * Time.deltaTime;

        if (isSprinting)
        {
            verticalSpeed *= 1.75f;
            horizontalSpeed *= 1.5f;
        }

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if(playerGravity > gravityMin)
        {
            playerGravity -= gravity * Time.deltaTime;
        }
                
        if (playerGravity < -0.1f && characterController.isGrounded)
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(newMovementSpeed);
    }

    private void CalculateStance()
    {
        var stanceHeight = cameraStandHeight;
        var stanceColider = standCollider;
        
        if (isCrouched)
        {
            stanceHeight = cameraCrouchHeight;
            stanceColider = crouchCollider;            
        }
        
        cameraHeight = Mathf.SmoothDamp(cameraHolder.localPosition.y, stanceHeight, ref cameraHeightVelocity, playerStanceSmothing);
        cameraHolder.localPosition = new Vector3(cameraHolder.localPosition.x, cameraHeight, cameraHolder.localPosition.z);
        characterController.height = Mathf.SmoothDamp(characterController.height, stanceColider.height, ref stanceCapsuleHeightVelocity, playerStanceSmothing);
        characterController.center = Vector3.SmoothDamp(characterController.center, stanceColider.center, ref stanceCapsuleCenterVelocity, playerStanceSmothing);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpFalloff);
    }

    private void Jump()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        if (isCrouched)
        {
            isCrouched = false;
            return;
        }

        jumpingForce = Vector3.up * playerSettings.JumpHeight;
        playerGravity = 0;
    }

    private void Crouch()
    {
        if (isCrouched)
        {
            if (!CantStand(standCollider.height))
            {
                isCrouched = false;
            }            
        }
        else
        {
            isCrouched = true;
        }
    }

    private bool CantStand(float checkHeight)
    {
        var start = new Vector3(feetTransform.position.x, feetTransform.position.y + characterController.radius + stanceCheckErrorMargin, feetTransform.position.z);
        var end = new Vector3(feetTransform.position.x, feetTransform.position.y + characterController.radius - stanceCheckErrorMargin + checkHeight, feetTransform.position.z);

        return Physics.CheckCapsule(start, end, characterController.radius, playerMask);
    }

    private void Sprint()
    {
        if (inputMovement.y <= 0.1f)
        {
            isSprinting = false;
            return;
        }

        isSprinting = !isSprinting;
    }

    private void StopSprint()
    {
        isSprinting = false;
    }

    private void Fire()
    {
        if(isFiring == false)
        {
            StartCoroutine(FireAnimation());            
        }
    }

    IEnumerator FireAnimation()
    {
        isFiring = true;
        RaycastHit hit;
        if (Physics.Raycast(aimCam.transform.position, aimCam.transform.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.transform.gameObject.tag == "Target")
            {
                hit.transform.gameObject.SendMessage("TargetHit");
            }
        }
        weapon.GetComponent<Animator>().Play("FirePistol");
        weapon.GetComponent<AudioSource>().Play();
        flash.SetActive(true);
        yield return new WaitForSeconds(0.04f);
        flash.SetActive(false);
        yield return new WaitForSeconds(0.21f);
        weapon.GetComponent<Animator>().Play("New State");
        isFiring = false;
    }

    private void escExit()
    {
        Application.Quit();
    }
}
