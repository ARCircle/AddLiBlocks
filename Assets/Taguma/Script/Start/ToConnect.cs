using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToConnect : MonoBehaviour {
    float timer = 1f;
    bool flag = false, start = true;
    AudioSource audios;
    Animation anim;
    public GameObject SceneMoveEffect;

    void Start() {
        audios = SceneMoveEffect.GetComponent<AudioSource>();
        anim = SceneMoveEffect.GetComponent<Animation>();
    }

    void Update() {
        if (flag) {
            if (start) {
                start = false;
                audios.PlayOneShot(audios.clip);
                anim.Play();
            }
            timer -= Time.deltaTime;
            if (timer < 0f) {
                SceneManager.LoadScene("Connect");
            }
        }

    }

    public void SceneLoad () {
        flag = true;
	}

}