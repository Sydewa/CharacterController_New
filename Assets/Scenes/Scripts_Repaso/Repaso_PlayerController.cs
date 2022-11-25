using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repaso_PlayerController : MonoBehaviour
{
#region Variables
    private CharacterController controller;
    private Transform cam;

    [SerializeField]private float speed = 5f;

    float currentVelocity;
    [SerializeField]private float smoothTime = 0.5f;
    
    [SerializeField]float gravity = -9.81f;
    [SerializeField]float jumpHeight = 1f;
    [SerializeField]Transform groundSensor;
    [SerializeField]float sensorRadius;
    [SerializeField]LayerMask groundLayer;
    [SerializeField]bool isGrounded;

    private Vector3 playerVelocity;
    


#endregion
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
    }


    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        
        if(movement != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg * cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * transform.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, groundLayer);
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if(playerVelocity.y < 0 && isGrounded)
        {
            playerVelocity.y = 0;
        }

        if(isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        

    }
}
