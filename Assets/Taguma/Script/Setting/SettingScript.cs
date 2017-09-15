using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour {
	public int[,] minocells = new int[5, 5];
	public int[,] minocellsDefault = new int[5, 5];
	public int[,] minocellsInput = new int[5, 5];
	public int minonum;
	public int playernum;
	public SettingText st;
    public SettingSceneManager manager;
    SettingScene scene;
    AudioSource enter, back;
	string xLine;
	string yLine;
    bool preset = true, setting = false, cancel = false;

	public GameObject obj1;
	public GameObject obj2;
	public GameObject obj3;
	public GameObject obj4;
	public GameObject obj5;
	public GameObject obj6;
	public GameObject obj7;
	public GameObject obj8;
	public GameObject obj9;
	public GameObject obj10;
	public GameObject obj11;
	public GameObject obj12;
	public GameObject obj13;
	public GameObject obj14;
	public GameObject obj15;
	public GameObject obj16;
	public GameObject obj17;
	public GameObject obj18;
	public GameObject obj19;
	public GameObject obj20;
	public GameObject obj21;
	public GameObject obj22;
	public GameObject obj23;
	public GameObject obj24;
	public GameObject obj25;

    /*Button B1; Button B2; Button B3; Button B4; Button B5;
	Button B6; Button B7; Button B8; Button B9; Button B10;
	Button B11; Button B12; Button B13; Button B14; Button B15;
	Button B16; Button B17; Button B18; Button B19; Button B20;
	Button B21; Button B22; Button B23; Button B24; Button B25;*/
    Button[] B = new Button[26];
    GameObject disp;

	// Use this for initialization
	void Start () {
        enter = transform.parent.GetComponent<AudioSource>();
        back = transform.parent.Find("ClickBlocker").GetComponent<AudioSource>();
        disp = transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").gameObject;
        World.setPlayerInfo ();
        scene = this.GetComponent<SettingScene>();
		st.Input ();
		B[1] = obj1.GetComponent<Button> ();
		B[2] = obj2.GetComponent<Button> ();
		B[3] = obj3.GetComponent<Button> ();
		B[4] = obj4.GetComponent<Button> ();
		B[5] = obj5.GetComponent<Button> ();
		B[6] = obj6.GetComponent<Button> ();
		B[7] = obj7.GetComponent<Button> ();
		B[8] = obj8.GetComponent<Button> ();
		B[9] = obj9.GetComponent<Button> ();
		B[10] = obj10.GetComponent<Button> ();
		B[11] = obj11.GetComponent<Button> ();
		B[12] = obj12.GetComponent<Button> ();
		B[13] = obj13.GetComponent<Button> ();
		B[14] = obj14.GetComponent<Button> ();
		B[15] = obj15.GetComponent<Button> ();
		B[16] = obj16.GetComponent<Button> ();
		B[17] = obj17.GetComponent<Button> ();
		B[18] = obj18.GetComponent<Button> ();
		B[19] = obj19.GetComponent<Button> ();
		B[20] = obj20.GetComponent<Button> ();
		B[21] = obj21.GetComponent<Button> ();
		B[22] = obj22.GetComponent<Button> ();
		B[23] = obj23.GetComponent<Button> ();
		B[24] = obj24.GetComponent<Button> ();
		B[25] = obj25.GetComponent<Button> ();

        manager.P1 = false;
        manager.P2 = false;
    }
	
	// Update is called once per frame
	void Update () {
		switch (minonum) {
		case 0:
            if(preset) {
                preset = false;
                MakePreset(8, 13, 18, 23, Color.cyan, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
			}
            if(cancel) {
                cancel = false;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
			break;
		case 1:
            if(preset) {
                preset = false;
                MakePreset(7, 12, 13, 14, Color.blue, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		case 2:
            if(preset) {
                preset = false;
                MakePreset(9, 12, 13, 14, Color.magenta, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		case 3:
            if(preset) {
                preset = false;
                MakePreset(7, 8, 12, 13, Color.yellow, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		case 4:
            if(preset) {
                preset = false;
                MakePreset(8, 9, 12, 13, Color.green, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		case 5:
            if(preset) {
                preset = false;
                MakePreset(8, 12, 13, 14, Color.red, false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                preset = true;
                setting = false;
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		case 6:
            if(preset) {
                preset = false;
                MakePreset(7, 8, 13, 14, new Color(1f, 0.5f, 0f), false);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

                MakePreset(0, 0, 0, 0, Color.black, true);

                preset = true;
                setting = false;
                scene.SelectEnd();

                if(playernum == 1) {
					manager.P2 = true;
				} else if (playernum == 2) {
					manager.P1 = true;
				}
				st.Wait ();
            }
            if(cancel) {
                preset = true;
                cancel = false;
                minonum--;
                disp.transform.GetChild(minonum).GetComponent<DesideBlock>().AllGray();
            }
            break;
		}
	}

    public void PlayEnter() {
    }

    void MakePreset(int b1, int b2, int b3, int b4, Color col, bool final) {
        for(int i = 0; i < 5; i++) {        // リセット
            for(int j = 0; j < 5; j++) {
                minocellsDefault[i, j] = 0;
                B[5*i + j+1].interactable = false;
                ColorManager.BtnStateColorChange(B[5*i + j+1], Color.gray, 3);
            }
        }
        if(!final) {
            trueIntera(b1, 1);
            trueIntera(b2, 2);
            trueIntera(b3, 3);
            trueIntera(b4, 4);
            B[b1].interactable = false;
            B[b2].interactable = false;
            B[b3].interactable = false;
            B[b4].interactable = false;
            ColorManager.BtnStateColorChange(B[b1], col, 3);
            ColorManager.BtnStateColorChange(B[b2], col, 3);
            ColorManager.BtnStateColorChange(B[b3], col, 3);
            ColorManager.BtnStateColorChange(B[b4], col, 3);
        }

    }

    void trueIntera(int num, int order) {
        int i = (num - 1) / 5;
        int j = (num - 1) % 5;
        minocellsDefault[i, j] = order;
        if(j - 1 >= 0)
            B[5*i + j].interactable = true;
        if(j + 1 < 5)
            B[5*i + j+2].interactable = true;
        if(i - 1 >= 0)
            B[5*(i-1) + j+1].interactable = true;
        if(i + 1 < 5)
            B[5*(i+1) + j+1].interactable = true;
    }

    public void trueSetting() {
        enter.PlayOneShot(enter.clip);
        setting = true;
    }

    public void trueCancel() {
        back.PlayOneShot(back.clip);
        cancel = true;
    }
}