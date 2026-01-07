namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
        // LXI H,3000h
        0x21, 0x00, 0x30,

        // MVI A,12h
        0x3E, 0x12,

        // MOV M,A
        0x77,

        // INX H
        0x23,

        // MVI A,34h
        0x3E, 0x34,

        // MOV M,A
        0x77,

        // MVI H,00h
        0x26, 0x00,

        // MVI L,00h
        0x2E, 0x00,

        // LHLD 3000h
        0x2A, 0x00, 0x30,

        // SHLD 4000h
        0x22, 0x00, 0x40,

        // HLT
        0x76
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