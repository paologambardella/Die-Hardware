
using System;
using UnityEngine;
public class PasrTimer
{
	// The PASR timer is a class designed to count from 0 to 1 and back again.
	// P = Pause = the amount of time before the timer starts to tick.
	// A = Attack = the amount of time the timer takes to tick from 0 to 1
	// S = Sustain = the amount of time the timer holds at 1
	// R = Release = the amount of time the timer takes to tick from 1 back to 0

	// As a potential improvement I might add a "soft" reset which takes into account the current stage timer when resetting... (to ensure you don't see hard animation breaks)


	int stage; // pause=0 attack=1 sustain=2 release=3 finish=4
	float[] stageTime;
	float timer;

	float value;

	bool queriedSustain = false;
	bool queriedRelease = false;
	bool queriedFinished = false;

	public PasrTimer ( float pause, float attack, float sustain, float release )
	{
		stageTime = new float[4];

		stageTime[0] = pause;
		stageTime[1] = attack;
		stageTime[2] = sustain;
		stageTime[3] = release;

		stage = 4; // do not start.

		queriedFinished = true;
	}

	public float Tick( float deltaTime )
	{
		if (stage == 4)
		{
			value = 0;
			return (0);
		}

		while ( stage < 4 && (timer >= 1 || stageTime[stage] == 0) )
		{
			//Debug.Log ("Stage:"+stage+" completed");
			timer = 0;
			stage++;
		}


		if (stage < 4)
		{
			timer += deltaTime / stageTime[stage];
			if (timer > 1)
				timer = 1;
		}

		if (stage == 0)
			value = 0;
		else if (stage == 1)
			value = timer;
		else if (stage == 2)
			value = 1;
		else if (stage == 3)
			value = 1 - timer;
		else 
			value = 0;

		//Debug.Log("Stage:"+stage+" timer:"+timer+" value:"+value);

		return value;
	}

	public float GetValue()
	{
		return value;
	}



	// only returns true "once"! The first time this is queried after the attack phase has completed
	public bool reachedSustain()
	{
		if (queriedSustain) // player has already queried.
			return false;

		if (stage >= 2)
		{
			queriedSustain = true;
			return true;
		}

		return false;
	}


	public bool reachedRelease()
	{
		if (queriedRelease) // player has already queried.
			return false;
		
		if (stage >= 3)
		{
			queriedRelease = true;
			return true;
		}
		
		return false;
	}


	public bool isFinished()
	{
		return (stage == 4);
	}

	public bool reachedFinished()
	{
		if (stage == 4 && !queriedFinished)
		{
			queriedFinished = true;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void Reset()
	{
		stage = 0;
		timer = 0;
		value = 0;
		queriedSustain = false;
		queriedFinished = false;
	}

	public void Stop()
	{
		stage = 4;
	}

	public int GetStage()
	{
		return stage;
	}
	
	public void SetStage( int stage )
	{
		this.stage = stage;
		timer = 0;
	}

	public float TotalTime()
	{
		return stageTime[0] + stageTime[1] + stageTime[2] +stageTime[3];
	}

	/*
	public void SoftReset() // based on the current "value"... restart the PASR timer gently (to preserve position) 
	{
		// this could get quite tricky as it's a lot like running two timers at once...

	}
	*/

}


