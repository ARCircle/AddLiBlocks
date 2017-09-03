using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingText : MonoBehaviour {
	public Text text;

	public void Input(){
		text.text = "Please choice \nenemy's add-Block";
	}

	public void Wait(){
		text.text = "Please wait";
	}
}
