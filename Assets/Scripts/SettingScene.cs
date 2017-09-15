using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour {

    int row = 0;
    int column = 0;
    public int pnum;
    string pname, pstring;
    Button b;
    Color HL = new Color(0.9f, 1f, 0f);
    int[,] ajust = new int[,] { { 1, 0 }, { 0, 1 }, { 0, 8 }, { 1, 1 }, { 1, 0 }, { 1, 0 }, { 1, 0 } };
    AudioSource focus;

    // Use this for initialization
    void Start() {
        pname = "P" + pnum;
        pstring = pnum + "P_";
        focus = transform.parent.Find("BackGround 1").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if(b != null) {
            int cnt = 0;
            bool anglepush = Input.GetButtonDown(pstring + "RightRotate");
            float angle = Input.GetAxis(pstring + "RightRotate");
            bool movepush = Input.GetButtonDown(pstring + "RightMove");
            float move = Input.GetAxis(pstring + "RightMove");
            bool updownpush = Input.GetButtonDown(pstring + "Down");
            float updown = Input.GetAxis(pstring + "Down");

            do {
                if(anglepush) {
                    if(angle > 0.1f && b.interactable)
                        b.GetComponent<SettingButtonScript>().Setting();
                    else if(angle < -0.1f && b.interactable)
                        b.GetComponent<SettingButtonScript>().Cancel();
                }
                if(movepush) {
                    if(move > 0.1f) {
                        column = (column + ajust[cnt, 0]) % 5;
                        row = (row + ajust[cnt, 1]) % 5;
                    } else if(move < -0.1f) {
                        column = (column + ajust[cnt, 0] * 4) % 5;
                        row = (row + ajust[cnt, 1] * 4) % 5;
                    }
                    b = GetSelectButton();
                }
                if(updownpush) {
                    if(updown > 0.1f) {
                        row = (row + ajust[cnt, 0]) % 5;
                        column = (column + ajust[cnt, 1]) % 5;
                    } else if(updown < -0.1f) {
                        row = (row + ajust[cnt, 0] * 4) % 5;
                        column = (column + ajust[cnt, 1] * 4) % 5;
                    }
                    b = GetSelectButton();
                }
                cnt = (cnt + 1) % 7;
            } while(!b.interactable);

            if (movepush || updownpush) {
                focus.PlayOneShot(focus.clip);
            }
        }
    }


    public void SetFirstButton() {
        int cnt = 0;
        do {
            row = cnt / 5;
            column = cnt % 5;
            b = GetSelectButton();
            cnt++;
        } while(!b.interactable);
    }

    Button GetSelectButton() {
        if(b != null)
            ColorManager.BtnStateColorChange(b, Color.white, 0);
        Button tmp = transform
            .Find(pname + "Line" + (row + 1))
            .Find(pname + "Button" + (row * 5 + column + 1))
            .GetComponent<Button>();
        ColorManager.BtnStateColorChange(tmp, HL, 0);
        return tmp;
    }

    public void SelectEnd() {
        b = null;
    }
}
