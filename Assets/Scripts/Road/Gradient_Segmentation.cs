using UnityEngine;
public class Gradient_Segmentation : MonoBehaviour
{
    public static Color[] segmentColors;    // Colors for each segment

    private static Texture2D gradientTexture;

    private void Start()
    {
        gradientTexture = CreateGradientTexture(256, 1, Color.red, Color.white);
        //DivideGradientIntoSegments(10);
        //UpdateObjectsWithSegmentedColors();
    }

    // Function to create a gradient texture
    public static Texture2D CreateGradientTexture(int width, int height, Color startColor, Color endColor)
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            Color color = Color.Lerp(startColor, endColor, (float)x / (width - 1));
            texture.SetPixel(x, 0, color);
        }

        texture.Apply();

        return texture;
    }

    public static void DivideGradientIntoSegments(int numberOfSegments)
    {
        segmentColors = new Color[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            float segmentStart = (float)i / numberOfSegments;
            float segmentEnd = (float)(i + 1) / numberOfSegments;
            segmentColors[i] = GetAverageColorInGradient(segmentStart, segmentEnd);
        }
    }
    private static Color GetAverageColorInGradient(float start, float end)
    {
        int startX = Mathf.RoundToInt(start * gradientTexture.width);
        int endX = Mathf.RoundToInt(end * gradientTexture.width);

        Color averageColor = Color.black;

        for (int x = startX; x < endX; x++)
        {
            averageColor += gradientTexture.GetPixel(x, 0);
        }

        averageColor /= (endX - startX);
        return averageColor;
    }
    public static void UpdateObjectsWithSegmentedColors(GameObject[] objectsToUpdate)
    {
        for (int i = 0; i < objectsToUpdate.Length; i++)
        {
            Renderer renderer = objectsToUpdate[i].GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = new(Shader.Find("Standard"));
                material.color = segmentColors[i];
                renderer.material = material;
            }
        }
    }
}
