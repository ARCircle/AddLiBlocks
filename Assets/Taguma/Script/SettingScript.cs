using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour {
	public int[,] minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
	public int minonum;
	public int playernum;
	public PlayerButtonScript script;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
			switch (minonum) {
			case 0:
				minocells [1, 3] = 1;
				minocells [2, 3] = 2;
				minocells [3, 3] = 3;
				minocells [4, 3] = 4;
				Debug.Log (minocells);
			if (script.setting) {
					World.SettingMino (playernum, minonum, minocells);
				    minonum++;
					minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
				}
				break;
			case 1:
				minocells [2, 2] = 1;
				minocells [3, 2] = 2;
				minocells [3, 3] = 3;
				minocells [3, 4] = 4;
				Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			case 2:
				minocells [2, 4] = 1;
				minocells [3, 2] = 2;
				minocells [3, 3] = 3;
				minocells [3, 4] = 4;
				Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			case 3:
				minocells [2, 2] = 1;
				minocells [2, 3] = 2;
				minocells [3, 2] = 3;
				minocells [3, 3] = 4;
				Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			case 4:
				minocells [2, 3] = 1;
				minocells [2, 4] = 2;
				minocells [3, 2] = 3;
				minocells [3, 3] = 4;
				Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			case 5:
				minocells [2, 3] = 1;
				minocells [3, 2] = 2;
				minocells [3, 3] = 3;
				minocells [3, 4] = 4;
			Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			case 6:
				minocells [2, 2] = 1;
				minocells [2, 3] = 2;
				minocells [3, 3] = 3;
				minocells [3, 4] = 4;
				Debug.Log (minocells);
			if (script.setting) {
				World.SettingMino (playernum, minonum, minocells);
				minonum++;
				minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
			}
			break;
			}
	}
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