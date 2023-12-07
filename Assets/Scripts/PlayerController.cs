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
    // Start is called before the first frame update
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
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + (Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime), transform.position.y + (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime), transform.position.z);
        mainCamera.transform.position = transform.position;
        bool isNowWalking = input.x != 0 || input.y != 0;
        if (isNowWalking)
        {
            if (input.x > 0)
            {
                transform.rotation.y = 180f;
            }
            else if (input.x < 0)
            {
                transform.rotation.y = 0f;
            }
            animationState.SetAnimation(0, "walk", true);
        }
        else
        {
            animationState.SetAnimation(0, "idle", true);
        }
        // if (!isMoving)
        // {

        //     if (input != Vector2.zero)
        //     {
        //         animator.SetFloat("moveX", input.x);
        //         animator.SetFloat("moveY", input.y);

        //         if (isWalkable(targetPos)){ 
        //         //This will run constantly in my game
        //         StartCoroutine(Move(targetPos));
        //         }
        //     }
        // }
    }
    // Function that runs for coroutines
    // This function will run in the routine manner
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // this is only for coroutines

        }
        transform.position = targetPos;
        isMoving = false;

    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

    }


}
