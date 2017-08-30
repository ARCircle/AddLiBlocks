using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour {
	public int pnum = 1;

	int x = 4, y = 23;
	int[,] basecell;
	int[,] nowcell = new int[5,5];
	int[,] nextcell = new int[5,5];
	int rot = 0; //0, 1, 2, 3
	float timer = 0f, clock = 1f;
	GameObject[] box = new GameObject[5];

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > clock) {  // ミノの落下
			y -= 1;
			PutBoxes (!CheckEnable ());
			timer = 0f;
		}


		timer -= Time.deltaTime;
	}

	public void Set(int minonum, int rotnum){
		basecell = World.Plr[pnum].mino[minonum].cell;
		Rotating (rotnum);
	}

	void PutBoxes(bool isFinal){
		if (isFinal) {
		} else {
		}
	}

	bool CheckEnable(){
		int tmpx = x - 2, tmpy = y - 2;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (nextcell [i, j] > 0) {
					if (World.Plr [pnum].stage [i + tmpx, j + tmpy] > 0) {
						return false;
					}
				}
			}
		}
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				nowcell [i, j] = nextcell [i, j];
			}
		}
		return true;
	}

	void Rotating(int degree){ //degree = 0, 1, 2, 3
		rot = (rot + degree) % 4;
		Vector2 mps;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				mps = Chgxy (i, j, rot);
				nextcell [(int)mps.x, (int)mps.y] = basecell [i, j];
			}
		}
	}

	Vector2 Chgxy (int xx, int yy, int funcrot) {
		int tmpx = xx - 2, tmpy = yy - 2;
		switch(funcrot){
		case 1:
			return new Vector2(tmpy + 2, -tmpx + 2);
		case 2:
			return new Vector2(-tmpx + 2, -tmpy + 2);
		case 3:
			return new Vector2(-tmpy + 2, tmpx + 2);
		default:
			return new Vector2(tmpx + 2, tmpy + 2);
		}
	}
}
