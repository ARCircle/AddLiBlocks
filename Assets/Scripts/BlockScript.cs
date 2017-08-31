using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
	public bool aura = true;
	float timer = 0f, dur = 0.7f, sign = 1f;
	SpriteRenderer colA, colB;

	// Use this for initialization
	void Start () {
		colA = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		colB = transform.GetChild (1).GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (aura) {
			timer += Time.deltaTime * sign;
			colA.color = new Color (1f, 1f, 0.8f, timer / (dur * 2f));
			colB.color = new Color (1f, 1f, 0.8f, timer / (dur * 2f));
			if (timer < 0f)
				sign = 1f;
			else if (timer > dur)
				sign = -1f;
		}
	}

	public void StopAura(){
		aura = false;
		colA.color = new Color (1f, 1f, 0.8f, 0f);
		colB.color = new Color (1f, 1f, 0.8f, 0f);
	}

	public void Suicide(){
		Destroy (this.gameObject);
	}
}
