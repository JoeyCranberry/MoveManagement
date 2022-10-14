using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public bool useMouseControl = true;

    public float PanSpeed = 20f;
    public float PanBorderThickness = 10f;
    public float ScrollSpeed = 20f;

    public Vector2 cameraHorizontalMoveLimit;
    public Vector2 cameraVerticalLimit;

    private float scrollSpeedMultiplier = 100f;

    // Update is called once per frame
    void Update()
    {
        Vector3 updatePosition = GetHorizontalMovement(transform.position);
        updatePosition = GetVerticalMovement(updatePosition);

        transform.position = updatePosition;
    }

    /*
     * Get movement on the x and z axis
     */
    private Vector3 GetHorizontalMovement(Vector3 originalPosition)
    {
        Vector3 newPosition = originalPosition;

        // Get keyboard and mouse inputs
        // On the z
        if (Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - PanBorderThickness && useMouseControl))
        {
            newPosition.z += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= PanBorderThickness && useMouseControl))
        {
            newPosition.z -= PanSpeed * Time.deltaTime;
        }

        // On the x
        if (Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - PanBorderThickness && useMouseControl))
        {
            newPosition.x += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= PanBorderThickness && useMouseControl))
        {
            newPosition.x -= PanSpeed * Time.deltaTime;
        }

        // Clamp horizontal movement to negative/positive move limit
        newPosition.x = Mathf.Clamp(newPosition.x, -cameraHorizontalMoveLimit.x, cameraHorizontalMoveLimit.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -cameraHorizontalMoveLimit.y, cameraHorizontalMoveLimit.y);

        return newPosition;
    }

    /*
     * Get movement on the z axis
     */
    private Vector3 GetVerticalMovement(Vector3 originalPosition)
    {
        Vector3 newPosition = originalPosition;

        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        newPosition.y -= scrollAmount * Time.deltaTime * ScrollSpeed * scrollSpeedMultiplier;

        newPosition.y = Mathf.Clamp(newPosition.y, cameraVerticalLimit.x, cameraVerticalLimit.y);

        return newPosition;
    }
}
