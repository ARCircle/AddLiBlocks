using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhotonManager : Photon.PunBehaviour
{
	Text mes;

	void Start()
	{
		// Photonネットワークの設定を行う
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.sendRate = 30;
		mes = GameObject.Find ("Text").GetComponent<Text> ();
	}

	// 「ロビー」に接続した際に呼ばれるコールバック
	public override void OnJoinedLobby()
	{
		Debug.Log("OnJoinedLobby");
		PhotonNetwork.JoinRandomRoom();
	}

	// いずれかの「ルーム」への接続に失敗した際のコールバック
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("OnPhotonRandomJoinFailed");

		// ルームを作成（今回の実装では、失敗＝マスタークライアントなし、として「ルーム」を作成）
		World.connection = true;
		World.connectnum = 0;
		PhotonNetwork.CreateRoom(null);
	}

	// Photonサーバに接続した際のコールバック
	public override void OnConnectedToPhoton()
	{
		Debug.Log("OnConnectedToPhoton");
	}

	// マスタークライアントに接続した際のコールバック
	public override void OnConnectedToMaster()
	{
		Debug.Log("OnConnectedToMaster");
		World.connection = true;
		World.connectnum = 1;
		PhotonNetwork.JoinRandomRoom();
	}

	// いずれかの「ルーム」に接続した際のコールバック
	public override void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom");
		AudioSource audios = mes.GetComponent<AudioSource> ();
		audios.PlayOneShot (audios.clip);
		StartCoroutine("LoadSceneAndWait");
	}

	// 現在の接続状況を表示（デバッグ目的）
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	//時間経過で遷移
	IEnumerator LoadSceneAndWait() {		
		float start = Time.realtimeSinceStartup;
		AsyncOperation ope = SceneManager.LoadSceneAsync("Setting_C");
		ope.allowSceneActivation = false;

		while (Time.realtimeSinceStartup - start < 1) {
			yield return null;
		}
		ope.allowSceneActivation = true;
	}
}