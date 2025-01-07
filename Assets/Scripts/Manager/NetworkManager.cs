using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CertHandler : CertificateHandler
{
	protected override bool ValidateCertificate(byte[] certificateData)
	{
		return true;
	}
}

public class SendPacketBase
{
	public readonly string PacketName;

	public SendPacketBase(PACKET_NAME_TYPE packetName)
	{
		PacketName = packetName.ToString();
	}
}

public class ReceivePacketBase
{
	public readonly int ReturenCode; // Success, Fail

	public ReceivePacketBase(int returenCode)
	{
		ReturenCode = returenCode;
	}
}

public class ApplicationConfigSendPacket : SendPacketBase // <- parent
{
	// server -> as environment -> we CHOOSE different localhost address
	//
	// Ex...
	// enviroment: dev, stage, live
	// os type: Android, iOS
	// version: 1.0.0

	public int E_ENVIRONMENT_TYPE;
	public int E_OS_TYPE;
	public string AppVersion;

	public ApplicationConfigSendPacket(PACKET_NAME_TYPE packetName, 
		ENVIRONMENT_TYPE e_ENVIRONMENT_TYPE, 
		OS_TYPE e_OS_TYPE, 
		string appVersion) : base(packetName)
	{
		E_ENVIRONMENT_TYPE = (int)e_ENVIRONMENT_TYPE;
		E_OS_TYPE = (int)e_OS_TYPE;
		AppVersion = appVersion;
	}
}

public class ApplicationConfigReceivePacket : ReceivePacketBase
{
	public readonly string ApiUrl;

	public ApplicationConfigReceivePacket(int returnCode,string apiUrl) : base(returnCode)
	{
		ApiUrl = apiUrl;
	}
}

public class NetworkManager : ManagerBase
{
	private string apiUrl;

	private void Awake()
	{
		Dontdestroy<NetworkManager>();
	}

	public void SetInit(string apiUrl)
	{
		this.apiUrl = apiUrl;
	}

	public void SendPacket()
	{
		StartCoroutine(GetDataFromServer());	
	}

	IEnumerator GetDataFromServer()
	{
		ApplicationConfigSendPacket applicationConfigSendPacket
			= new ApplicationConfigSendPacket(PACKET_NAME_TYPE.ApplicationConfig,
			Config.E_ENVIRONMENT_TYPE,
			Config.E_OS_TYPE,
			Config.APP_VERSION);

		string packet = JsonUtility.ToJson(applicationConfigSendPacket);

		Debug.Log("[NetworkManager Send Packet] " + packet);

		using (UnityWebRequest request = UnityWebRequest.PostWwwForm(this.apiUrl, packet))
		{
			byte[] bytes = new System.Text.UTF8Encoding().GetBytes(packet);
			request.uploadHandler = new UploadHandlerRaw(bytes);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");

			// HTTP connect security Setting
			request.certificateHandler = new CertHandler();

			yield return request.SendWebRequest();

			//if(request.isNetworkError || request.isHttpError)
			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				// error case
				Debug.Log("Error: " + request.error);
			}
			else
			{
				// success case
				string jsonData = request.downloadHandler.text;
				Debug.Log("Received Data: " + jsonData);

				// json Data use..

			}
		}
	}
}
