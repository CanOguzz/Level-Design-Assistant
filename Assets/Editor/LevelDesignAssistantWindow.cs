using UnityEngine;
using UnityEditor;

public class LevelDesignAssistantWindow : EditorWindow
{
    // Terrain generation parameters
    private int terrainWidth = 256;
    private int terrainLength = 256;
    private int terrainHeight = 50;
    private float noiseScale = 20f;
    private int seed = 0;
    private int octaves = 4;
    private float persistence = 0.5f;
    private float lacunarity = 2f;

    // Declare terrainData as a class-level variable
    private TerrainData terrainData;

    [MenuItem("Tools/Level Design Assistant")]
    public static void ShowWindow()
    {
        GetWindow<LevelDesignAssistantWindow>("Level Design Assistant");
    }

    private void OnGUI()
    {
        GUILayout.Label("Terrain Generation Settings", EditorStyles.boldLabel);

        terrainWidth = EditorGUILayout.IntField("Terrain Width", terrainWidth);
        terrainLength = EditorGUILayout.IntField("Terrain Length", terrainLength);
        terrainHeight = EditorGUILayout.IntField("Terrain Height", terrainHeight);
        noiseScale = EditorGUILayout.FloatField("Noise Scale", noiseScale);

        GUILayout.Space(10);

        seed = EditorGUILayout.IntField("Seed", seed);
        octaves = EditorGUILayout.IntField("Octaves", octaves);
        persistence = EditorGUILayout.FloatField("Persistence", persistence);
        lacunarity = EditorGUILayout.FloatField("Lacunarity", lacunarity);

        GUILayout.Space(20);

        if (GUILayout.Button("Generate Terrain"))
        {
            GenerateTerrain();
        }
    }

    private void GenerateTerrain()
    {
        // Delete existing terrain if it exists
        GameObject existingTerrain = GameObject.Find("GeneratedTerrain");
        if (existingTerrain != null)
        {
            DestroyImmediate(existingTerrain);
        }

        // Create a new terrain GameObject
        GameObject terrainObject = new GameObject("GeneratedTerrain");
        Terrain terrain = terrainObject.AddComponent<Terrain>();
        TerrainCollider terrainCollider = terrainObject.AddComponent<TerrainCollider>();

        // Initialize and configure terrainData
        terrainData = new TerrainData();
        terrainData.heightmapResolution = terrainWidth + 1;
        terrainData.size = new Vector3(terrainWidth, terrainHeight, terrainLength);

        // Save terrainData asset (required in some Unity versions)
        AssetDatabase.CreateAsset(terrainData, "Assets/GeneratedTerrainData.asset");

        // Assign terrain data
        terrain.terrainData = terrainData;
        terrainCollider.terrainData = terrainData;

        // Generate heightmap using Perlin Noise
        float[,] heights = GenerateHeights();

        // Apply heights to terrain
        terrainData.SetHeights(0, 0, heights);

        // Refresh the AssetDatabase
        AssetDatabase.SaveAssets();
    }

    private float[,] GenerateHeights()
    {
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        float[,] heights = new float[width, height];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = prng.Next(-100000, 100000);
            float offsetY = prng.Next(-100000, 100000);
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (noiseScale <= 0)
        {
            noiseScale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x - halfWidth) / noiseScale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / noiseScale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                heights[x, y] = noiseHeight;
            }
        }

        // Normalize the noise heights
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                heights[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heights[x, y]);
            }
        }

        return heights;
    }
}
    