using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	Transform guide;
	GameObject blockprefab;
	GameObject[,] nextplace = new GameObject[2, 3];
	GameObject[,,] npblocks = new GameObject[2, 3, 5];
	AudioSource erase_weak, erase_strong;
	bool DirectStart = false;
    public GameObject laser;
    public Transform[] p_frame;
	float starttimer = 3f, downrowtimer = -1f;

	// Use this for initialization
	void Start () {
		guide = transform;//transform.parent.Find ("NextFrameGuide");
		//Debug.Log (guide);
		blockprefab = guide.Find ("Block").gameObject;
		string[] pname = new string[]{"1P_", "2P_"};
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 3; j++) {
				nextplace [i, j] = guide.Find(pname[i] + "Next").Find (pname [i] + j).gameObject;
				for (int k = 0; k < 5; k++) {
					npblocks [i, j, k] = Instantiate<GameObject> (blockprefab);
					npblocks [i, j, k].GetComponent<MeshRenderer> ().enabled = true;
					npblocks [i, j, k].transform.SetParent (nextplace [i, j].transform);
					npblocks [i, j, k].transform.localScale = Vector3.one;
				}
			}
		}
		GameObject audioobj = GameObject.Find ("Audios");
		erase_weak = audioobj.transform.Find ("shoukyo").GetComponent<AudioSource> ();
		erase_strong = audioobj.transform.Find ("power_shoukyo").GetComponent<AudioSource> ();

		if (DirectStart) {
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
		}
		// < 本来必要ない部分 -終わり- >

		SetNextMino (0);
		SetNextMino (1);
	}

	// Update is called once per frame
	void Update () {
        World.speed += Time.deltaTime / 60f;
		if (World.gameover < 0) {
			if (starttimer < 0f) {
				Vector2[] attack = new Vector2[2] {    //attack[].x ...揃ったライン数
					World.Plr [0].Judge (),           //attack[].y ...0 = 判定期間外  -1 = 判定期間内
					World.Plr [1].Judge ()            //              1 = 判定期間内 & 追加ブロックを使用している
				};
				for (int i = 0; i < 2; i++) {
					if (attack [i].y != 0) {
						int pow = (int)attack [i].x;
						if (World.Plr [i].effect) {
							if (downrowtimer < 0f) {
                                CreateLaser(i, pow);
								downrowtimer = 100.15f;
							} else {
								downrowtimer -= Time.deltaTime;
								if (downrowtimer < 100f) {
									World.Plr [i].effect = false;
									downrowtimer = -1f;
								}
							}
						} else {
							// 列の上昇
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
								World.Plr [1 - i].UpStack (uprows);
							}
							// ミノの更新
							SetNextMino (i);
						}
					}
				}
			} else {
				starttimer -= Time.deltaTime;
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
						npblocks [pnum, h, m - 1].transform.localPosition = new Vector3 (j, i, 0);
						npblocks [pnum, h, m - 1].GetComponent<Renderer> ().material.color = World.coler [n];
						/*if (n == 7) {
							for (int k = 0; k < 5; k++) {
								npblocks [pnum, h, k].transform.localPosition = new Vector3 (j, i, k);
								npblocks [pnum, h, k].GetComponent<Renderer> ().material.color = World.coler [n];
							}
						} else {*/
						//}
					}
				}
			}
		}
	}

    void CreateLaser(int p, int num) {
        for(int j = 0; j < num; j++) {
            int select_y = World.Plr[p].c_row[j];
            /*for(int k = 1; k <= 10; k++) {
                if(World.Plr[p].blocks_stack[k, select_y] != null)
                    World.Plr[p].blocks_stack[k, select_y].GetComponent<BlockScript>().SetWhite();
            }*/
            GameObject tmp1 = Instantiate<GameObject>(laser);
            GameObject tmp2 = Instantiate<GameObject>(laser);
            tmp1.transform.position = new Vector3(p_frame[p].position.x + 5.5f, select_y + p_frame[p].position.y, -1f);
            tmp2.transform.position = new Vector3(p_frame[p].position.x + 5.5f, select_y + p_frame[p].position.y, -1f);
            tmp1.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            tmp2.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            if(select_y == World.Plr[p].B5row) {//attack [i].y > 0) {
                BeamParam BP = tmp1.GetComponent<BeamParam>();
                BP.BeamColor = new Color(1f, 0.7f, 1f);
                BP.Scale = 4f;
                BP.AnimationSpd = 0.03f;
                erase_strong.PlayOneShot(erase_strong.clip);
                BP = tmp2.GetComponent<BeamParam>();
                BP.BeamColor = new Color(1f, 0.7f, 1f);
                BP.Scale = 4f;
                BP.AnimationSpd = 0.03f;
            } else {
                erase_weak.PlayOneShot(erase_weak.clip);
            }
            //tmp.transform.rotation = Quaternion.Euler (0f, 90f, 0f);
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
