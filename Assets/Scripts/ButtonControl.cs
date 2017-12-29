using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour {

	PhotonView pv;

	// Use this for initialization
	void Start () {
		pv = GetComponent<PhotonView>();		
	}

	// Update is called once per frame
	void Update () {
		int pnum = World.connectnum + 1;
		//string pstring = pnum + "P_";
		string pstring = "1P_";
		bool rmov_down = Input.GetButtonDown (pstring + "RightMove");
		bool rrot_down = Input.GetButtonDown (pstring + "RightRotate");
		bool hold_down = Input.GetButtonDown (pstring + "Hold");
		bool down_down = Input.GetButtonDown (pstring + "Down");
		float rmov = Input.GetAxis(pstring + "RightMove");
		float rrot = Input.GetAxis(pstring + "RightRotate");
		float hold = Input.GetAxis(pstring + "Hold");
		float down = Input.GetAxis(pstring + "Down");
		pv.RPC(
			"SyncMyButton", PhotonTargets.All,
			pnum - 1,
			rmov_down, rrot_down, hold_down, down_down,
			rmov, rrot, hold, down
		);
	}

	/*void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)	{
		// マスターが書き込み、それ以外が読み込み
		if (stream.isWriting) {
			stream.SendNext(World.Plr[0].rmov_down);
			stream.SendNext(World.Plr[0].rrot_down);
			stream.SendNext(World.Plr[0].hold_down);
			stream.SendNext(World.Plr[0].down_down);
			stream.SendNext(World.Plr[0].rmov);
			stream.SendNext(World.Plr[0].rrot);
			stream.SendNext(World.Plr[0].hold);
			stream.SendNext(World.Plr[0].down);
			stream.SendNext(World.Plr[1].rmov_down);
			stream.SendNext(World.Plr[1].rrot_down);
			stream.SendNext(World.Plr[1].hold_down);
			stream.SendNext(World.Plr[1].down_down);
			stream.SendNext(World.Plr[1].rmov);
			stream.SendNext(World.Plr[1].rrot);
			stream.SendNext(World.Plr[1].hold);
			stream.SendNext(World.Plr[1].down);
		}
		else {
			World.Plr[0].rmov_down = (bool)stream.ReceiveNext();
			World.Plr[0].rrot_down = (bool)stream.ReceiveNext();
			World.Plr[0].hold_down = (bool)stream.ReceiveNext();
			World.Plr[0].down_down = (bool)stream.ReceiveNext();	
			World.Plr[0].rmov = (float)stream.ReceiveNext();
			World.Plr[0].rrot = (float)stream.ReceiveNext();
			World.Plr[0].hold = (float)stream.ReceiveNext();
			World.Plr[0].down = (float)stream.ReceiveNext();
			World.Plr[1].rmov_down = (bool)stream.ReceiveNext();
			World.Plr[1].rrot_down = (bool)stream.ReceiveNext();
			World.Plr[1].hold_down = (bool)stream.ReceiveNext();
			World.Plr[1].down_down = (bool)stream.ReceiveNext();	
			World.Plr[1].rmov = (float)stream.ReceiveNext();
			World.Plr[1].rrot = (float)stream.ReceiveNext();
			World.Plr[1].hold = (float)stream.ReceiveNext();
			World.Plr[1].down = (float)stream.ReceiveNext();
		}
	}*/

	[PunRPC]
	void SyncMyButton(int pnum, bool b1, bool b2, bool b3, bool b4, float f1, float f2, float f3, float f4) {
		World.Plr [pnum].rmov_down = b1;
		World.Plr [pnum].rrot_down = b2;
		World.Plr [pnum].hold_down = b3;
		World.Plr [pnum].down_down = b4;
		World.Plr [pnum].rmov = f1;
		World.Plr [pnum].rrot = f2;
		World.Plr [pnum].hold = f3;
		World.Plr [pnum].down = f4;                         
	}
}
