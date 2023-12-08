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

     void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("collision");
        // Debug.Log(other.gameObject.name);

    }


}
