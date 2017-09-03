using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsMove : MonoBehaviour {
    public Sprite[] image = new Sprite[4];
    public GameObject star;
    public bool copyed = false;

	// Use this for initialization
	void Start () {
        if (!this.gameObject.name.Contains("Clone")) {
            for(int i = 0; i < 16; i++) {
                if(i == 8)
                    continue;
                GameObject stars = Instantiate<GameObject>(this.gameObject);
                stars.transform.SetParent(this.transform.parent);
                stars.transform.localPosition = new Vector3(i * 0.5f - 4f, i * 0.5f - 4f);
                stars.transform.localScale = Vector3.one;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(copyed) {
            transform.localPosition -= new Vector3(0.5f, 0.5f, 0) * Time.deltaTime;
            if(transform.localPosition.y < -4) {
                transform.localPosition = new Vector3(4, 4, 0);
            }
        } else {
            copyed = true;
            int nowY = Mathf.RoundToInt(transform.position.y * 2) + 1000;
            for(int i = 0; i < 15; i++) {
                GameObject tmp = Instantiate<GameObject>(star);
                SpriteRenderer sp = tmp.GetComponent<SpriteRenderer>();
                sp.enabled = true;
                //Debug.Log("num:" + nowY % 4);
                sp.sprite = image[nowY % 4];
                tmp.transform.SetParent(this.transform);
                tmp.transform.localPosition = new Vector3(i - 7f, 0f);
                tmp.transform.localScale = Vector3.one * 0.2f;
            }
        }
    }
}
