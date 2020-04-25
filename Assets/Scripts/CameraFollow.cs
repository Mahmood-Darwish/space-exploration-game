using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	float Sensitivity = 10f;
	float xRotation = 0f;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;
	}

	void Update()
	{
		float MouseX = Input.GetAxis("Mouse X") * Sensitivity;
		float MouseY = Input.GetAxis("Mouse Y") * Sensitivity;
		transform.Rotate(Vector3.up * MouseX);
		xRotation -= MouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 90);
		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
	}

}