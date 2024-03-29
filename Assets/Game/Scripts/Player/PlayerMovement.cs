using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	private float moveSpeed;
	public float walkSpeed;
	public float sprintSpeed;

	public float groundDrag;


	[Header("Jumping")]
	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;
	bool readyToJump;

	[Header("Crouching")]
	public float crouchSpeed;
	public float crouchYScale;
	private float startYScale;

	[Header("Keybinds")]
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode sprintKey = KeyCode.LeftShift;
	public KeyCode crouchKey = KeyCode.LeftControl;

	[Header("Ground Check")]
	public float playerHeight;
	public LayerMask whatIsGround;
	bool grounded;

	[Header("Slope Handing")]
	public float maxSlopeAngle;
	private RaycastHit slopeHit;
	private bool exitingSlope;

	public Transform orientation;

	float horizontalInput;
	float verticalInput;

	Vector3 moveDirection;

	Rigidbody rb;

	public enum MovementState
	{
		walking,
		sprinting,
		crouching,
		air
	}
	public MovementState state;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;

		readyToJump = true;

		startYScale = transform.localScale.y;
	}

	private void Update()
	{
		// ground check
		grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

		MyInput();
		SpeedControl();
		StateHandler();

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

		if (Input.GetKeyDown(crouchKey))
		{
			transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
			rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
		}
		if (Input.GetKeyUp(crouchKey))
		{
			transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
		}
	}

	private void StateHandler()
	{
		if (Input.GetKey(crouchKey))
		{
			state = MovementState.crouching;
			moveSpeed = crouchSpeed;
		}

		if (grounded && Input.GetKey(sprintKey))
		{
			state = MovementState.sprinting;
			moveSpeed = sprintSpeed;
		}
		else if (grounded)
		{
			state = MovementState.walking;
			moveSpeed = walkSpeed;
		}
		else
		{
			state = MovementState.air;
		}
	}

	private void MovePlayer()
	{
		// calculate movement direction
		moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

		// on slope
		if (OnSlope() && !exitingSlope)
		{
			rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20.0f, ForceMode.Force);

			if (rb.linearVelocity.y > 0)
				rb.AddForce(Vector3.down * 80f, ForceMode.Force);
		}

		// on ground
		else if (grounded)
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

		// in air
		else if (!grounded)
			rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

		// turn gravity off while on slope
		rb.useGravity = !OnSlope();

	}

	private void SpeedControl()
	{
		// limiting speed on slope
		if (OnSlope() && !exitingSlope)
		{
			if (rb.linearVelocity.magnitude > moveSpeed)
				rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
		}
		// limiting speed on groun or in air
		else
		{
			Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

			// limit velocity if needed
			if (flatVel.magnitude > moveSpeed)
			{
				Vector3 limitedVel = flatVel.normalized * moveSpeed;
				rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
			}
		}
	}

	private void Jump()
	{
		exitingSlope = true;

		// reset y velocity
		rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}
	private void ResetJump()
	{
		readyToJump = true;
		exitingSlope = false;
	}

	private bool OnSlope()
	{
		if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
		{
			float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
			return angle < maxSlopeAngle && angle != 0;
		}
		return false;
	}

	private Vector3 GetSlopeMoveDirection()
	{
		return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
	}
}
