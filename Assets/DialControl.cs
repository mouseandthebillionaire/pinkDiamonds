using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class DialControl : MonoBehaviour
{
    private Vector3       oldMouse;
	private Quaternion    oldRotation;
	private float         screenWidth;
    public  LibPdInstance pdPatch;
    public  string        pdControl;

    private Transform pt;


	private void Start()
	{
		pt = this.transform.parent;
		screenWidth = Screen.width;
		
		// Random Rotation
		float z = Random.rotation.z;
		Debug.Log(z);
		pt.rotation = new Quaternion(pt.rotation.x, pt.rotation.y, z, pt.rotation.w);
		
		// Audio Control
		pdPatch = GameObject.Find("AudioControl").GetComponent<LibPdInstance>();
		// Apply Initial settings based on starting position
		ApplyEffect();
	}

 
	public void OnMouseDown()
	{
		oldMouse = Input.mousePosition;
		oldRotation = pt.transform.rotation;

	}
 
	public void OnMouseDrag()
	{
		float rotationAmount = -((Input.mousePosition - oldMouse).x + (Input.mousePosition - oldMouse).y);
		pt.rotation = oldRotation * Quaternion.Euler(Vector3.forward * (rotationAmount / screenWidth) * 360);
		
		ApplyEffect();

	}

    private void ApplyEffect()
    {
		float effectAmount = Mathf.Atan2(pt.up.x, pt.up.y);
		effectAmount  += Mathf.PI;
		effectAmount  /= 2f * Mathf.PI;
		Debug.Log(effectAmount);
		pdPatch.SendFloat(pdControl, effectAmount);
    }
}
