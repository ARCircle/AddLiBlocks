using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour {
	
	public static void BtnStateColorChange(Button btn, Color color, int changeState){
		ColorBlock cbBtn = btn.colors;
		switch (changeState) {
		case 0:
			cbBtn.normalColor = color;
			break;
		case 1:
			cbBtn.highlightedColor = color;
			break;
		case 2:
			cbBtn.pressedColor = color;
			break;
		case 3:
			cbBtn.disabledColor = color;
			break;
		}
		btn.colors = cbBtn;
	}
}
