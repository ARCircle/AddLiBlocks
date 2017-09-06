using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRow : MonoBehaviour {
    public int pnum = 0;
    public int rownum = 1;
    int countnum = 0;
    TextMesh txt;
    Color[] cols = new Color[] {
        new Color(0.2f, 1f, 0, 1),
        new Color(1f, 1f, 0, 1),
        new Color(1f, 0, 0, 1),
        new Color(1f, 0.9f, 0.9f, 1)
    };

    void Start () {
        txt = GetComponent<TextMesh>();
    }
	
	// Update is called once per frame
	void Update () {
        int cnt = 0, result = 8;
        for(int i = 1; i <= 10; i++)
            if(World.Plr[pnum].stage[i, rownum] > 0)
                cnt++;
        if(cnt > 8)
            result = 0;
        else
            result = 8 - cnt;
        if(countnum != result) {
            countnum = result;
            txt.text = "" + countnum;
            if(countnum == 0)
                txt.color = cols[3];
            else if(countnum <= 1)
                txt.color = cols[2];
            else if(countnum <= 3)
                txt.color = cols[1];
            else
                txt.color = cols[0];
        }
	}
}
