using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewmanMover : MonoBehaviour {

	NavMeshAgent agent;
	Animator anim;
	Vector3 scalechanger = new Vector3();
	Quaternion rotation = new Quaternion();
	public float minidletime;
	public float maxidletime;
	float idletime;
	bool idle = true;
	public float minmovetime;
	public float maxmovetime;
	float movetime;
	bool moving = false;
	List<string> animpara;
	float stopwatch;
	public float itchiness = 20f;
	float randompercent;
	idleaction scratch = new idleaction("Isscratching",1.43f, 1.43f,"Isscratching");


	void Start ()
	{
		rotation.x = 0f;
		rotation.y = 0f;
		rotation.z = 0f;
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		scalechanger = transform.localScale;
		animpara = new List<string> ();
		animpara.Add ("Ismoving");
		animpara.Add ("Isstretching");
		animpara.Add ("Isscratching");
		idletime = Random.Range (minidletime,maxidletime);
		movetime = Random.Range (minidletime,maxidletime);

	}
	void Update ()
	{
		itchiness = itchiness + (Random.Range (0f, 0.1f));
		stopwatch = stopwatch + Time.deltaTime;
		if (stopwatch > idletime && idle) 
		{
			randompercent = Random.value;
			if (randompercent < 0.33f)
			{
				stopwatch = 0f;
				idletime = 3.0f;
				clearanimation ("Isstretching");
			}

			if (randompercent > 0.33f && randompercent < 0.66f && itchiness > 50)
			{
				stopwatch = 0f;
				idletime = scratch.maxtime;
				clearanimation (scratch.animbool);
				itchiness = 0f;
			}
			if (randompercent > 0.66f) 
			{
			moving = true;
			idle = false;
			idletime = Random.Range (minidletime, maxidletime);
			stopwatch = 0.0f;
			clearanimation ("Ismoving");
			Vector3 destination = new Vector3 ();
			destination = transform.localPosition;
			destination.x = destination.x + Random.Range (-3f, 3f);
			destination.z = destination.z + Random.Range (-3f, 3f);
			agent.SetDestination (destination);
			agent.Resume ();
			}
		}

		if (stopwatch > movetime && moving) 
		{
			moving = false;
			agent.Stop ();
			idle = true;
			movetime = Random.Range (minmovetime, maxmovetime);
			stopwatch = 0.0f;
			clearanimation ();
		}
			

		if (moving) 
		{
			
			if (agent.velocity.x > 0)
			{
				scalechanger.x = Mathf.Abs (scalechanger.x);
				transform.localScale = scalechanger;
			}
			if (agent.velocity.x < 0) 
			{
				scalechanger.x = Mathf.Abs (scalechanger.x)*(-1);
				transform.localScale = scalechanger;
			}
		}
		transform.localRotation = rotation;
	}
	

	/// <summary>
	/// Sets animator bool parameters to false, except the value specified by a string.
	/// </summary>
	/// <param name="exceptstring">Exceptstring.</param>
	void clearanimation(string exceptstring)
		{
		for (int i=0; i < animpara.Count; i++) 
			{
			anim.SetBool (animpara[i], false);
			}
		anim.SetBool (exceptstring, true);
		}

	/// <summary>
	/// Sets animator bool parameters to false.
	/// </summary>
	void clearanimation()
	{
		for (int i=0; i < animpara.Count; i++) 
		{
			anim.SetBool (animpara[i], false);
		}
	}


	/// <summary>
	/// Template Class for all Idle Actions to be built into a list for
	/// random generation of contextual idle actions.
	/// </summary>
	class idleaction 
	{
		public idleaction(string id, float mint, float maxt, string animhandle){
			actionid = id;
			mintime = mint;
			maxtime = maxt;
			animbool = animhandle;
		}
		public string actionid;
		public float mintime;
		public float maxtime;
		public string animbool;
	}




	}   ///end of function///


