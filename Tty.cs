namespace intel8080;

public class Tty
{
    private readonly Queue<byte> inputBuffer = new();

    public void Init()
    {
        //inputBuffer.Enqueue((byte)'R');
        //inputBuffer.Enqueue((byte)'O');
    }

    public void Reset()
        => inputBuffer.Clear();
    
    public void HostInput()
    {
        while (Console.KeyAvailable)
        {
            inputBuffer.Enqueue((byte)Console.ReadKey(intercept: true).KeyChar);
        }
    }

    public byte ReadStatus()
    {
        Environment.Exit(5);
        return inputBuffer.Count > 0 ? (byte)0xFF : (byte)0x00;
    }

    public byte ReadData()
    {
        Environment.Exit(4);
        return inputBuffer.Dequeue();;
    }

    public void WriteData(byte data)
    {
        Environment.Exit(3);
        Console.Write((char)data);
    }
}