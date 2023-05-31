// using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CController = UnityEngine.CharacterController;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    [SerializeField] private int desiredLane = 1; //0 = left, 1 = middle, 2 = right
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float initalPlayerSpeed = 4f;
    [SerializeField] private float maximumPlayerSpeed = 30f;
    [SerializeField] private float speedIncrease = .1f;
    [SerializeField] AnimationClip slideAnimationClip;
    [SerializeField] private Animator animator;

    private CController controller;
    private float gravity;
    private bool sliding = false;
    bool alive = true;
    private int slidinganimationId;

    public float playerSpeed;
    public float laneDist = 4; //Distance between two lanes
    public float jumpHeight = 1.0f;
    public float intialGravity = -9.81f;

    private void Awake() 
    {
        controller = GetComponent<CController>();

        slidinganimationId = Animator.StringToHash("Sliding");
    }
    private void Start()
    {
        playerSpeed = initalPlayerSpeed;
        gravity = intialGravity;
    }
    private void Update()
    {
        if (!alive) return;
        //Movement
        controller.Move(transform.forward * playerSpeed * Time.deltaTime);
        if (IsGrounded() && dir.y < 0)
        {
            dir.y = 0f;
        }
        dir.y += gravity * Time.deltaTime;
        controller.Move(dir * Time.deltaTime);

        //Jump
        // dir.z = forwardSpeed;
        if (IsGrounded())
        {
            if (SwipeManager.swipeUp) 
            {
                dir.y += Mathf.Sqrt(jumpHeight * gravity * -3f);
                controller.Move(dir * Time.deltaTime);                      
            }
        }
        else
        {
            if (SwipeManager.swipeDown)
            {
                dir.y -= Mathf.Sqrt(jumpHeight * gravity * -3f);
                controller.Move(dir * Time.deltaTime);
            }
        }

        //Slide
        if (!sliding && IsGrounded())
        {
            if (SwipeManager.swipeDown)
            {
                StartCoroutine(Slide());
            }
        }

        //collecting input on which lane we should be
        //by default desiredLane is always 1 which is the middle
        if (SwipeManager.swipeRight)
        {                         
            desiredLane++;         //desiredLane = 2, press right it becomes 3
            if (desiredLane == 3)  //if desiredLane = 3
                desiredLane = 2;   //change it to 2 so it stays on the right
        }
        if (SwipeManager.swipeLeft)
        {
            desiredLane--;         //desiredLane = 0, press left it becomes -1
            if (desiredLane == -1) //if desiredLane = -1
                desiredLane = 0;   //change it to 0 so it stays on the left
        }

        //calculating where we should be next
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDist;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDist;
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.3f); //changing the lanes to right or left

        //speed Increase
        if (playerSpeed < maximumPlayerSpeed)
        {
            playerSpeed += Time.deltaTime * speedIncrease;
            gravity = intialGravity - (playerSpeed * playerSpeed) / (2 * jumpHeight);

            if (animator.speed < 1.25f)
                animator.speed += (1 / playerSpeed) * Time.deltaTime;
        }
    }

    private bool IsGrounded(float length = .2f)
    {
        Vector3 raycastOriginFirst = transform.position;
        raycastOriginFirst.y -= controller.height / 2f;
        raycastOriginFirst.y += .1f;

        Vector3 raycastOriginSecond = raycastOriginFirst;
        raycastOriginFirst -= transform.forward * .2f;
        raycastOriginSecond += transform.forward * .2f;

        if (Physics.Raycast(raycastOriginFirst, Vector3.down, out RaycastHit hit, length, groundLayer) || Physics.Raycast(raycastOriginSecond, Vector3.down, out RaycastHit hit2, length, groundLayer))
        {
            return true;
        }
        return false;
    }
    private IEnumerator Slide()
    {
        Vector3 originalControllerCenter = controller.center;
        Vector3 newControllerCenter = originalControllerCenter;
        controller.height /= 2;
        newControllerCenter.y -= controller.height / 2;
        controller.center = newControllerCenter;

        sliding = true;
        animator.Play(slidinganimationId);
        yield return new WaitForSeconds(slideAnimationClip.length / animator.speed);

        controller.height *= 2;
        controller.center = originalControllerCenter;
        sliding = false;
    }
    public void Die()
    {
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
