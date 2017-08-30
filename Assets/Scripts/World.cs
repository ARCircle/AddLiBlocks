using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {
	
	public static P[] Plr = new P[2];

	public static void setP(){
		Plr [0] = new P ();
		Plr [1] = new P ();
	}

	public static void MinoReset(){

	}

	public static void GameReset(){
		
	}

	public class P{
		public int[,] stage = new int[12, 26];  //0~11,0~25 値が0=空白, 9=壁, 1=通常ブロック, 2=追加ブロック
		public MinoShape[] mino = new MinoShape[5];
		public P(){
			for (int i = 0; i < 12; i++) {
				for (int j = 0; j < 26; j++) {
					if (i==0 || i==11 || j==0){
						stage[i,j] = 9;
					} else {
						stage[i,j] = 0;
					}
				}
			}
		}
	}
}
