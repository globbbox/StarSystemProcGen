using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;


public class StarSystem
{
	public GameObject sun { get; set; }
	public GameObject[] planets { get; set; }
}

public class StarSystemManager : MonoBehaviour
{
	

	private float speed = 1.0f;
	public float radius = 2f;
	private float angle = 0f;
	private float rotationSpeed = 100f;

	private StarSystem system = null;
	//private GameObject gameObject;


	private void Awake()
	{
		system = new StarSystem();
		system.sun = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		system.sun.transform.position = Vector3.zero;
		system.sun.transform.localScale = new Vector3(5, 5, 5);
		system.sun.GetComponent<Renderer>().material.color = Color.yellow;



		var planetGen = new GanerateTexturePlanet(1000, 0.5f, 0.02f, 3.2f, 4);

        var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        a.transform.parent = transform;
		a.AddComponent<Planet>();
		var comp = a.GetComponent<Planet>();
		comp.xPos = system.sun.transform.position.x;
		comp.zPos = system.sun.transform.position.z;
		comp.radius = system.sun.transform.localScale.x + comp.transform.localScale.x / 2 + 2;
		Debug.Log(comp.radius);
        planetGen.Apply(a);
		
	}

	void Start()
    {
        
    }


	
}
