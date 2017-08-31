using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
<<<<<<< HEAD
	public const int MINONUM = 3, HEIGHT = 20, WIDTH = 10;
=======
	const int MINONUM = 7;
>>>>>>> origin/Taguma
	public bool mino_controling = false;
	public int[,] stage = new int[WIDTH + 2, HEIGHT + 6];  // 値が0=空白, 9=壁, 1~4=通常ブロック, 5=追加ブロック
	public GameObject[,] blocks_stack = new GameObject[WIDTH + 2, HEIGHT + 6];
	public MinoShape[] mino = new MinoShape[MINONUM];
	public Mino myself;
	public Transform stackobj;

	// そろったとき専用の変数
	//GameObject target;
	public bool effect = false;
	public int B5row = 0;
	public int[] c_row = new int[HEIGHT];

	public PlayerInfo(){    // 最初の定義
		for (int i = 0; i < HEIGHT + 6; i++) {
			for (int j = 0; j < WIDTH + 2; j++) {
				if (i==0 || j==0 || j==WIDTH+1){
					stage[j, i] = 9;
				} else {
					stage[j, i] = 0;
				}
				blocks_stack [j, i] = null;
			}
		}
		for (int i = 0; i < MINONUM; i++){
			mino[i] = new MinoShape();
		}
		for (int i = 0; i < HEIGHT; i++){
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
		for (int i = 1; i <= HEIGHT; i++) {
			complete = true;
			for (int j = 1; j <= WIDTH; j++){
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
		bool ans = false;
		for (int i = 0; i < HEIGHT; i++) {
			if (c_row [i] == 0) {
				break;
			} else if (c_row [i] == B5row) {
				ans = true;
				break;
			}
		}
		B5row = 0;
		return ans;
	}

	public void DownStack(){    // たまっているブロックを下げる
		int k = 1, downsize = 0;
		for (int i = 1; k <= HEIGHT; i++) {
			if (c_row [downsize] == i) {
				c_row [downsize] = 0;
				downsize++;
			} else {
				for (int j = 1; j <= 10; j++) {
					BlockSlide (j, k, -downsize);
				}
				k++;
			}
		}
	}

	public void UpStack(int upnum){    // 相手からの攻撃
		int spacex = 0;
		for(int h = 0; h < upnum; h++){
			myself.UpRow ();
			for (int i = 20; i > 0; i--) {
				for (int j = 1; j <= 10; j++){
					BlockSlide (j, i, 1);
				}
			}
			spacex = Random.Range (1, 11);
			for (int j = 1; j <= 10; j++){
				if (j != spacex) {
					stage [j, 1] = 1;
					blocks_stack [j, 1] = myself.MakeBlock (j, 1, Color.gray, stackobj);
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

	void BlockSlide(int jj, int ii, int upslide){   // たまっているブロックを任意の段数だけ上下移動 移動先を指定
		if (ii < 1 || HEIGHT < ii || upslide == 0) {
			return;
		}
		else if (ii - upslide < 1 || HEIGHT < ii - upslide) {
			stage [jj, ii] = 0;
			if (blocks_stack [jj, ii] != null) {
				blocks_stack [jj, ii].GetComponent<BlockScript> ().Suicide ();
				blocks_stack [jj, ii] = null;
			}
		} else {
			stage [jj, ii] = stage [jj, ii - upslide];
			if (blocks_stack [jj, ii - upslide] != null) {
				if (blocks_stack [jj, ii] != null) {
					blocks_stack [jj, ii].GetComponent<BlockScript> ().Suicide ();
					blocks_stack [jj, ii] = null;
				}
				blocks_stack [jj, ii - upslide].transform.localPosition = new Vector3 (jj, ii, 0);
				blocks_stack [jj, ii] = blocks_stack [jj, ii - upslide];
				blocks_stack [jj, ii - upslide] = null;
			} else {
				if (blocks_stack [jj, ii] != null) {
					blocks_stack [jj, ii].GetComponent<BlockScript> ().Suicide ();
					blocks_stack [jj, ii] = null;
				}
			}
		}
	}
}