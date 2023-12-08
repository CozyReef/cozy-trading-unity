using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    private Animator animator;
    public LayerMask solidObjectsLayer;

    public Camera mainCamera;

    protected SkeletonAnimation skeletonAnimation;
    protected Spine.AnimationState animationState;
    protected Spine.Skeleton skeleton;

    private bool canMove = true;

    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeleton = skeletonAnimation.Skeleton;
        animationState = skeletonAnimation.AnimationState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime), transform.position.y + (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime), transform.position.z);
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (input.x > 0)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                180f,
                transform.eulerAngles.z
            );
        }
        else if (input.x < 0)
        {
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                0f,
                transform.eulerAngles.z
            );
        }
        bool isNowWalking = input.x != 0 || input.y != 0;
        if (isNowWalking && !isMoving)
        {
            animationState.SetAnimation(0, "walk", true);
        }
        else if (!isNowWalking && isMoving)
        {
            animationState.SetAnimation(0, "idle", true);
        }
        isMoving = isNowWalking;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("collision");
        // Debug.Log(other.gameObject.name);
    }

    public void SetIsMining(bool isMining)
    {
        if (isMining)
        {
            animationState.SetAnimation(0, "mining", true);
            canMove = false;
        }
        else
        {
            animationState.SetAnimation(0, "idle", true);
            canMove = true;
        }
    }
}
