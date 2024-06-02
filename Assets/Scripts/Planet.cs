using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private float speed = 1.0f;
	public float radius = 2f;
	private float angle = 0f;
	private float rotationSpeed = 100f;

	public float xPos = 0f;
	public float zPos = 0f;

	void Start()
    {
        
    }

    
    void Update()
    {
		angle += speed * Time.deltaTime;

		float x = Mathf.Cos(angle) * radius;
		float z = Mathf.Sin(angle) * radius;
		gameObject.transform.position = new Vector3(x + xPos, 0, z + zPos);
		gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);


	}
}
