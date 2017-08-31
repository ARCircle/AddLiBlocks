using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingSceneManager : MonoBehaviour {
	public static bool P1;
	public static bool P2;
	public Text ayr;
	// Use this for initialization
	void Start () {
		P1 = false;
		P2 = false;
		ayr = GameObject.Find ("Are You Ready?").GetComponent<Text> ();

		ayr.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if ((P1 == true) && (P2 == true)) {
			ayr.gameObject.SetActive (true);
			if (Input.GetKey (KeyCode.Return)) {
				Application.LoadLevel ("Main");
			}
		}
	}
}
