using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmdline : MonoBehaviour {

	public string args;
	public string command;
	public bool start = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			System.Diagnostics.Process.Start (command, args);
			start = false;
		}
	}
}
