using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
	const int MINONUM = 3;
	public bool mino_controling = false;
	public int[,] stage = new int[12, 26];  // 値が0=空白, 9=壁, 1~4=通常ブロック, 5=追加ブロック
	public GameObject[,] blocks_stack = new GameObject[12, 26];
	public MinoShape[] mino = new MinoShape[MINONUM];
	public Mino myself;
	public Transform stackobj;

	// そろったとき専用の変数
	public bool effect = false;
	public int B5row = 0;
	public int[] c_row = new int[20];

	public PlayerInfo(){    // 最初の定義
		for (int i = 0; i < 26; i++) {
			for (int j = 0; j < 12; j++) {
				if (i==0 || j==0 || j==11){
					stage[j, i] = 9;
				} else {
					stage[j, i] = 0;
				}
			}
		}
		for (int i = 0; i < MINONUM; i++){
			mino[i] = new MinoShape();
		}
		for (int i = 0; i < 20; i++){
			c_row[i] = 0;
		}
	}

	public void InputStack(int xx, int yy, int value, GameObject oneblock){  // ミノをコントロール外へ
		stage [xx, yy] = value;
		oneblock.transform.SetParent (stackobj);
		blocks_stack [xx, yy] = oneblock;
		if (value == 5) {
			B5row = yy;
		}
	}

	public Vector2 Judge(){
		Vector2 ans = new Vector2(0f, 0f);
		if (myself != null) {
			if (!mino_controling) {
				ans.x = checkLine ();
				if(UseB5 ())
					ans.y = 1f;
				DownStack ();
				if (!effect) {
					myself.Set (Random.Range (0, 1), 0);
				}
			}
		}
		return ans;
	}

	public int checkLine(){  // そろっている行があるかチェック
		int compcnt = 0;
		bool complete = true;
		for (int i = 1; i <= 20; i++) {
			complete = true;
			for (int j = 1; j <= 10; j++){
				if (stage [j, i] == 0) {    // そろってないことを検知
					complete = false;
					break;
				}
			}
			if (complete) {
				c_row [compcnt] = i;
				compcnt++;
			}
		}
		return compcnt;
	}

	public bool UseB5(){    // 直前のミノの追加ブロックを利用して揃えたか
		for (int i = 0; i < 20; i++) {
			if (c_row [i] == 0)
				return false;
			else if (c_row [i] == B5row)
				return true;
		}
		return true;
	}

	public void DownStack(){    // たまっているブロックを下げる
		int downsize = 0;
		for (int i = 1; i < 26; i++) {
			if (c_row [downsize] == i) {
				c_row [downsize] = 0;
				downsize++;
				for (int j = 1; j <= 10; j++) {
					stage [j, i] = 0;
					if (blocks_stack [j, i] != null) {
						blocks_stack [j, i].GetComponent<BlockScript> ().Suicide ();
						blocks_stack [j, i] = null;
					}
				}
			} else {
				for (int j = 1; j <= 10; j++) {
					stage [j, i - downsize] = stage [j, i];
					blocks_stack [j, i - downsize] = blocks_stack [j, i];
					if (blocks_stack [j, i] != null) {
						blocks_stack [j, i].transform.localPosition = new Vector3 (j, i - downsize, 0);
					}
				}
			}
		}
		for (int i = 26 - downsize; i < 26; i++) {
			for (int j = 1; j <= 10; j++){
				stage [j, i] = 0;
				if (blocks_stack [j, i] != null) {
					blocks_stack [j, i].GetComponent<BlockScript> ().Suicide ();
					blocks_stack [j, i] = null;
				}
			}
		}
		B5row = 0;
	}

	public void UpStack(int upnum){    // 相手からの攻撃
		for(int h = 0; h < upnum; h++){
			myself.UpRow ();
			for (int i = 20; i > 0; i--) {
				for (int j = 1; j <= 10; j++){
					stage [j, i + 1] = stage [j, i];
					blocks_stack [j, i + 1] = blocks_stack [j, i];
					if (blocks_stack [j, i] != null) {
						blocks_stack [j, i].transform.localPosition = new Vector3 (j, i + 1, 0);
					}
				}
			}
			int spacex = Random.Range (1, 11);
			for (int j = 1; j <= 10; j++){
				if (j != spacex) {
					stage [j, 1] = 1;
					blocks_stack [j, 1] = myself.MakeBlock ();
					blocks_stack [j, 1].transform.localPosition = new Vector3 (j, 1, 0);
				} else {
					stage [spacex, 1] = 0;
					if (blocks_stack [spacex, 1] != null) {
						blocks_stack [spacex, 1].GetComponent<BlockScript> ().Suicide ();
						blocks_stack [spacex, 1] = null;
					}
				}
			}
		}
	}
}