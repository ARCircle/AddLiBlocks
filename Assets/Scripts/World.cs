using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {	
	public static PlayerInfo[] Plr = new PlayerInfo[2];

	public static void setPlayerInfo(){
		Plr [0] = new PlayerInfo ();
		Plr [1] = new PlayerInfo ();
	}

	public static void SettingMino(int playernum, int minonum, int[,] cells){  //ミノの配置代入
		Plr [playernum - 1].mino [minonum].SetCell(cells);
	}

	public static void MinoReset(){

	}

	public static void GameReset(){
		
	}

	public class PlayerInfo{
		public bool mino_controling = false;
		public int[,] stage = new int[12, 26];  // 値が0=空白, 9=壁, 1~4=通常ブロック, 5=追加ブロック
		public GameObject[,] blocks_stack = new GameObject[12, 26];
		public MinoShape[] mino = new MinoShape[3];
		public Mino myself;
		public Transform stackobj;

		public PlayerInfo(){
			for (int i = 0; i < 12; i++) {
				for (int j = 0; j < 26; j++) {
					if (i==0 || i==11 || j==0){
						stage[i,j] = 9;
					} else {
						stage[i,j] = 0;
					}
				}
			}
			for (int i = 0; i < 3; i++){
				mino[i] = new MinoShape();
			}
		}
		public void InputStack(int xx, int yy, int value, GameObject oneblock){
			stage [xx, yy] = value;
			oneblock.transform.SetParent (stackobj);
			blocks_stack [xx, yy] = oneblock;
		}
	}
}
