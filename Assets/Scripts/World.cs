using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World {	
	public static PlayerInfo[] Plr = new PlayerInfo[2];
	public static int gameover = -1;
    public static float speed = 1f;
    public static int completenum = 9;

	public static Color[] coler = new Color[]{
		Color.cyan,
		Color.blue,
		Color.magenta,
		Color.yellow, 
		Color.green,
		Color.red, 
		new Color(1f, 0.5f, 0f)
		//,new Color(1f, 0f, 1f)
	};

	public static void setPlayerInfo(){
		Plr [0] = new PlayerInfo ();
		Plr [1] = new PlayerInfo ();
		gameover = -1;
        speed = 1f;
	}

	public static void SettingMino(int playernum, int minonum, int[,] cells){  //ミノの配置代入
		Plr [playernum - 1].mino [minonum].SetCell(cells);
	}
    
    public static int OtherNum(int prenum, int maxnum) {    // 直前の数字以外の数値を出力するランダム
        int result = (prenum + Random.Range(0, maxnum - 1) + 1) % maxnum;
        //Debug.Log(result);
        return result;
    }

	//[MenuItem ("Tools/Clear Console %#c")]
	public static void ClearConsole() {
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null,null);
	}

}
