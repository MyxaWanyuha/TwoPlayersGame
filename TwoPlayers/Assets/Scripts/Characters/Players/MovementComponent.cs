using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] string horizontal = "Horizontal";
    [SerializeField] string vertical = "Vertical";
    [SerializeField] string jump = "Jump";
    [SerializeField] string attack = "Attack";

    Transform otherPlayerPosition;

    private CharacterController controller;
    private Vector3 playerVelocity = Vector3.zero;
    
    private float playerSpeed = 4.0f;
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
            playerVelocity *= playerSpeed;

            const float sqrMaxDistanceToCenter = 24f * 24f;
            const float sqrMaxDistanceBetweenPlayers = 10f * 10f;
            var newPosition = (transform.position + playerVelocity * Time.deltaTime);
            var sqrDistanceToCenter = newPosition.sqrMagnitude;
            if (sqrDistanceToCenter > sqrMaxDistanceToCenter 
                || (newPosition - otherPlayerPosition.position).sqrMagnitude > sqrMaxDistanceBetweenPlayers)
                playerVelocity = Vector3.zero;

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
        
        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
