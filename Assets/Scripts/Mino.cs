﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour {
	public int pnum = 0;
	string pstring;
	bool doStartDelay = false;

	int now_x = 4, now_y = 23;
	int next_x = 4, next_y = 23;
	int[,] basecell;    // cellの値 0=空白, 1~4=通常ブロック, 5=追加ブロック
	int[,] nowcell = new int[5,5];
	int[,] nextcell = new int[5,5];
	int rot = 0; //0, 1, 2, 3
	float timer = 0f, clock = 0.5f, accel = 10f;

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
		doStartDelay = true;
	}

	void Update () {
		if (doStartDelay) {	
			
			bool anglepush = Input.GetButtonDown (pstring + "RightRotate");
			float angle = Input.GetAxis (pstring + "RightRotate");
			if (anglepush) {
				if (angle > 0.1f)    // ミノの右回転
					Rotating (1);
				else if (angle < -0.1f)    // ミノの左回転
					Rotating (3);
				if (CheckEnable ())    // 実際にブロック移動
					PutBoxes (false);
			}

			bool movepush = Input.GetButtonDown (pstring + "RightMove");
			float move = Input.GetAxis (pstring + "RightMove");
			if (movepush) {
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
				if (timer < clock * (1f - 1f / accel)) {
					timer += clock * (1f - 1f / accel);				
				}
			}
			if (timer > clock) {  // ミノの自動落下
				next_y -= 1;
				PutBoxes (!CheckEnable ());
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

		} else {
			StartDelay ();
		}
	}

	public void Set(int minonum, int rotnum){    // ミノの取得
		World.Plr[pnum].mino_controling = true;
		now_x = 4; now_y = 23;
		next_x = 4; next_y = 23;
		for (int i = 0; i < 5; i++) {    // 実際に配置するブロックの生成
			box [i] = Instantiate<GameObject> (boxbase);
			box [i].transform.localPosition = new Vector3 (now_x, now_y, 0f);
			box [i].transform.SetParent (useobj.transform);
			box [i].GetComponent<MeshRenderer> ().enabled = true;
		}
		basecell = World.Plr[pnum].mino[minonum].cell;
		Rotating (rotnum);
		next2now ();
	}

	public void UpRow(){    //一段上昇時、操作中のミノも一段上昇させる
		now_y += 1;
		next_y += 1;
	}

	void PutBoxes(bool isFinal){    // ブロック配置
		int tmpx = now_x - 2, tmpy = now_y - 2;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				if (nextcell [i, j] > 0) {
					if (isFinal) {
						World.Plr [pnum].InputStack(i + tmpx, j + tmpy, 
							nextcell [i, j], box [nextcell [i, j] - 1]);
					}
					box [nextcell [i, j] - 1].transform.localPosition = new Vector3 (i + tmpx, j + tmpy, 0);
				}
			}
		}
		if (isFinal)
			World.Plr[pnum].mino_controling = false;
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
