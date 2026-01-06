namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
        0x3E, 0xFF,
        0x87,             // ADD A (sets flags)
        0x01, 0x00, 0x50,
        0x02,             // STAX B
        0x0A,             // LDAX B
        0x76              //              // HLT
    ];
    
    public void Init()
    {
        for (int i = 0; i < ROM.Length; i++)
            Memory[i] = ROM[i];
    }
    
    public void Read(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        dBus.Set(Memory[Merge(aBus_H.Get(), aBus_L.Get())]);
    }

    public void Write(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        Memory[Merge(aBus_H.Get(), aBus_L.Get())] = dBus.Get();
        MemoryDump[Merge(aBus_H.Get(), aBus_L.Get())] = Memory[Merge(aBus_H.Get(), aBus_L.Get())];
    }

    private ushort Merge(byte high, byte low)
    {
        return  (ushort)((high << 8) + low);
    }
}