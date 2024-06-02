using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystemManager : MonoBehaviour
{
	private float speed = 1.0f;
	public float radius = 2f;
	private float angle = 0f;
	private float rotationSpeed = 100f;
	//private GameObject gameObject;


	private void Awake()
	{
        var planetGen = new GanerateTexturePlanet(1000, 0.5f, 0.02f, 3.2f, 4);
        var a = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        a.transform.parent = transform;
		//gameObject = a;
        planetGen.Apply(a);
	}

	void Start()
    {
        
    }


	//    void Update()
	//    {
	//		angle += speed * Time.deltaTime;

	//		float x = Mathf.Cos(angle) * radius;
	//		float z = Mathf.Sin(angle) * radius;
	//		gameObject.transform.position = new Vector3(x, 0, z);
	//		gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);


	//	}
}
