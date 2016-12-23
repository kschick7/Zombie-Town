using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float mouseSensitivity;
    public float maxVerticalViewAngle;

    CharacterController cc;
    float verticalRotation;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        verticalRotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Movement
        float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 speed = transform.rotation * (new Vector3(sideSpeed, 0, forwardSpeed));
        cc.SimpleMove(speed);

        // Camera movement
        float horizontalDeltaRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalDeltaRotation, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalViewAngle, maxVerticalViewAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
       
	}
}
