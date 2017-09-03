using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingButtonScript : MonoBehaviour {

	public SettingScript script;
	public PlayerButtonScript NextButton;
	public int x;
	public int y;
	private int[,] minocells;

	public void Setting(){
		minocells = new int[,]{{0, 0, 0, 0, 0} , {0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} ,{0, 0, 0, 0, 0} };
		minocells [x, y] = 5;
		/*for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				Debug.Log (i + "," + j + ":" + minocells [i, j]);
			}
		}*/
		script.minocellsInput = minocells;

		NextButton.playerButton.interactable = true;

	}
}
