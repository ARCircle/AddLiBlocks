using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButtonScript : MonoBehaviour {

	public SettingScript script;
	public int x;
	public int y;

	public void Setting(){
		script.minocells [x, y] = 5;
	}
}
