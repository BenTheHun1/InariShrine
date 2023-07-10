using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private float mouseSensitivity = 100f;
	public Transform playerBody;
	public PlayerController pc;
	public Camera cam;
	private float xRotation = 0f;
	private RaycastHit hit;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit, 4f))
		{
			//print(hit.transform.gameObject.name);
			pc.ray = hit;
		}

		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

		playerBody.Rotate(Vector3.up * mouseX);
	}
}
