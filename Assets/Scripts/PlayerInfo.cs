using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo{
	public const int MINONUM = 7, HEIGHT = 20, WIDTH = 10;//, MINIBLOCK = 10000;
	public bool mino_controling = false, holdenable = true;
    public int holdmino = -1, nowmino = -1;
    public int[] nextmino = new int[]{0, 0, 0, 0, 0};
	public int[,] stage = new int[WIDTH + 2, HEIGHT + 6];  // 値が0=空白, 9=壁, 1~4=通常ブロック, 5=追加ブロック
	public GameObject[,] blocks_stack = new GameObject[WIDTH + 2, HEIGHT + 6];
    public MinoShape[] mino = new MinoShape[MINONUM];
	public Mino myself;
	public Transform stackobj;
    //int minib_cnt = 0;

    // そろったとき専用の変数
    //GameObject target;
    public bool effect = false;
	public int B5row = 0;
	public int[] c_row = new int[HEIGHT];

	public PlayerInfo(){    // 最初の定義
		for (int i = 0; i <= HEIGHT + 5; i++) {
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
		for (int i = 0; i < 5; i++){
            if(i == 0)
                nextmino[i] = Random.Range(0, MINONUM);
            else
                nextmino[i] = World.OtherNum(nextmino[i - 1], MINONUM);
            //minib_cnt++;
        }
	}

    public void InputHold() {
        if(mino_controling && holdenable) {
            myself.DeleteControlBlocks();
            if(holdmino < 0) {
                holdmino = nowmino;
                myself.Set(nextmino[0], 0);
                nowmino = nextmino[0];
                nextmino[0] = nextmino[1];
				nextmino[1] = nextmino[2];
				nextmino[2] = nextmino[3];
				nextmino[3] = nextmino[4];
                nextmino[4] = World.OtherNum(nextmino[3], MINONUM);
            } else {
                int tmp = nowmino;
                myself.Set(holdmino, 0);
                nowmino = holdmino;
                holdmino = tmp;
                holdenable = false;
            }
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
				if (UseB5 ()) {
					ans.y = 1f;
				} else {
					ans.y = -1f;
				}
				if (!effect) {
					B5row = 0;
                    holdenable = true;
					myself.AuraOff ();
					DownStack ();
					myself.Set (nextmino[0], 0);
					nowmino = nextmino[0];
					nextmino[0] = nextmino[1];
					nextmino[1] = nextmino[2];
					nextmino[2] = nextmino[3];
					nextmino[3] = nextmino[4];
					nextmino[4] = World.OtherNum(nextmino[3], MINONUM);
                    myself.SetNextMino();
					//minib_cnt++;
					//if (minib_cnt < MINIBLOCK) {
					//} else {
					//	nextmino [2] = MINONUM - 1;
					//	minib_cnt = 0;
					//}
				}
			}
		}
		return ans;
	}

	public int checkLine(){  // そろっている行があるかチェック
		int compcnt = 0;
		int complete = 0;
		for (int i = 1; i <= HEIGHT; i++) {
			complete = 0;
			for (int j = 1; j <= WIDTH; j++){
				if (stage [j, i] == 0) {    // そろってないことを検知
					complete++;
					//break;
				}
			}
			if (complete <= 10 - World.completenum) {
				c_row [compcnt] = i;
				compcnt++;
			}
		}
		if (compcnt <= 0) {
			effect = false;
		}
		return compcnt;
	}

	public bool UseB5(){    // 直前のミノの追加ブロックを利用して揃えたか
		bool ans = false;
		//Debug.Log ("B5row : " + B5row);
		for (int i = 0; i < HEIGHT; i++) {
			if (c_row [i] == 0) {
				break;
			} else if (c_row [i] == B5row) {
				ans = true;
				//Debug.Log ("useB5");
				break;
			}
		}
		return ans;
	}

	public void DownStack(){    // たまっているブロックを下げる
		int k = 1, downsize = 0;
		for (int i = 1; k <= HEIGHT + 5; i++) {
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
        int[] space = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int lim = 0, pos = 0;
        //string sp = "";
		for(int h = 0; h < upnum; h++){
			myself.UpRow ();
			for (int i = 20; i > 0; i--) {
				for (int j = 1; j <= 10; j++){
					BlockSlide (j, i, 1);
				}
			}
            for (int i = 0; i < 11 - World.completenum; i++) {
                lim = Random.Range(0, 10 - i);
                pos = lim;
                for (int j = 0; j <= pos; j++) {
                    if(space[j] != 0)
                        pos = (pos + 1) % 10;
                }
                space[pos] = 1;
            }
			for (int j = 1; j <= 10; j++){
                //sp += space[j - 1];
				if (space[j - 1] == 0) {
					stage [j, 1] = 1;
                    blocks_stack [j, 1] = myself.MakeBlock (j, 1, Color.gray, stackobj);
				} else {
                    space[j - 1] = 0;
                    stage [j, 1] = 0;
					if (blocks_stack [j, 1] != null) {
						blocks_stack [j, 1].GetComponent<BlockScript> ().Suicide ();
						blocks_stack [j, 1] = null;
					}
				}
			}
		}
        //Debug.Log(sp);
	}

	void BlockSlide(int jj, int ii, int upslide){   // たまっているブロックを任意の段数だけ上下移動 移動先を指定
		if (ii < 1 || HEIGHT < ii || upslide == 0) {
			return;
		}
		else if (ii - upslide < 1 || HEIGHT + 5 < ii - upslide) {
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