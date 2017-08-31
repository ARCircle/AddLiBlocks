using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingText : MonoBehaviour {
	public Text text;

	public static void Input(){
		text.text = "Please choice add Block";
	}

	public static void Wait(){
		text.text = "Please wait";
	}
}
