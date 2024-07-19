using System.Collections.Generic;
using UnityEngine;
public class Character_Controller : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField] private List<GameObject> Characters = new();

    [Header("Controller Limits")]
    [SerializeField] private float Max_Left = -6.75f;
    [SerializeField] private float Max_Right = 6.75f;

    [Header("Movement Parameters")]
    [SerializeField] private float Movement_Speed = 7f;
    [SerializeField] private Vector3 targetPosition;

    [Header("Screen Configration")]
    [SerializeField] private float Screen_Width;
    [SerializeField] private float Screen_Width_Half;

    [Header("Local Components")]
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        GetReferences();
    }
    private void Start()
    {
        StartUp();
    }
    private void Update()
    {
        MoveWithMouse();
    }
    private void MoveWithTilt()
    {

    }
    private void GetReferences()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }
    private void StartUp()
    {
        Screen_Width = Screen.width;
        Screen_Width_Half = Screen_Width / 2f;
    }
    private void MoveWithMouse()
    {
        float Mouse_Position_X = Input.mousePosition.x;

        if (Input.GetMouseButton(0))
        {
            Debug.Log("Geçti");

            if (Mouse_Position_X < Screen_Width_Half) // Go Left //
            {
                targetPosition = new Vector3(Max_Left, transform.position.y, transform.position.z);
            }
            else // Go Right //
            {
                targetPosition = new Vector3(Max_Right, transform.position.y, transform.position.z);
            }
            transform.position = Vector3.MoveTowards(rb.position,targetPosition,Time.smoothDeltaTime * Movement_Speed);
        }
    }
}
