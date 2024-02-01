using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	
	[SerializeField] private float moveSpeed = 10;

	[SerializeField] private float groundDrag = 5;

	[SerializeField] private float jumpForce = 12;
	[SerializeField] private float jumpCooldown = 0.25f;
	[SerializeField] private float airMultiplier = 0.4f;
	bool readyToJump;

	[HideInInspector] public float walkSpeed;
	[HideInInspector] public float sprintSpeed;

	[Header("Keybinds")]

	[SerializeField] private KeyCode jumpKey = KeyCode.Space;

	[Header("Ground Check")]

	[SerializeField] private float playerHeight = 2;
	[SerializeField] private LayerMask whatIsGround;
	bool grounded;

	private Transform orientation;


	float horizontalInput;
	float verticalInput;

	Vector3 moveDirection;

	Rigidbody rb;

	private void Awake()
	{
		orientation = GetComponentInParent<Transform>();
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		readyToJump = true;
	}

	private void Update()
	{
		// ground check
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

		MyInput();
		SpeedControl();

		// handle drag
		if (grounded)
			rb.linearDamping = groundDrag;
		else
			rb.linearDamping = 0;
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	private void MyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		// when to jump
		if (Input.GetKey(jumpKey) && readyToJump && grounded)
		{
			readyToJump = false;

			Jump();

			Invoke(nameof(ResetJump), jumpCooldown);
		}
	}

	private void MovePlayer()
	{
		// calculate movement direction
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		// on ground
		if (grounded)
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

		// in air
		else if (!grounded)
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
	}

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		// limit velocity if needed
		if (flatVel.magnitude > moveSpeed)
		{
			Vector3 limitedVel = flatVel.normalized * moveSpeed;
			rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
		}
	}

	private void Jump()
	{
		// reset y velocity
		rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}
	private void ResetJump()
	{
		readyToJump = true;
	}
}
