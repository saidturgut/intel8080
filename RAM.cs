namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();

    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    public void LoadArray(ushort address, byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
            Memory[i + address] = data[i];
    }

    public void LoadByte(int address, byte value)
    {
        Memory[address] = value;
    }
    
    public void Read(TriStateBus aBusH, TriStateBus aBusL, TriStateBus dBus)
    {
        dBus.Set(Memory[Merge(aBusH.Get(), aBusL.Get())]);
    }

    public void Write(TriStateBus aBusH, TriStateBus aBusL, TriStateBus dBus)
    {
        Memory[Merge(aBusH.Get(), aBusL.Get())] = dBus.Get();
        MemoryDump[Merge(aBusH.Get(), aBusL.Get())] = Memory[Merge(aBusH.Get(), aBusL.Get())];
    }

    private ushort Merge(byte high, byte low)
    {
        return  (ushort)((high << 8) + low);
    }

}