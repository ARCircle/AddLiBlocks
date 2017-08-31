using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	Transform guide;
	GameObject blockprefab;
	GameObject[,] nextplace = new GameObject[2, 3];
	GameObject[,,] npblocks = new GameObject[2, 3, 5];

	// Use this for initialization
	void Start () {
		guide = transform;//transform.parent.Find ("NextFrameGuide");
		Debug.Log (guide);
		blockprefab = guide.Find ("Block").gameObject;
		string[] pname = new string[]{"1P_", "2P_"};
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 3; j++) {
				nextplace [i, j] = guide.Find (pname [i] + j).gameObject;
				for (int k = 0; k < 5; k++) {
					npblocks [i, j, k] = Instantiate<GameObject> (blockprefab);
					npblocks [i, j, k].GetComponent<MeshRenderer> ().enabled = true;
					npblocks [i, j, k].transform.SetParent (nextplace [i, j].transform);
					npblocks [i, j, k].transform.localScale = Vector3.one;
				}
			}
		}

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
		});/*
		World.Plr [0].mino [7].SetCell (new int[,] {
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 1, 0, 0 },
			{ 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0 }
		});*/
		World.Plr [1].mino = World.Plr [0].mino;
		// < 本来必要ない部分 -終わり- >
	}

	// Update is called once per frame
	void Update () {
		Vector2[] attack = new Vector2[2]{    //attack[].x ...揃ったライン数
			World.Plr [0].Judge (),           //attack[].y ...0 = 判定期間外  -1 = 判定期間内
			World.Plr [1].Judge ()            //              1 = 判定期間内 & 追加ブロックを使用している
		};

		for (int i = 0; i < 2; i++) {
			if (attack [i].y != 0) {
				// 列の上昇
				int pow = (int)attack [i].x;
				int uprows = 0;
				if (pow > 0) {
					if (pow <= 3) {
						uprows = pow - 1;
					} else {
						uprows = pow;
					}
					if (attack [i].y > 0f) {
						uprows += 1;
					}
					World.Plr [1-i].UpStack (uprows);
				}
				// ミノの更新
				SetNextMino (i);
			}
		}
	}

	void SetNextMino(int pnum) {
		for (int h = 0; h < 3; h++) {
			int n = World.Plr [pnum].nextmino [h];
			for (int i = 0; i < 5; i++) {
				for (int j = 0; j < 5; j++){
					int m = World.Plr [pnum].mino [n].cell [i, j];					
					if (m > 0) {
						/*if (n == 7) {
							for (int k = 0; k < 5; k++) {
								npblocks [pnum, h, k].transform.localPosition = new Vector3 (j, i, k);
								npblocks [pnum, h, k].GetComponent<Renderer> ().material.color = World.coler [n];
							}
						} else {*/
							npblocks [pnum, h, m - 1].transform.localPosition = new Vector3 (j, i, 0);
							npblocks [pnum, h, m - 1].GetComponent<Renderer> ().material.color = World.coler [n];
						//}
					}
				}
			}
		}
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
