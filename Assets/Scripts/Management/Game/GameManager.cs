using UnityEngine;
using UnityEngine.SceneManagement;
[DefaultExecutionOrder(-100000)]
public class GameManager : MonoBehaviour
{
    [Header("Target FrameRate If All Fails")]
    [SerializeField] private static readonly int LastResortFrameRate = 120;
    public static GameManager Instance { get; private set; }
    private GameManager() { }

    private void Awake()
    {
        CheckInstance();
        StartUpOperations();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void CheckInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void StartUpOperations()
    {
        try
        {
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
            Application.targetFrameRate = LastResortFrameRate;
        }

        //Debug.Log("Frame Rate is Before Correction is: "+ Application.targetFrameRate + "");

        if (Application.targetFrameRate < 60)
        {
            Application.targetFrameRate = LastResortFrameRate;
        }

        Debug.Log("Frame Rate is Set To: " + Application.targetFrameRate + "");
    }
}
