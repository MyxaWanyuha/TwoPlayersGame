using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 4f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 300;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = false;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 4f;

    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    [Header("ControlBindings")]
    [SerializeField] string horizontal = "Horizontal";
    [SerializeField] string vertical = "Vertical";
    [SerializeField] string jump = "Jump";
    [SerializeField] string attack = "Attack";

    private Transform otherPlayerPosition;

    private Animator animator;
    private float animSpeed;
    private bool animIsInAir;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessActions();
        animator.SetBool("IsInAir", IsGrounded == false);
    }

    private void Start()
    {
        var otherPlayerTag = tag == "Player1" ? "Player2" : "Player1";
        var otherPlayer = GameObject.FindGameObjectWithTag(otherPlayerTag);
        otherPlayerPosition = otherPlayer.GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        ForwardInput = Mathf.RoundToInt(Input.GetAxis(vertical));
        TurnInput = Mathf.RoundToInt(Input.GetAxis(horizontal));
        JumpInput = Input.GetButton(jump);
    }

    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    IsGrounded = true;
            }
        }
    }

    private void ProcessActions()
    {
        var cam = Camera.main;
        var camf = cam.transform.forward;
        camf.y = 0;
        camf.Normalize();
        var camr = cam.transform.right;
        camr.y = 0;
        camr.Normalize();

        var move = camf * Mathf.Clamp(Input.GetAxis(vertical), -1f, 1f) 
                 + camr * Mathf.Clamp(Input.GetAxis(horizontal), -1f, 1f);
        move *= moveSpeed;
        animator.SetFloat("Speed", move.sqrMagnitude);

        if (move.x != 0f || move.z != 0f)
        {
            Quaternion direction = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, 15f * Time.deltaTime);
        }
        //var newPosition = (transform.position + move);
        //const float MaxDistanceBetweenPlayers = 24f;
        //const float sqrMaxDistanceBetweenPlayers = MaxDistanceBetweenPlayers * MaxDistanceBetweenPlayers;
        //if ((newPosition - otherPlayerPosition.position).sqrMagnitude > sqrMaxDistanceBetweenPlayers)
        //{
        //    move = Vector3.zero;//otherPlayerPosition.position - newPosition;
        //}

        rigidbody.MovePosition(transform.position + move * Time.deltaTime);
        // Jump
        if (allowJump && JumpInput && IsGrounded)
        {
            rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
        }
    }
}
/*
MIT License

Copyright (c) 2021 Immersive Limit LLC

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/