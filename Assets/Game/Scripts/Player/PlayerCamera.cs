using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	[SerializeField] private float sensX = 400.0f;
	[SerializeField] private float sensY = 400.0f;

	private Transform orientation;

	private float xRotation;
	private float yRotation;

	private void Awake()
	{
		orientation = GetComponentInParent<Transform>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update()
	{
		// get mouse input
		float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
		float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

		yRotation += mouseX;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		// rotate cam and orientation
		orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
	}
}
