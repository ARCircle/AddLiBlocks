using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonText : MonoBehaviour {

	public Text ButtonText;

	public void NextText(){
		ButtonText.text = "Next";
	}

	public void OKText(){
		ButtonText.text = "OK!";
	}
}
