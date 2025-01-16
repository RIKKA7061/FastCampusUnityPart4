
public class SendLetterBase
{
	public string url;
	public string PacketName;

	public SendLetterBase(string url, PACKET_NAME_TYPE packetName)
	{
		this.url = url;
		this.PacketName = packetName.ToString();
	}
}

public class ReceiveLetterBase
{
	public int ReturenCode; // Success, Fail

	public ReceiveLetterBase(int returenCode)
	{
		ReturenCode = returenCode;
	}
}