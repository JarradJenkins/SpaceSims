using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	private float horizontalmove;
	private float verticalmove;
	private Vector3 cameraposition;
	public float movespeed = 1;
	float zoomlevel;
	public float zoomrate = 0.33f;
	
	
	void Update ()
	{
		zoomlevel = 0;
		if (Input.GetKey ("="))
			zoomlevel = 1;
		if (Input.GetKey ("-"))
			zoomlevel = -1;
		cameraposition = transform.localPosition;
		horizontalmove = Input.GetAxis ("Horizontal");
		verticalmove = Input.GetAxis ("Vertical");
		cameraposition.x = cameraposition.x + (horizontalmove*movespeed);
		cameraposition.y = cameraposition.y + (verticalmove*movespeed);
		cameraposition.z = cameraposition.z + (zoomlevel * zoomrate);
		transform.localPosition = cameraposition;
	}
}
