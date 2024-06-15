using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    float movementSpeed;

    [SerializeField]
    float lookSpeed;

    private float currentX, currentY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentX = playerCamera.transform.localEulerAngles.x;
        currentY = transform.eulerAngles.y;
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
        float moveZ = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;
        controller.Move(moveX * transform.right + moveZ * transform.forward);

        float lookX = -Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        float lookY = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;

        currentX = Mathf.Clamp(currentX + lookX, -90f, 90f);
        currentY = (currentY + lookY) % 360f;
        playerCamera.transform.localEulerAngles = new Vector3(currentX, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, currentY, transform.eulerAngles.z);
    }
}