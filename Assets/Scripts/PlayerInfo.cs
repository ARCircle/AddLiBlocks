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
	public int compnum = 0, B5row = 0;
	public int[] c_row = new int[HEIGHT];
    public bool powerup = false, effect = false;
    public float effecttimer = 0f;

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

    public bool InputHold() {
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
            return true;
        } else {
            return false;
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

	public void Judge(){
		if (myself != null && !mino_controling && !effect) {
            checkLine();
            UseB5();
        }
	}

    public void Next() {
        compnum = 0; B5row = 0;
        powerup = false; effect = false; holdenable = true;
        effecttimer = 0f;
        myself.AuraOff();
        DownStack();
        if(World.gameover < 0) {
            myself.Set(nextmino[0], 0);
            nowmino = nextmino[0];
            int i = 0;
            for(; i < 4; i++) {
                nextmino[i] = nextmino[i + 1];
            }
            nextmino[i] = World.OtherNum(nextmino[i - 1], MINONUM);
            myself.SetNextMino();
        }
    }

	public void checkLine(){  // そろっている行があるかチェック
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
				c_row [compnum] = i;
				compnum++;
			}
        }
        if(compnum > 0)
            effect = true;
    }

	public void UseB5(){    // 直前のミノの追加ブロックを利用して揃えたか
        powerup = false;
		for (int i = 0; i < HEIGHT; i++) {
			if (c_row [i] == 0) {
				return;
			} else if (c_row [i] == B5row) {
				powerup = true;
				return;
			}
		}
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
        JudgeGameover();
    }

	public void UpStack(int upnum){    // 相手からの攻撃
        int[] space = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int lim = 0, pos = 0;
        //string sp = "";
		for(int h = 0; h < upnum; h++){
			myself.UpRow ();
			for (int i = HEIGHT + 5; i > 0; i--) {
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
        JudgeGameover();
        //Debug.Log(sp);
	}

	void BlockSlide(int jj, int ii, int upslide){   // たまっているブロックを任意の段数だけ上下移動 移動先を指定
		if (upslide == 0) {
			return;
		} else if (ii < 1 || HEIGHT + 5 < ii) {
            int tmpy = ii - upslide;
            if(1 <= tmpy && tmpy <= HEIGHT + 5 && blocks_stack[jj, tmpy] != null) {
                blocks_stack[jj, tmpy].GetComponent<BlockScript>().Suicide();
                blocks_stack[jj, tmpy] = null;
            }
        } else if (ii - upslide < 1 || HEIGHT + 5 < ii - upslide) {
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

    void JudgeGameover() {
        if(World.gameover < 0) {    // ゲームオーバー判定
            for(int i = 21; i <= 25; i++) {
                for(int j = 1; j <= 10; j++) {
                    if(stage[j, i] > 0) {
                        World.gameover = myself.pnum;
                        return;
                    }
                }
            }
        }
    }
}