using UnityEngine;
public class RoadSpawner : MonoBehaviour
{
    [Header("Resource Addresses")]
    [SerializeField] private readonly string Test_Road_Address = "Roads/Test_Road";

    [Header("Spawn Parameters")]
    [SerializeField] private int Initial_Spawn_Amount = 8;
    [SerializeField] private int Min_Spawned_Roads = 0;

    [Header("Road Prefabs")]
    [SerializeField] private GameObject Test_Road;

    [Header("Latest Road")]
    [SerializeField] private GameObject Latest_Road;
    private void Start()
    {
        GetResources();
        Startup();
        ColorUp();
    }
    private void Update()
    {
        
    }
    private void ColorUp()
    {
        GameObject[] children = GetAllChildObjects(transform);

        Texture2D texture  = Gradient_Segmentation.CreateGradientTexture(256, 1, Color.red, Color.white);
        Gradient_Segmentation.DivideGradientIntoSegments(Initial_Spawn_Amount);
        Gradient_Segmentation.UpdateObjectsWithSegmentedColors(children);
    }
    private void Startup()
    {
        for (int i = 0; i < Initial_Spawn_Amount; i++)
        {
            Vector3 spawnPosition;

            if (Latest_Road == null) // First Road
            {
                spawnPosition = Test_Road.transform.position;
            }
            else // After first road
            {
                Transform desiredCoordinates = GetRoadSocketLocation(Latest_Road);
                spawnPosition = desiredCoordinates.position;
            }

            GameObject newRoad = Instantiate(Test_Road, spawnPosition, Quaternion.identity);
            Latest_Road = newRoad; // Update Latest_Road to the newly spawned road
            Latest_Road.transform.parent = transform;
            Latest_Road.name = "Road_" + transform.childCount.ToString();
        }
    }
    private void GetResources() // Get Road Blocks from the Resource folder //
    {
        try
        {
            Test_Road = Resources.Load<GameObject>(Test_Road_Address);

            if (Test_Road == null)
            {
                Debug.LogError("Failed to load road prefab from Resources: " + Test_Road_Address);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }
    private Transform GetRoadSocketLocation(GameObject Road) // Gets the desired socket coordinates from the latest road piece to instantiate the next piece's location //
    {
        return Road.transform.GetChild(0).transform;
    }

    private GameObject[] GetAllChildObjects(Transform parent)
    {
        GameObject[] childObjects = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            childObjects[i] = parent.GetChild(i).gameObject;
        }

        return childObjects;
    }
}
