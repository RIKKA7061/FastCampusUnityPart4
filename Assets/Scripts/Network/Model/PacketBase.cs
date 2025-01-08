
public class SendPacketBase
{
	public string PacketName;

	public SendPacketBase(PACKET_NAME_TYPE packetName)
	{
		PacketName = packetName.ToString();
	}
}

public class ReceivePacketBase
{
	public int ReturenCode; // Success, Fail

	public ReceivePacketBase(int returenCode)
	{
		ReturenCode = returenCode;
	}
}