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

}
