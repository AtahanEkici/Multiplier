using UnityEngine;
public class RoadSpawner : MonoBehaviour
{
    [Header("Resource Addresses")]
    [SerializeField] private readonly string Test_Road_Address = "Roads/Road_Test";

    [Header("Spawn Parameters")]
    [SerializeField] private int Initial_Spawn_Amount = 10;
    [SerializeField] private int Currently_Awailable_Road_Count = 0;

    [Header("Spawn Threshold")]
    [SerializeField] private bool Spawn_After_Minimum_Threshold = true;
    [SerializeField] private int Spawn_After_Minimum_Count = 5;
    [SerializeField] private int How_Much_To_Spawn = 5;

    [Header("Road Prefabs")]
    [SerializeField] private GameObject Test_Road;

    [Header("Latest Road")]
    [SerializeField] private GameObject Latest_Road;
    private void Start()
    {
        GetResources();
        SpawnRoad(Initial_Spawn_Amount);
        //ColorUp();
    }
    private void LateUpdate()
    {
        GetCurrentRoadCount();
        SpawnInRealTime();
    }
    /*
    private void ColorUp()
    {
        GameObject[] children = GetAllChildObjects(transform);
        Texture2D texture = Gradient_Segmentation.instance.CreateGradientTexture(256, 1, Color.red, Color.white);
        Gradient_Segmentation.instance.DivideGradientIntoSegments(Initial_Spawn_Amount, texture);
        Gradient_Segmentation.instance.UpdateObjectsWithSegmentedColors(children);
    }
    */
    private void GetCurrentRoadCount()
    {
        Currently_Awailable_Road_Count = transform.childCount;
    }
    private void SpawnInRealTime()
    {
        if (Spawn_After_Minimum_Threshold == false) { return; }

        if (Currently_Awailable_Road_Count < Spawn_After_Minimum_Count)
        {
            SpawnRoad(How_Much_To_Spawn+1);
        }
    }
    private void SpawnRoad(int SpawnAmount)
    {
        for (int i = 0; i < SpawnAmount; i++)
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

            if (i % 2 != 0)
            {
                newRoad.GetComponent<Renderer>().materials[0].color = Color.white;
            }

            Latest_Road = newRoad; // Update Latest_Road to the newly spawned road
            Latest_Road.transform.parent = transform;
            Latest_Road.name = "Road_" + (transform.childCount+1).ToString();
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
    /*
    private GameObject[] GetAllChildObjects(Transform parent)
    {
        GameObject[] childObjects = new GameObject[parent.childCount];

        for (int i = 0; i < parent.childCount; i++)
        {
            childObjects[i] = parent.GetChild(i).gameObject;
        }

        return childObjects;
    }
    */
}
