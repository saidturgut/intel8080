namespace i8080_emulator.External;
using Executing;
using Devices;
using Firmware;

public class IO
{
    private readonly Disk Disk = new();
    private readonly Terminal Terminal = new();

    private readonly byte[] Ports = 
    [
        0x00, // CONSOLE STATUS (IN)
        0x01,  // CONSOLE INPUT (IN)
        0x02, // CONSOLE OUTPUT (OUT)
    ];

    public void Init(RAM RAM)
    {
        Disk.Init();
        Terminal.Init();
    }

    public void HostInput() => Terminal.HostInput();
    
    public void Read(TriStateBus aBusL, TriStateBus dBus)
    {
        dBus.Set(
            Ports[aBusL.Get()] switch
            {
                0x00 => Terminal.ReadStatus(),
                0x01 => Terminal.ReadData(),
                _ => throw new Exception("INVALID READ PORT")
            }
        );
    }

    public void Write(TriStateBus aBusL, TriStateBus dBus)
    {
        if(Ports[aBusL.Get()] != 0x02)
            throw new Exception("INVALID WRITE PORT");
        
        Terminal.WriteData(dBus.Get());
    }
}