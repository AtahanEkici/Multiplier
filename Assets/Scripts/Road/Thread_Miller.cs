using UnityEngine;

public class Thread_Miller : MonoBehaviour
{
    [Header("Stop Movement ?")]
    [SerializeField] private bool StopMovement = false;

    [Header("ThreadMill Controlls")]
    [SerializeField] private float ThreadMill_Speed = 25f;

    private void Update()
    {
        RollBackMovement();
    }
    private void RollBackMovement()
    {
        if (StopMovement) { return; }

        float deltaZ = Time.smoothDeltaTime * ThreadMill_Speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - deltaZ);
    }
    public void ChangeSpeed(float speed)
    {
        ThreadMill_Speed = speed;
    }
}
