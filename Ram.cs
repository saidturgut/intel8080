using i8080_emulator.Testing;

namespace i8080_emulator;
using Executing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x1000];

    public void Init(bool hexDump)
    {
        Assembler.Run();
        LoadArray(0, File.ReadAllBytes("Test.bin"));

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
        => dBus.Set(Memory[Merge(aBusL.Get(), aBusH.Get())]);

    public void Write(TriStateBus aBusL, TriStateBus aBusH, TriStateBus dBus)
        => Memory[Merge(aBusL.Get(), aBusH.Get())] = dBus.Get();

    private ushort Merge(byte low, byte high)
        => (ushort)(low + (high << 8));
}