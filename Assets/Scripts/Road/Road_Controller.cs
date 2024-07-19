using UnityEngine;
public class Road_Controller : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Debug.Log(gameObject + " Became invisible to the camera");
        Destroy(gameObject,1.0f);
    }
}
