using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		World.setPlayerInfo ();
		World.Plr [0].mino [0].SetCell (new int[,] {
			{ 0, 1, 0, 0, 0 },
			{ 0, 2, 0, 0, 0 },
			{ 0, 3, 0, 0, 0 },
			{ 0, 4, 0, 0, 0 },
			{ 0, 5, 0, 0, 0 }
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
	}
	
	// Update is called once per frame
	void Update () {
		if (!World.Plr [0].mino_controling && World.Plr [0].myself != null) {
			World.Plr [0].myself.Set (Random.Range (0, 3), 0);
		}
	}
}
