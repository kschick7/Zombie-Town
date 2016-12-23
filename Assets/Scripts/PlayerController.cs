using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float mouseSensitivity;
    public float maxVerticalViewAngle;
    public float jumpSpeed;

    CharacterController cc;
    float verticalRotation;
    float yVelocity;

	// Use this for initialization
	void Start () {
        cc = GetComponent<CharacterController>();
        verticalRotation = 0;
        yVelocity = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        // Camera movement
        float horizontalDeltaRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalDeltaRotation, 0);
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalViewAngle, maxVerticalViewAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movement
        float forwardSpeed = Input.GetAxis("Vertical") * moveSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * moveSpeed;
        Vector3 vel = transform.rotation * (new Vector3(sideSpeed, 0, forwardSpeed));

        // Jumping
        yVelocity += Time.deltaTime * Physics.gravity.y;
        if (cc.isGrounded && Input.GetAxis("Jump") > 0) {
            Debug.Log("Jump");
            yVelocity = jumpSpeed;
        }

        vel.y = yVelocity;
        cc.Move(vel * Time.deltaTime);
    }
}
