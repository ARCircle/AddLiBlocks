using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : MonoBehaviour {

	Button Title;
	//Button Option;
	//Button Exit;

	// Use this for initialization
	void Start () {
		// ボタンコンポーネントの取得
		Title   = GameObject.Find ("/Canvas/TitleButton").GetComponent<Button> ();
		//Option  = GameObject.Find ("/Canvas/OptionButton").GetComponent<Button> ();
		//Exit    = GameObject.Find ("/Canvas/ExitButton").GetComponent<Button> ();

		//最初に選択状態するボタン
		Title.Select ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
