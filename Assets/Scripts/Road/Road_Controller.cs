using UnityEngine;
public class Road_Controller : MonoBehaviour
{
    private static bool Optimization_Is_Enabled = true;
    private void OnBecameInvisible()
    {
        DestroyRoadAfterNotViewedOnCamera();
    }
    private void DestroyRoadAfterNotViewedOnCamera()
    {
        try
        {
            if (Optimization_Is_Enabled)
            {
                Debug.Log(gameObject + " Became invisible to the camera");
                Destroy(gameObject, 1.0f);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }
        
    }
}
