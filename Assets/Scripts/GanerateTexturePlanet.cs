using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public delegate Color ColorGet(float value);

public class Biome
{
	public float value;
	public ColorGet color;
}

public class BiomeColor
{

	public static Color water = new Color();
	public static Color sand = new Color(255, 140, 0);
	public static Color grass = new Color();
	public static Color rocks = new Color();
	public static Color snow = new Color();


	public List<Biome> biomes = new List<Biome>
	{
		new Biome{value = 0.65f, color = (x) => new Color(0.0f, x, 1.0f)},
		new Biome{value = 0.7f, color = (x) => new Color(1.0f, 0.55f, 0.0f)},
		new Biome{value = 0.85f, color = (x) => new Color(0.0f, 1.0f - ((x - 0.7f) * 4), 0.0f)},
		new Biome{value = 0.95f, color = (x) => new Color(1 - x + 0.1f, 1 - x + 0.1f, 1 - x + 0.1f)},
		new Biome{value = 100.0f, color = (x) => new Color(0.9f, 0.9f, 0.9f)}
	};
	public BiomeColor()
	{

	}
	public Color GetBiomeColor(float x)
	{
		var a = biomes.Find(
			(biome) => {
				return x <= biome.value;
			});
		return a.color(x);
	}

	
}

public class GanerateTexturePlanet
{
	int width;
	int height;


	//float speed = 1.0f;
	//float radius = 2f;
	//float angle = 0f;
	//float rotationSpeed = 100f;

	float definition = 0.5f;
	float freq = 0.001f;
	float amplitude = 5.6f;
	int octavs = 4;
	float seed = 0;
	//public Texture2D noiseTexture;
	public float[,] noiseMatrix;
	//public Texture2D worldTexture;
	public Texture2D worldTemperTexture;
	public GameObject Earth;
	Material material;
	BiomeColor biomeColor;

	public GanerateTexturePlanet(int worldSize, float definition, float freq, float amplitude, int octavs)
    {
        this.definition = definition;
		this.freq = freq;
		this.amplitude = amplitude;
		this.octavs = octavs;

		//this.speed = speed;
		//this.radius = radius;
		//this.rotationSpeed = rotationSpeed;

		this.width = worldSize;
		this.height = worldSize / 2;
		biomeColor = new BiomeColor();
		
	}

	public void Apply(GameObject gameObject)
	{
		
		var material = gameObject.GetComponent<Renderer>().material;
		GenerateNoizeTexture();
		material.mainTexture = GenerateWorldTexture();

	}

	
	//void Update(GameObject gameObject)
	//{
	//	angle += speed * Time.deltaTime;

	//	float x = Mathf.Cos(angle) * radius;
	//	float z = Mathf.Sin(angle) * radius;
	//	gameObject.transform.position = new Vector3(x, 0, z);
	//	gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	//}
 //   void Start()
	//{

		
	//	//material = new Material(Shader.Find("Standard"));
	//	//material.mainTexture = worldTexture;
	//	//Earth.GetComponent<Renderer>().material.mainTexture = worldTexture;
	//	//material = gameObject.GetComponent<MeshRenderer>().material;
	//	//GoodSphere();
	//	Generate();

	//}
	//public void Generate()
	//{


	//	GenerateNoizeTexture();
	//	//GenerateWorldTexture();
	//	//GenerateworldTemperTexture(1);
	//	material.mainTexture = GenerateWorldTexture();


	//}



	private float PerlinNoiseGen(int x, int y, float amplitude, float frequency, int actava)
	{
		float v = amplitude * (Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency));
		for (int i = 1; i < actava; i++)
		{

			v += (amplitude / (i * 2)) * (Mathf.PerlinNoise((x + seed) * (frequency * (i * 2)),
				(y + seed) * (frequency * (i * 2))));

		}
		return v / actava;


		//return amplitude * (Mathf.PerlinNoise((x + seed) * (noizeFreq * frequency), (y + seed) * (noizeFreq * frequency)));
	}





	public void GenerateNoizeTexture()
	{
		noiseMatrix = new float[width, height];

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				//float v = (PerlinNoiseGen(x, y, 2.5f, 0.3f) + PerlinNoiseGen(x, y, 1.3f, 0.6f) + PerlinNoiseGen(x, y, 0.8f, 0.9f) + PerlinNoiseGen(x, y, 0.4f, 1.3f )) / 4;
				//float v = PerlinNoiseGen(x, y, noizeAmplitude, noizeFreq, noizeActavs);



				//noiseTexture.SetPixel(x, y, new Color(v, v, v));
				noiseMatrix[x, y] = PerlinNoiseGen(x, y, amplitude, freq, octavs);



			}

		}
		//noiseTexture.Apply();
	}
	public Texture2D GenerateWorldTexture()
	{

		Texture2D worldTexture = new Texture2D(width, height);
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{

				worldTexture.SetPixel(x, y, biomeColor.GetBiomeColor(noiseMatrix[x, y]));



			}

		}
		worldTexture.Apply();
		return worldTexture;


	}



}