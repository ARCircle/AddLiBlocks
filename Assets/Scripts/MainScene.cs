using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour {
	float finishtimer = 8f;
	GameObject[] winlose = new GameObject[2];
	AudioSource bgm, gareki, win;
	Animation p1, p2, fade;
	int level = 0;

	// Use this for initialization
	void Start () {
		GameObject pics = GameObject.Find ("Pictures");
		GameObject audioobj = GameObject.Find ("Audios");
		winlose [0] = transform.Find ("1P_text").gameObject;
		winlose [1] = transform.Find ("2P_text").gameObject;
		pics.transform.Find ("Picture" + Random.Range (1, 6)).GetComponent<SpriteRenderer>().enabled = true;
		bgm = audioobj.transform.Find ("battle").GetComponent<AudioSource> ();
		gareki = audioobj.transform.Find ("gareki_edit").GetComponent<AudioSource> ();
		win = audioobj.transform.Find ("win").GetComponent<AudioSource> ();
		p1 = transform.Find ("1P_text").GetComponent<Animation> ();
		p2 = transform.Find ("2P_text").GetComponent<Animation> ();
		fade = transform.Find ("FadeBlack").GetComponent<Animation> ();
		bgm.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (World.gameover >= 0) {
			if (bgm.isPlaying) {
				bgm.Stop ();
				gareki.PlayOneShot(gareki.clip);
			}
			finishtimer -= Time.deltaTime;
			switch (level) {
			case 0:
				if (finishtimer < 6.5f) {
					if (World.gameover == 0) {
						p2.GetComponent<Text>().text = "Win";
						p1.GetComponent<Text>().text = "Lose";
						p1.Play ("White");
						p2.Play ("Rainbow");
					} else {
						p1.GetComponent<Text>().text = "Win";
						p2.GetComponent<Text>().text = "Lose";
						p1.Play ("Rainbow");
						p2.Play ("White");
					}
					p2.GetComponent<Text> ().enabled = true;
					p1.GetComponent<Text> ().enabled = true;
					win.PlayOneShot (win.clip);
					level++;
				}
				break;
			case 1:
				if (finishtimer < 2f) {
					fade.Play ();
					level++;
				}
				break;
			}
			if (finishtimer < 0f) {
				SceneManager.LoadScene ("Start");
			}
		}
	}
}
