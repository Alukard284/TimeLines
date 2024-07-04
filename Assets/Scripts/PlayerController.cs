using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("MovementSettings")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float rotatinonSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] private float jumpHorizontalSpeed;
    private float speed;
    private float sprintTransitionSpeed = 5f;
    private float ySpeed;
    private bool isJunping;
    private bool isGrounded;

    [Header("PlayerInput")]
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] bool jump;

    [Header("Preferenses")]
    private CharacterController controller;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Animator animator;
    private float originalStepOffeset;

    [Header("Animation")]
    private int animMoveSpeed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        originalStepOffeset = controller.stepOffset;
        SetupAnimator();
    }

    private void Update()
    {
        PlayerInput();
        PlayerMovement();
        PlayerJump();
    }
    private void PlayerInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");

    }
    private void PlayerMovement()
    {
        Vector3 movementDirection = new Vector3(horizontal, 0f, vertical);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        Vector3 velosity = movementDirection * magnitude;
        velosity.y = ySpeed;
        controller.Move(velosity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);

        }
        else
        {
            speed = Mathf.Lerp(speed, movementSpeed, sprintTransitionSpeed * Time.deltaTime);

        }
        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotatinonSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        //Animation
        animator.SetFloat(animMoveSpeed, speed * Mathf.Max(Mathf.Abs(vertical), Mathf.Abs(horizontal)));
    }
    private void PlayerJump()
    {
        if (controller.isGrounded)
        {
            controller.stepOffset = originalStepOffeset;
            ySpeed = -0.5f;
            animator.SetBool("isGrounded", true);
            isGrounded = true;
            animator.SetBool("isJumping", false);
            isJunping = false;
            animator.SetBool("isFalling", false);
            if (jump)
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
                isJunping = true;
            }
        }
        else
        {
            controller.stepOffset = 0f;
            animator.SetBool("isGrounded", false);
            isGrounded = false;

            if ((isJunping && ySpeed <0) || ySpeed < -2)
            {
                animator.SetBool("isFalling", true);
            }
        }
    }
    private void SetupAnimator()
    {
        animMoveSpeed = Animator.StringToHash("MovengSpeed");
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void OnAnimatorMove()
    {
        if ((isGrounded))
        {
            Vector3 velosity = animator.deltaPosition;
            velosity.y = ySpeed * Time.deltaTime;
            controller.Move(velosity);
        }
    }
}
