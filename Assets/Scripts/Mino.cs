using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour {
	public int pnum = 1;

	int now_x = 4, now_y = 23;
	int next_x = 4, next_y = 23;
	int[,] basecell;    // cellの値 0=空白, 1~4=通常ブロック, 5=追加ブロック
	int[,] nowcell = new int[5,5];
	int[,] nextcell = new int[5,5];
	int rot = 0; //0, 1, 2, 3
	float timer = 0f, clock = 1f;
	GameObject[] box = new GameObject[5];

	void Start () {    // 初期設定
	}

	void Update () {
		if (timer > clock) {  // ミノの自動落下
			next_y -= 1;
			PutBoxes (!CheckEnable ());
			timer = 0f;
		}
		// ミノの右移動
		// ミノの左移動
		// ミノの右回転
		// ミノの左回転
		// ミノの高速落下
		// ミノの即着地

		timer -= Time.deltaTime;
	}

	public void Set(int minonum, int rotnum){    // ミノの取得
		basecell = World.Plr[pnum].mino[minonum].cell;
		Rotating (rotnum);
	}

	void PutBoxes(bool isFinal){    // ブロック配置
		int tmpx = now_x - 2, tmpy = now_y - 2;
		next2now ();

		if (isFinal) {
		} else {
		}
	}

	bool CheckEnable(){    // 動けるか判定
		int tmpx = next_x - 2, tmpy = next_y - 2;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (nextcell [i, j] > 0) {
					if (World.Plr [pnum].stage [i + tmpx, j + tmpy] > 0) {
						now2next ();
						return false;
					}
				}
			}
		}
		next2now ();
		return true;
	}

	void now2next(){    // 移動予定位置を取り消し
		next_x = now_x;
		next_y = now_y;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				nextcell [i, j] = nowcell [i, j];
			}
		}
	}

	void next2now(){    // 現在位置を移動予定位置に書き換え
		now_x = next_x;
		now_y = next_y;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				nowcell [i, j] = nextcell [i, j];
			}
		}
	}

	void Rotating(int degree){     //degree分だけ90度時計回り  degree = 0, 1, 2, 3
		rot = (rot + degree) % 4;
		Vector2 mps;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				mps = Chgxy (i, j, rot);
				nextcell [(int)mps.x, (int)mps.y] = basecell [i, j];
			}
		}
	}

	Vector2 Chgxy (int xx, int yy, int funcrot) {    // 回転後のx,yの関係
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
