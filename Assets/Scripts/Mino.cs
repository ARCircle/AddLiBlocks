using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour {
	public int pnum = 0;
	string pstring;
	bool doStartDelay = false;
    
	int now_x = 5, now_y = 23;
	int next_x = 5, next_y = 23;
	int[,] basecell;    // cellの値 0=空白, 1~4=通常ブロック, 5=追加ブロック
	int[,] nowcell = new int[5,5];
	int[,] nextcell = new int[5,5];
	int nowrot = 0, nextrot = 0; //0, 1, 2, 3
	bool agaki = false; int agalim = 3;
	float timer = 0f, clock = 1f, accel = 10f;

	AudioSource turn, landing;
	GameObject boxbase;
	GameObject useobj;
	GameObject[] box = new GameObject[5];

	void StartDelay () {    // 初期設定
		if (pnum == 0) {
			pstring = "1P_";
		} else {
			pstring = "2P_";
		}
		boxbase = transform.Find("Block").gameObject;
		useobj = transform.Find("UseBlocks").gameObject;
		World.Plr [pnum].stackobj = transform.Find("BlockStack");
		World.Plr [pnum].myself = this;
		GameObject audioobj = GameObject.Find ("Audios");
		turn = audioobj.transform.Find ("kaiten").GetComponent<AudioSource> ();
		landing = audioobj.transform.Find ("chakuchi").GetComponent<AudioSource> ();
		doStartDelay = true;
	}

	void Update () {
		if (doStartDelay && World.Plr[pnum].mino_controling && World.gameover < 0) {	
			
			bool anglepush = Input.GetButtonDown (pstring + "RightRotate");
			float angle = Input.GetAxis (pstring + "RightRotate");
			if (anglepush) {
				agaki = true;
				int[] mvx = new int[]{ 0, -1, 1, -2, 2 };
				if (angle > 0.1f) {    // ミノの右回転
					for (int i = 0; i < 5; i++){
						Rotating (1);
						next_x += mvx [i];
						if (CheckEnable ()) {    // 実際にブロック移動
							PutBoxes (false);
							turn.PlayOneShot (turn.clip);
							break;
						}
					}
				} else if (angle < -0.1f) {   // ミノの左回転
					for (int i = 0; i < 5; i++){
						Rotating (3);
						next_x += mvx [i];
						if (CheckEnable ()) {    // 実際にブロック移動
							PutBoxes (false);
							turn.PlayOneShot (turn.clip);
							break;
						}
					}
				}

			}

			bool movepush = Input.GetButtonDown (pstring + "RightMove");
			float move = Input.GetAxis (pstring + "RightMove");
			if (movepush) {
				agaki = true;
				if (move > 0.1f)   // ミノの右移動
					next_x += 1;
				else if (move < -0.1f)   // ミノの左移動
					next_x -= 1;
				if (CheckEnable ())    // 実際にブロック移動
					PutBoxes (false);
			}

			bool updownpush = Input.GetButtonDown (pstring + "Down");
			float updown = Input.GetAxis (pstring + "Down");
			if (updown > 0.1f) {    // ミノの高速落下
				if (timer < clock * (1f - 1f / accel) / World.speed) {
					timer += clock * (1f - 1f / accel) / World.speed;
                }
			}
			if (timer > clock / World.speed) {  // ミノの自動落下
				next_y -= 1;
				if (!CheckEnable ()) {
					if (agaki && agalim > 0) {
						agalim--;
					} else {
						PutBoxes (true);
					}
				} else {
					PutBoxes (false);
				}
				agaki = false;
				timer = 0f;
			}

			if (updownpush) {
				if (updown < -0.1f) {    // ミノの即着地
					while (World.Plr [pnum].mino_controling) {
						next_y -= 1;
						PutBoxes (!CheckEnable ());
					}
				}
			}
			timer += Time.deltaTime;

		} else if (!doStartDelay){
			StartDelay ();
		}
	}

	public void Set(int minonum, int rotnum){    // ミノの取得
		World.Plr[pnum].mino_controling = true;
		now_x = 5; now_y = 23;
		next_x = 5; next_y = 23;
		nowrot = 0; nextrot = 0; timer = 0f;
		agaki = false; agalim = 10;
		for (int i = 0; i < 5; i++) {    // 実際に配置するブロックの生成
			box [i] = MakeBlock (now_x, now_y, World.coler[minonum], useobj.transform);
			if (i == 4)
				box [i].GetComponent<BlockScript> ().aura = true;
		}
		basecell = World.Plr[pnum].mino[minonum].cell;
		Rotating (rotnum);
		next2now ();
		//if (!CheckEnable ()) {
		//	World.gameover = pnum;
		//}
	}

	public void UpRow(){    //一段上昇時、操作中のミノも一段上昇させる
		if(now_y < 23)
			now_y += 1;
		if(next_y < 23)
			next_y += 1;
	}

	public GameObject MakeBlock(int xx, int yy, Color col, Transform parentobj){   // ブロック作成
		GameObject tmp = Instantiate<GameObject> (boxbase);
		tmp.GetComponent<MeshRenderer> ().enabled = true;
		tmp.GetComponent<Renderer> ().material.color = col;
		tmp.GetComponent<BlockScript> ().SetPnum(pnum);
		tmp.transform.SetParent (parentobj);
		tmp.transform.localPosition = new Vector3 (xx, yy, 0f);
		return tmp;
	}

	bool InRangeCheck(int a, int b) {    // 枠外判定
		if (a < 0 || b < 0) {
			return false;
		} else {
			return true;
		}
	}

	void PutBoxes(bool isFinal){    // ブロック配置
		int tmpx = now_x - 2, tmpy = now_y - 2, max_y = 99;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (nowcell [j, i] > 0 && InRangeCheck(j + tmpx, i + tmpy)) {
					//Debug.Log ("" + nowcell [j, i] + " : j=" + j + ", i=" + i);
					if (isFinal) {
						World.Plr [pnum].InputStack(j + tmpx, i + tmpy, nowcell [j, i], box [nowcell [j, i] - 1]);
                        if(max_y > i + tmpy)
                            max_y = i + tmpy;
                        //Debug.Log ((j + tmpx) + "," + (i + tmpy));
                    }
					box [nowcell [j, i] - 1].transform.localPosition = new Vector3 (j + tmpx, i + tmpy, 0);
				}
			}
		}
		if (isFinal) {
			World.Plr [pnum].mino_controling = false;
			World.Plr [pnum].effect = true;
			landing.PlayOneShot (landing.clip);
            if (max_y > 20)
                World.gameover = pnum;
        }
	}

	public void AuraOff(){    // 追加ブロックの光をオフ
		if (box [4] != null) {
			box [4].GetComponent<BlockScript> ().StopAura ();
		}
	}

	bool CheckEnable(){    // 動けるか判定
		int tmpx = next_x - 2, tmpy = next_y - 2;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (nextcell [j, i] > 0 && InRangeCheck(j + tmpx, i + tmpy)) {
					if (World.Plr [pnum].stage [j + tmpx, i + tmpy] > 0) {
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
		nextrot = nowrot;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				nextcell [j, i] = nowcell [j, i];
			}
		}
	}

	void next2now(){    // 現在位置を移動予定位置に書き換え
		now_x = next_x;
		now_y = next_y;
		nowrot = nextrot;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				nowcell [j, i] = nextcell [j, i];
			}
		}
	}

	void Rotating(int degree){     //degree分だけ90度時計回り  degree = 0, 1, 2, 3
		nextrot = (nextrot + degree) % 4;
		Vector2 mps;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				mps = Chgxy (j, i, nextrot);
				nextcell [(int)mps.y, (int)mps.x] = basecell [j, i];
			}
		}
	}

	Vector2 Chgxy (int xx, int yy, int funcrot) {    // 回転後のx,yの関係
		int tmpx = xx - 2, tmpy = yy - 2;
		switch(funcrot){
		case 3:
			return new Vector2(tmpy + 2, -tmpx + 2);
		case 2:
			return new Vector2(-tmpx + 2, -tmpy + 2);
		case 1:
			return new Vector2(-tmpy + 2, tmpx + 2);
		default:
			return new Vector2(tmpx + 2, tmpy + 2);
		}
	}
}
