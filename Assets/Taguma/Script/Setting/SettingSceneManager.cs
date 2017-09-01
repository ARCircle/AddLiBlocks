using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneManager : MonoBehaviour {
	public static bool P1;
	public static bool P2;
	public GameObject SceneMoveEffect;
	public Text ayr;

	bool next = false;
	float timer = 4f;

	AudioSource audios;
	Animation anim;
	// Use this for initialization
	void Start () {
		P1 = false;
		P2 = false;
		ayr = GameObject.Find ("Are You Ready?").GetComponent<Text> ();
		audios = SceneMoveEffect.GetComponent<AudioSource> ();
		anim = SceneMoveEffect.GetComponent<Animation> ();
		ayr.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (P1 && P2 && !next) {
			ayr.gameObject.SetActive (true);
			if (Input.GetKey (KeyCode.Return)) {
				next = true;
				anim.Play ();
				audios.PlayOneShot (audios.clip);
				//Application.LoadLevel ("Main");
				//audios.PlayOneShot(audios.clip);
			}
		} else if (next) {
			timer -= Time.deltaTime;
			if (timer < 0f) {
				SceneManager.LoadScene ("Main");				
			}
		}
	}
}
