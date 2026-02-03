namespace intel8080;

public class Tty
{
    private readonly Queue<byte> inputBuffer = new();

    public void HostInput()
    {
        while (Console.KeyAvailable)
        {
            inputBuffer.Enqueue((byte)Console.ReadKey(intercept: true).KeyChar);
        }
    }

    public byte ReadStatus()
        => inputBuffer.Count > 0 ? (byte)0xFF : (byte)0x00;

    public byte ReadData()
        => inputBuffer.Dequeue();

    public void WriteData(byte data)
        => Console.Write((char)data);
}