using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

	// Use this for initialization
	/*void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
	public GameObject CopyMyself(){
		GameObject tmp = Instantiate<GameObject> (this.gameObject);
		tmp.transform.SetParent (this.transform.parent);
		return tmp;
	}

	public void Suicide(){
		Destroy (this.gameObject);
	}
}
