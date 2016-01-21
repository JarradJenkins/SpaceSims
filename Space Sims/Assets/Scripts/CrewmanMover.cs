using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrewmanMover : MonoBehaviour {

	Animator anim;
	Vector3 scalechanger = new Vector3();
	Vector3 movechanger = new Vector3();
	public float minidletime;
	public float maxidletime;
	float idletime;
	bool idle = true;
	public float minmovetime;
	public float maxmovetime;
	float movetime;
	bool moving = false;
	List<string> animpara;
	float movespeed;
	float stopwatch;
	public float itchiness = 20f;
	float randompercent;
	idleaction scratch = new idleaction("Isscratching",1.43f, 1.43f,"Isscratching");


	void Start ()
	{
		anim = GetComponent<Animator> ();
		scalechanger = transform.localScale;
		movechanger = transform.localPosition;
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
				if (Random.value < 0.5)
				movespeed = 0.02f;
				else movespeed = -0.02f;
			moving = true;
			idle = false;
			idletime = minidletime + (Random.Range (0, maxidletime - minidletime));
			stopwatch = 0.0f;
			clearanimation ("Ismoving");
			}
		}

		if (stopwatch > movetime && moving) 
		{
			moving = false;
			idle = true;
			movespeed = 0.0f;
			movetime = minmovetime + (Random.Range (0, maxmovetime - minmovetime));
			stopwatch = 0.0f;
			clearanimation ();
		}

		if (moving) 
		{
			movechanger.x = movechanger.x + movespeed;
			transform.localPosition = movechanger;
		
			if (movespeed > 0)
			{
				scalechanger.x = Mathf.Abs (scalechanger.x);
				transform.localScale = scalechanger;
			}
			if (movespeed < 0) 
			{
				scalechanger.x = Mathf.Abs (scalechanger.x)*(-1);
				transform.localScale = scalechanger;
			}
		}
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


