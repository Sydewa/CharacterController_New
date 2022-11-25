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
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        }


        controller.Move(movement * speed * Time.deltaTime);

    }
}
