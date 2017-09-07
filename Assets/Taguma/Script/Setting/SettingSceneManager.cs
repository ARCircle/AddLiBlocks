using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingSceneManager : MonoBehaviour {
	public bool P1;
	public bool P2;
    public GameObject SceneMoveEffect;
    public GameObject[] hideobj;
	public Text ayr;

    bool animed = false, next = false;
	float timer = 4f, starttimer = 0.3f;

	AudioSource audios;
    Animation anim, anim1, anim2;
    Animator anitor;
    // Use this for initialization
    void Start () {
		P1 = false;
		P2 = false;
		ayr = GameObject.Find ("Are You Ready?").GetComponent<Text> ();
		audios = SceneMoveEffect.GetComponent<AudioSource> ();
		anim = SceneMoveEffect.GetComponent<Animation> ();
        anim1 = GameObject.Find("P1SelectedDisplay").GetComponent<Animation>();
        anim2 = GameObject.Find("P2SelectedDisplay").GetComponent<Animation>();
        anitor = GameObject.Find("PressEnter").GetComponent<Animator>();
        ayr.gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
        if (starttimer > 0f) {
            starttimer -= Time.deltaTime;
            if(starttimer <= 0f) {
                SceneMoveEffect.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            }
        }
		if (P1 && P2 && !next) {
            if(!animed) {
                animed = true;
                ayr.gameObject.SetActive(true);
                for(int i = 0; i < hideobj.Length; i++) {
                    hideobj[i].SetActive(false);
                }
                anim1.Play();
                anim2.Play();
            }
            if (Input.GetButtonDown("Submit")) {
				next = true;
                anitor.SetTrigger("Submit");
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
