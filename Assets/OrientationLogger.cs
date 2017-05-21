﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class OrientationLogger : MonoBehaviour {

	// Used to save the logged data.
	public static Dictionary<float,float> data;
	public static string dataType;

	public float logInterval = 0.5f;
	[Range(1.0f, 360.0f)]
	public float testOrientation = 180f;

	private bool isLogging = false;
	private float lastLogTimeNormalized = 0;

	public void StartLogging()
	{
		dataType = "non_discrete";
		data = new Dictionary<float,float>();
		isLogging = true;
		Input.gyro.enabled = true;
		lastLogTimeNormalized = 0f;
		LogData();
	}

	public void StopLogging()
	{
		isLogging = false;
	}

	static public float CurrentOrientation()
	{	
		float xOrientation = 0;
		//if(!SystemInfo.supportsGyroscope)
        //{
            float roll = Mathf.Atan2(Input.acceleration.y, Input.acceleration.z) * 180 / Mathf.PI;
            //TODO: Properly decorate to produce equivalent results compared to Gyro
            xOrientation = 90 - Mathf.Abs(Mathf.Abs(roll) - 90);
        //}
		//else
        //{
        //    xOrientation = Input.gyro.attitude.eulerAngles.x;
        //}

		return xOrientation;
	}
	
	private void LogData()
	{
		float xOrientation = CurrentOrientation();
		Debug.Log(lastLogTimeNormalized + ": " + xOrientation);
		data.Add(lastLogTimeNormalized, xOrientation);
	}

	// Update is called once per frame
	public void Update () {
		if (isLogging && Time.time - lastLogTimeNormalized >= logInterval)
		{
			lastLogTimeNormalized += logInterval;
			LogData();
		}
	}
}
