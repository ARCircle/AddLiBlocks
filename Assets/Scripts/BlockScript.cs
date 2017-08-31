using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
	public bool aura = true;
	int pnum = -2;
	float timer = 0f, dur = 0.7f, sign = 1f;
	float deadtimer = 0f;
	int deadlevel = 0;
	SpriteRenderer colA, colB;
	Renderer MyRend;

	// Use this for initialization
	void Start () {
		colA = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		colB = transform.GetChild (1).GetComponent<SpriteRenderer> ();
		MyRend = GetComponent<Renderer> ();
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
		if (World.gameover == pnum) {
			switch (deadlevel) {
			case 0:
				float posy = transform.localPosition.y;
				deadtimer = 0.9f / 20 * posy + 0.2f;
				deadlevel++;
				break;
			case 1:
				deadtimer -= Time.deltaTime;
				if (deadtimer < 0f) {
					MyRend.material.color = Color.gray;
					deadlevel++;
				} 
				break;
			}
		}
	}
	public void SetPnum(int num){
		pnum = num;
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
