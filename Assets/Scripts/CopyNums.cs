using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyNums : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject num = transform.Find("num").gameObject;
        for (int i = 2; i <= 20; i++) {
            GameObject tmp = Instantiate<GameObject>(num);
            tmp.transform.SetParent(transform);
            tmp.transform.localPosition = new Vector3(0f, i);
            tmp.GetComponent<CheckRow>().rownum = i;
        }
	}
}
