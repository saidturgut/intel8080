using i8080_emulator.Testing;

namespace i8080_emulator;
using Executing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x150];

    private byte[] Rom =
    [
        0x37,         // STC
        0x3F,         // CMC

        0x07,         // RLC
        0x17,         // RAL
        0x0F,         // RRC
        0x1F,         // RAR

        0x2F,         // CMA

        0x37,         // STC
        0x3F,         // CMC

        0x27,         // DAA

        0x76          // HLT
    ];

    private Dictionary<ushort, byte> Dump = new();
    
    public void Init(bool hexDump)
    {
        /*Assembler.Run();
        LoadArray(0, File.ReadAllBytes("Test.bin"));*/

        LoadArray(0, Rom);
        
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