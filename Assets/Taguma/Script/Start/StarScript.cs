using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		transform.localPosition -= new Vector3 (0.01f, 0.01f, 0) * 60f * Time.deltaTime;
		if (transform.localPosition.y < -4) {
			transform.localPosition = new Vector3 (4, 4, 0);
		}
	}
}
