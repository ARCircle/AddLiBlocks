using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingScene : MonoBehaviour {
	/*
	int row = 0;
	int column = 0;
	public int pnum;
	string pname, pstring;
	Button b, next;
	//StandaloneInputModule eve;

	// Use this for initialization
	void Start () {
		//eve = GameObject.Find ("EventSystem").GetComponent<StandaloneInputModule>();
		int cnt = 0;
		pname = "P" + pnum;
		pstring = pnum + "P_";
		do {
			row = cnt / 5;
			column = cnt % 5;
			b = GetSelectButton ();
		} while (b.transition.ToString() != "Disabled");
		b.Select ();
		next = transform.parent.Find (pname + "Button").GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {/
		bool anglepush = Input.GetButtonDown (pstring + "RightRotate");
		float angle = Input.GetAxis (pstring + "RightRotate");
		if (anglepush) {
			if (angle > 0.1f)
				next.OnPointerClick (eve.Getpo);
		}
		bool movepush = Input.GetButtonDown (pstring + "RightMove");
		float move = Input.GetAxis (pstring + "RightMove");
		if (movepush) {
			if (move > 0.1f)    // ミノの右移動
				column = (column + 1) % 5;
			else if (move < -0.1f)   // ミノの左移動
				column = (column - 1) % 5;
			EventSystem.current.
			b = GetSelectButton ();
			b.OnPointerClick (eve);
		}
		bool updownpush = Input.GetButtonDown (pstring + "Down");
		float updown = Input.GetAxis (pstring + "Down");
		if (updownpush) {
			if (updown > 0.1f)
				row = (row + 1) % 5;
			else if (updown < -0.1f)
				row = (row - 1) % 5;
			b = GetSelectButton ();
			b.OnPointerClick (eve);
		}
	}

	Button GetSelectButton(){
		Button tmp = transform
			.Find (pname + "Line" + (row + 1))
			.Find (pname + "Button" + (row * 5 + column + 1))
			.GetComponent<Button> ();
		return tmp;
	}
	*/
}
