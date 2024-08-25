using System.Collections.Generic;
using UnityEngine;
public class Character_Controller : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private List<GameObject> Characters = new();

    [Header("Controller Limits")]
    [SerializeField] private float Max_Left = -5;
    [SerializeField] private float Max_Right = 5;

    [Header("Movement Parameters")]
    [SerializeField] private float Movement_Speed = 7f;
    //[SerializeField] private Vector3 targetPosition;

    [Header("Local Components")]
    [SerializeField] private Rigidbody rb;

    [Header("Foreign Componenets")]
    [SerializeField] private Camera MainCamera;

    private void Awake()
    {
        GetReferences();
    }
    private void Update()
    {
        MoveWithMouse();
        //MoveWithTilt();
    }
    private void GetReferences()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if(MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }
    private void MoveWithMouse()
    {
        if (!Input.GetMouseButton(0)) { return; } // Check if the left mouse button is pressed

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = MainCamera.transform.position.z - transform.position.z;

        Vector3 worldPosition = MainCamera.ScreenToWorldPoint(mousePosition);

        float targetX = -worldPosition.x;

        targetX = Mathf.Clamp(targetX, Max_Left, Max_Right);

        float step = Movement_Speed * Time.deltaTime;

        float newX = Mathf.MoveTowards(transform.position.x, targetX, step);

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void MoveWithTilt() // Move with device tilt TO BE IMPLEMENTED //
    {

    }
}
