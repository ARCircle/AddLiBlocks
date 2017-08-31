using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject pics = GameObject.Find ("Pictures");
		pics.transform.Find ("Picture" + Random.Range (1, 6)).GetComponent<SpriteRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
