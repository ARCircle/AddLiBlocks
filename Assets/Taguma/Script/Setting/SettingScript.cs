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
    AudioSource enter;
	string xLine;
	string yLine;
    bool preset = true, setting = false;

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

	Button B1; Button B2; Button B3; Button B4; Button B5;
	Button B6; Button B7; Button B8; Button B9; Button B10;
	Button B11; Button B12; Button B13; Button B14; Button B15;
	Button B16; Button B17; Button B18; Button B19; Button B20;
	Button B21; Button B22; Button B23; Button B24; Button B25;

	// Use this for initialization
	void Start () {
        enter = transform.parent.GetComponent<AudioSource>();
        World.setPlayerInfo ();
        scene = this.GetComponent<SettingScene>();
		st.Input ();
		B1 = obj1.GetComponent<Button> ();
		B2 = obj2.GetComponent<Button> ();
		B3 = obj3.GetComponent<Button> ();
		B4 = obj4.GetComponent<Button> ();
		B5 = obj5.GetComponent<Button> ();
		B6 = obj6.GetComponent<Button> ();
		B7 = obj7.GetComponent<Button> ();
		B8 = obj8.GetComponent<Button> ();
		B9 = obj9.GetComponent<Button> ();
		B10 = obj10.GetComponent<Button> ();
		B11 = obj11.GetComponent<Button> ();
		B12 = obj12.GetComponent<Button> ();
		B13 = obj13.GetComponent<Button> ();
		B14 = obj14.GetComponent<Button> ();
		B15 = obj15.GetComponent<Button> ();
		B16 = obj16.GetComponent<Button> ();
		B17 = obj17.GetComponent<Button> ();
		B18 = obj18.GetComponent<Button> ();
		B19 = obj19.GetComponent<Button> ();
		B20 = obj20.GetComponent<Button> ();
		B21 = obj21.GetComponent<Button> ();
		B22 = obj22.GetComponent<Button> ();
		B23 = obj23.GetComponent<Button> ();
		B24 = obj24.GetComponent<Button> ();
		B25 = obj25.GetComponent<Button> ();

        manager.P1 = false;
        manager.P2 = false;
    }
	
	// Update is called once per frame
	void Update () {
		switch (minonum) {
		case 0:
            if(preset) {
                preset = false;
                minocellsDefault[1, 2] = 1;
                minocellsDefault[2, 2] = 2;
                minocellsDefault[3, 2] = 3;
                minocellsDefault[4, 2] = 4;
                B8.interactable = false;
                B13.interactable = false;
                B18.interactable = false;
                B23.interactable = false;
                ColorManager.BtnStateColorChange(B8, Color.cyan, 3);
                ColorManager.BtnStateColorChange(B13, Color.cyan, 3);
                ColorManager.BtnStateColorChange(B18, Color.cyan, 3);
                ColorManager.BtnStateColorChange(B23, Color.cyan, 3);

                B1.interactable = false;
                B2.interactable = false;
                B4.interactable = false;
                B5.interactable = false;
                B6.interactable = false;
                B10.interactable = false;
                B11.interactable = false;
                B15.interactable = false;
                B16.interactable = false;
                B20.interactable = false;
                B21.interactable = false;
                B25.interactable = false;
                ColorManager.BtnStateColorChange(B1, Color.gray, 3);
                ColorManager.BtnStateColorChange(B2, Color.gray, 3);
                ColorManager.BtnStateColorChange(B4, Color.gray, 3);
                ColorManager.BtnStateColorChange(B5, Color.gray, 3);
                ColorManager.BtnStateColorChange(B6, Color.gray, 3);
                ColorManager.BtnStateColorChange(B10, Color.gray, 3);
                ColorManager.BtnStateColorChange(B11, Color.gray, 3);
                ColorManager.BtnStateColorChange(B15, Color.gray, 3);
                ColorManager.BtnStateColorChange(B16, Color.gray, 3);
                ColorManager.BtnStateColorChange(B20, Color.gray, 3);
                ColorManager.BtnStateColorChange(B21, Color.gray, 3);
                ColorManager.BtnStateColorChange(B25, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 2] = 0;
				minocellsDefault [2, 2] = 0;
				minocellsDefault [3, 2] = 0;
				minocellsDefault [4, 2] = 0;

				B2.interactable = true;
				B6.interactable = true;
				B8.interactable = true;
				B13.interactable = true;
				B18.interactable = true;
				B23.interactable = true;

				B11.interactable = true;
				B15.interactable = true;

                preset = true;
                setting = false;
			}
			break;
		case 1:
            if(preset) {
                preset = false;
                minocellsDefault[1, 1] = 1;
                minocellsDefault[2, 1] = 2;
                minocellsDefault[2, 2] = 3;
                minocellsDefault[2, 3] = 4;
                B7.interactable = false;
                B12.interactable = false;
                B13.interactable = false;
                B14.interactable = false;
                ColorManager.BtnStateColorChange(B7, Color.blue, 3);
                ColorManager.BtnStateColorChange(B12, Color.blue, 3);
                ColorManager.BtnStateColorChange(B13, Color.blue, 3);
                ColorManager.BtnStateColorChange(B14, Color.blue, 3);

                B3.interactable = false;
                B4.interactable = false;
                B22.interactable = false;
                B23.interactable = false;
                B24.interactable = false;
                ColorManager.BtnStateColorChange(B3, Color.gray, 3);
                ColorManager.BtnStateColorChange(B4, Color.gray, 3);
                ColorManager.BtnStateColorChange(B22, Color.gray, 3);
                ColorManager.BtnStateColorChange(B23, Color.gray, 3);
                ColorManager.BtnStateColorChange(B24, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 1] = 0;
				minocellsDefault [2, 1] = 0;
				minocellsDefault [2, 2] = 0;
				minocellsDefault [2, 3] = 0;

				B7.interactable = true;
				B8.interactable = true;
				B12.interactable = true;
				B13.interactable = true;
				B14.interactable = true;

				B4.interactable = true;
				B10.interactable = true;

                preset = true;
                setting = false;
            }
            break;
		case 2:
            if(preset) {
                preset = false;
                minocellsDefault[1, 3] = 1;
                minocellsDefault[2, 1] = 2;
                minocellsDefault[2, 2] = 3;
                minocellsDefault[2, 3] = 4;
                B9.interactable = false;
                B12.interactable = false;
                B13.interactable = false;
                B14.interactable = false;
                ColorManager.BtnStateColorChange(B9, Color.magenta, 3);
                ColorManager.BtnStateColorChange(B12, Color.magenta, 3);
                ColorManager.BtnStateColorChange(B13, Color.magenta, 3);
                ColorManager.BtnStateColorChange(B14, Color.magenta, 3);

                B2.interactable = false;
                B3.interactable = false;
                B6.interactable = false;
                ColorManager.BtnStateColorChange(B2, Color.gray, 3);
                ColorManager.BtnStateColorChange(B3, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 3] = 0;
				minocellsDefault [2, 1] = 0;
				minocellsDefault [2, 2] = 0;
				minocellsDefault [2, 3] = 0;

				B9.interactable = true;
				B12.interactable = true;
				B13.interactable = true;
				B14.interactable = true;

				B2.interactable = true;
				B3.interactable = true;
				B6.interactable = true;

                preset = true;
                setting = false;
            }
            break;
		case 3:
            if(preset) {
                preset = false;
                minocellsDefault[1, 1] = 1;
                minocellsDefault[1, 2] = 2;
                minocellsDefault[2, 1] = 3;
                minocellsDefault[2, 2] = 4;
                B7.interactable = false;
                B8.interactable = false;
                B12.interactable = false;
                B13.interactable = false;
                ColorManager.BtnStateColorChange(B7, Color.yellow, 3);
                ColorManager.BtnStateColorChange(B8, Color.yellow, 3);
                ColorManager.BtnStateColorChange(B12, Color.yellow, 3);
                ColorManager.BtnStateColorChange(B13, Color.yellow, 3);

                B4.interactable = false;
                B10.interactable = false;
                B15.interactable = false;
                B19.interactable = false;
                ColorManager.BtnStateColorChange(B4, Color.gray, 3);
                ColorManager.BtnStateColorChange(B10, Color.gray, 3);
                ColorManager.BtnStateColorChange(B15, Color.gray, 3);
                ColorManager.BtnStateColorChange(B19, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 1] = 0;
				minocellsDefault [1, 2] = 0;
				minocellsDefault [2, 1] = 0;
				minocellsDefault [2, 2] = 0;

				B7.interactable = true;
				B8.interactable = true;
				B12.interactable = true;
				B13.interactable = true;

				B4.interactable = true;
				B10.interactable = true;
				B15.interactable = true;

                preset = true;
                setting = false;
            }
            break;
		case 4:
            if(preset) {
                preset = false;
                minocellsDefault[1, 2] = 1;
                minocellsDefault[1, 3] = 2;
                minocellsDefault[2, 1] = 3;
                minocellsDefault[2, 2] = 4;
                B8.interactable = false;
                B9.interactable = false;
                B12.interactable = false;
                B13.interactable = false;
                ColorManager.BtnStateColorChange(B8, Color.green, 3);
                ColorManager.BtnStateColorChange(B9, Color.green, 3);
                ColorManager.BtnStateColorChange(B12, Color.green, 3);
                ColorManager.BtnStateColorChange(B13, Color.green, 3);

                B2.interactable = false;
                B6.interactable = false;
                B15.interactable = false;
                ColorManager.BtnStateColorChange(B2, Color.gray, 3);
                ColorManager.BtnStateColorChange(B6, Color.gray, 3);
                ColorManager.BtnStateColorChange(B15, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 2] = 0;
				minocellsDefault [1, 3] = 0;
				minocellsDefault [2, 1] = 0;
				minocellsDefault [2, 2] = 0;

				B8.interactable = true;
				B9.interactable = true;
				B12.interactable = true;
				B13.interactable = true;

				B15.interactable = true;
				B19.interactable = true;

                preset = true;
                setting = false;
            }
            break;
		case 5:
            if(preset) {
                preset = false;
                minocellsDefault[1, 2] = 1;
                minocellsDefault[2, 1] = 2;
                minocellsDefault[2, 2] = 3;
                minocellsDefault[2, 3] = 4;
                B8.interactable = false;
                B12.interactable = false;
                B13.interactable = false;
                B14.interactable = false;
                ColorManager.BtnStateColorChange(B8, Color.red, 3);
                ColorManager.BtnStateColorChange(B12, Color.red, 3);
                ColorManager.BtnStateColorChange(B13, Color.red, 3);
                ColorManager.BtnStateColorChange(B14, Color.red, 3);

                B4.interactable = false;
                B10.interactable = false;
                ColorManager.BtnStateColorChange(B4, Color.gray, 3);
                ColorManager.BtnStateColorChange(B10, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 2] = 0;
				minocellsDefault [2, 1] = 0;
				minocellsDefault [2, 2] = 0;
				minocellsDefault [2, 3] = 0;

				B8.interactable = true;
				B12.interactable = true;
				B13.interactable = true;
				B14.interactable = true;

				B2.interactable = true;
				B6.interactable = true;

                preset = true;
                setting = false;
            }
			break;
		case 6:
            if(preset) {
                preset = false;
                minocellsDefault[1, 1] = 1;
                minocellsDefault[1, 2] = 2;
                minocellsDefault[2, 2] = 3;
                minocellsDefault[2, 3] = 4;
                B7.interactable = false;
                B8.interactable = false;
                B13.interactable = false;
                B14.interactable = false;
                ColorManager.BtnStateColorChange(B7, new Color(1f, 0.5f, 0f), 3);
                ColorManager.BtnStateColorChange(B8, new Color(1f, 0.5f, 0f), 3);
                ColorManager.BtnStateColorChange(B13, new Color(1f, 0.5f, 0f), 3);
                ColorManager.BtnStateColorChange(B14, new Color(1f, 0.5f, 0f), 3);

                B11.interactable = false;
                B17.interactable = false;
                ColorManager.BtnStateColorChange(B11, Color.gray, 3);
                ColorManager.BtnStateColorChange(B17, Color.gray, 3);
                scene.SetFirstButton();
            }
			if (setting) {
				for (int x = 0; x < 5; x++) {
					for (int y = 0; y < 5; y++) {
						minocells[x,y] = minocellsDefault[x,y] + minocellsInput[x,y];
					}
				}
				World.SettingMino (playernum, minonum, minocells);
                transform.parent.Find("P" + (3 - playernum) + "SelectedDisplay").GetChild(minonum).GetComponent<DesideBlock>().SetColor(minocells);
                minonum++;

				minocellsDefault [1, 1] = 0;
				minocellsDefault [1, 2] = 0;
				minocellsDefault [2, 2] = 0;
				minocellsDefault [2, 3] = 0;

				B1.interactable = false;
				B2.interactable = false;
				B3.interactable = false;
				B4.interactable = false;
				B5.interactable = false;
				B6.interactable = false;
				B7.interactable = false;
				B8.interactable = false;
				B9.interactable = false;
				B10.interactable = false;
				B11.interactable = false;
				B12.interactable = false;
				B13.interactable = false;
				B14.interactable = false;
				B15.interactable = false;
				B16.interactable = false;
				B17.interactable = false;
				B18.interactable = false;
				B19.interactable = false;
				B20.interactable = false;
				B21.interactable = false;
				B22.interactable = false;
				B23.interactable = false;
				B24.interactable = false;
				B25.interactable = false;

				ColorManager.BtnStateColorChange (B7, Color.gray, 3);
				ColorManager.BtnStateColorChange (B8, Color.gray, 3);
				ColorManager.BtnStateColorChange (B9, Color.gray, 3);
				ColorManager.BtnStateColorChange (B12, Color.gray, 3);
				ColorManager.BtnStateColorChange (B13, Color.gray, 3);
				ColorManager.BtnStateColorChange (B14, Color.gray, 3);
				ColorManager.BtnStateColorChange (B18, Color.gray, 3);

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
			break;
		}
	}

    public void PlayEnter() {
        enter.PlayOneShot(enter.clip);
    }

    public void trueSetting() {
        setting = true;
    }
}