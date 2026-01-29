using i8080_emulator.Testing;

namespace i8080_emulator;
using Executing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    private byte[] Rom =
    [
        0b11111001,
        0x76
    ];

    private Dictionary<ushort, byte> Dump = new();
    
    public void Init(bool hexDump)
    {
        /*Assembler.Run();
        LoadArray(0, File.ReadAllBytes("Test.bin"));*/

        LoadArray(0, Rom);
        
        LoadByte(0xA302, 0x40);
        LoadByte(0xA303, 0x16);
        LoadByte(0xA12, 0x76);
        LoadByte(0x3B2, 0x76);
        LoadByte(0x38, 0x76);
        
        if (hexDump)
            HexDump.Run(Memory);
    }
    
    public void LoadArray(ushort address, byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            Memory[i + address] = data[i];
        }    
    }

    public void LoadByte(int address, byte value)
        => Memory[address] = value;

    public void Read(TriStateBus aBusL, TriStateBus aBusH, TriStateBus dBus)
    {
        dBus.Set(Memory[Merge(aBusL.Get(), aBusH.Get())]);
    }
    
    public void Write(TriStateBus aBusL, TriStateBus aBusH, TriStateBus dBus)
    {
        Memory[Merge(aBusL.Get(), aBusH.Get())] = dBus.Get();
        Console.WriteLine($"RAM[{Hex(Merge(aBusL.Get(), aBusH.Get()))}]: {Hex(dBus.Get())}");
    }

    private ushort Merge(byte low, byte high)
        => (ushort)(low + (high << 8));
    
    private string Hex(ushort input)         
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}