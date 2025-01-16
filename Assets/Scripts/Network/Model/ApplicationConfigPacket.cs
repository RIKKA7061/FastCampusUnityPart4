public class ApplicationConfigSendPacket : SendLetterBase // <- parent
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

	public ApplicationConfigSendPacket(string url, 
		PACKET_NAME_TYPE packetName,
		ENVIRONMENT_TYPE e_ENVIRONMENT_TYPE,
		OS_TYPE e_OS_TYPE,
		string appVersion) : base(url, packetName)
	{
		E_ENVIRONMENT_TYPE = (int)e_ENVIRONMENT_TYPE;
		E_OS_TYPE = (int)e_OS_TYPE;
		AppVersion = appVersion;
	}
}

public class ApplicationConfigReceiveLetter : ReceiveLetterBase
{
	public readonly string apiURL;

	public ApplicationConfigReceiveLetter(int returnCode, string apiUrl) : base(returnCode)
	{
		apiURL = apiUrl;
	}
}