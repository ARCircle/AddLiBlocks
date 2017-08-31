﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// < 本来必要ない部分 -始め- >
		World.setPlayerInfo ();
		World.Plr [0].mino [0].SetCell (new int[,] {
			{ 0, 0, 1, 0, 0 },
			{ 0, 0, 2, 0, 0 },
			{ 0, 0, 3, 0, 0 },
			{ 0, 0, 4, 0, 0 },
			{ 0, 0, 5, 0, 0 }
		});
		World.Plr [0].mino [1].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 0, 0 },
			{ 0, 4, 2, 5, 0 },
			{ 0, 0, 3, 0, 0 },
			{ 0, 0, 0, 0, 0 }
		});
		World.Plr [0].mino [2].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 1, 0, 0, 0 },
			{ 0, 2, 0, 0, 0 },
			{ 0, 3, 4, 5, 0 },
			{ 0, 0, 0, 0, 0 }
		});
		World.Plr [0].mino [3].SetCell (new int[,] {
			{ 0, 0, 1, 0, 0 },
			{ 0, 0, 2, 0, 0 },
			{ 0, 0, 3, 0, 0 },
			{ 0, 5, 4, 0, 0 },
			{ 0, 0, 0, 0, 0 }
		});
		World.Plr [0].mino [4].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 3, 0 },
			{ 0, 0, 2, 4, 0 },
			{ 0, 0, 0, 5, 0 },
			{ 0, 0, 0, 0, 0 }
		});
		World.Plr [0].mino [5].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 0, 0 },
			{ 0, 0, 2, 3, 0 },
			{ 0, 0, 0, 4, 0 },
			{ 0, 0, 0, 5, 0 }
		});
		World.Plr [0].mino [6].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 0, 0 },
			{ 0, 3, 2, 0, 0 },
			{ 5, 4, 0, 0, 0 },
			{ 0, 0, 0, 0, 0 }
		});
		World.Plr [1].mino = World.Plr [0].mino;
		// < 本来必要ない部分 -終わり- >
	}

	// Update is called once per frame
	void Update () {
		Vector2[] attack = new Vector2[2]{
			World.Plr [0].Judge (), 
			World.Plr [1].Judge ()
		};
		for (int i = 0; i < 2; i++) {
			int pow = (int)attack [i].x;
			int uprows = 0;
			if (pow > 0) {
				if (pow <= 3) {
					uprows = pow - 1;
				} else {
					uprows = pow;
				}
				if (attack [i].y > 1f) {
					uprows += 1;
				}
				World.Plr [1-i].UpStack (uprows);
			}
		}
		/*World.ClearConsole ();
		string test = "";
		for (int i = 20; i >= 1; i--) {
			test = i + " : ";
			for (int j = 1; j <= 10; j++) {
				int val = World.Plr [1].stage [j, i];
				test += val;
			}
			Debug.Log (test);
		}*/
	}
}
