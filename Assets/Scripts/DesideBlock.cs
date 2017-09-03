using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesideBlock : MonoBehaviour {
    public int pnum;
    public int minonum;
    string pname;
    int row = 0;
    int column = 0;

	// Use this for initialization
	void Start () {
        pname = "P" + pnum;
        AllGray();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AllGray() {
        for(int i = 0; i < 25; i++) {
            row = i / 5;
            column = i % 5;
            Button b = GetSelectButton();
            b.interactable = false;
            ColorManager.BtnStateColorChange(b, Color.gray, 3);
        }
    }

    public void SetColor(int[,] cells) {
        for(int i = 0; i < 25; i++) {
            row = i / 5;
            column = i % 5;
            Button b = GetSelectButton();
            if(cells[row, column] > 0) {
                ColorManager.BtnStateColorChange(b, World.coler[minonum], 3);
            } else {
                ColorManager.BtnStateColorChange(b, Color.white, 3);
            }
        }
    }

    Button GetSelectButton() {
        Button tmp = transform
            .Find("P1Line" + (row + 1))
            .Find("P1Button" + (row * 5 + column + 1))
            .GetComponent<Button>();
        return tmp;
    }
}
