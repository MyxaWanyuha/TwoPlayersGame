using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] string horizontal = "Horizontal";
    [SerializeField] string vertical = "Vertical";
    [SerializeField] string jump = "Jump";
    [SerializeField] string attack = "Attack";

    private Transform otherPlayerPosition;

    private Animator animator;
    private float animSpeed;
    private bool animIsInAir;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    
    private float speedMultiplier = 4.0f;
    private float jumpHeight = 4.0f;
    private float gravityValue = 9.81f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.center = new Vector3(0, 0.85f, 0);
        controller.height = 1.7f;
        var otherPlayerTag = tag == "Player1" ? "Player2" : "Player1";
        var otherPlayer = GameObject.FindGameObjectWithTag(otherPlayerTag);
        otherPlayerPosition = otherPlayer.GetComponent<Transform>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            var cam = Camera.main;
            var camf = cam.transform.forward;
            camf.y = 0;
            camf.Normalize();
            var camr = cam.transform.right;
            camr.y = 0;
            camr.Normalize();

            playerVelocity = camf * Input.GetAxis(vertical) + camr * Input.GetAxis(horizontal);
            playerVelocity *= speedMultiplier;
            animSpeed = playerVelocity.sqrMagnitude;
            const float sqrMaxDistanceBetweenPlayers = 29f * 29f;
            var newPosition = (transform.position + playerVelocity * Time.deltaTime);
            if ((newPosition - otherPlayerPosition.position).sqrMagnitude > sqrMaxDistanceBetweenPlayers)
            {
                playerVelocity = otherPlayerPosition.position - newPosition;
            }
            if (playerVelocity.x != 0f || playerVelocity.z != 0f)
            {
                Quaternion direction = Quaternion.LookRotation(playerVelocity);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, 15f * Time.deltaTime);
            }
            if (Input.GetButton(jump))
            {
                playerVelocity.y = jumpHeight;
            }
        }
        animIsInAir = playerVelocity.y > 0;
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        animator.SetFloat("Speed", animSpeed);
        animator.SetBool("IsInAir", animIsInAir);
    }
}
