//public class PacketName
//{
//	public const string ApplicationConfig = "ApplicationConfig";
//}

public enum PACKET_NAME_TYPE
{
	ApplicationConfig,
}

public enum SCENE_TYPE
{
	Init,
	Loading,
	Lobby,
	InGame
}

public enum ENVIRONMENT_TYPE
{
	Dev = 1, // now Developing(GaeBwal)...
	Stage = 2, // Same Open Environment // Before Open -> test it
	Live = 3, // Store Ver // Production version
}

public enum OS_TYPE
{
	Android = 1,
	iOS = 2,
}