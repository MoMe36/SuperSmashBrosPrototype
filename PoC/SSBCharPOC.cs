using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SSBCharPOC : MonoBehaviour {

	// public float dist = 1f; 
	// public Vector3 SensorOffset; 
	public float SensorHeightRatioOffset; 
	public enum CharState {normal, blocked}; 
	public CharState char_state = CharState.normal; 


	float height; 
	Rigidbody rb; 

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>(); 
		height = GetComponent<Collider>().bounds.size.y/2f; 

		
	}
	
	// Update is called once per frame
	void Update () {



		PhysicsUpdate(); 
		
		
	}

	void EnterNormal()
	{
		char_state = CharState.normal; 
		rb.useGravity = true; 
	}

	void EnterBlocked()
	{
		char_state = CharState.blocked; 
		rb.velocity = Vector3.ProjectOnPlane(rb.velocity, Vector3.up); 
		rb.useGravity = false; 
	}

	void PhysicsUpdate()
	{
		bool contact = RaycastTest(); 
		if(char_state == CharState.normal)
		{
			if(contact)
				EnterBlocked(); 

		}
		else if(char_state == CharState.blocked)
		{
			if(!contact)
				EnterNormal(); 

		}
		
	}

	bool RaycastTest()
	{

		Vector3 offset = transform.position + Vector3.down*height*SensorHeightRatioOffset; 
		float distance = (1f - SensorHeightRatioOffset)*height*1.05f;
		Ray ray = new Ray(offset, Vector3.down); 
		RaycastHit hit; 


		Debug.DrawRay(ray.origin, ray.direction*distance, Color.red, Time.deltaTime*2); 

		if(Physics.Raycast(ray, out hit, distance))
		{
			return true;  
		}
		else
		{
			return false; 
		}
	}
}
