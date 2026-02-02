namespace intel8080;
using Executing.Components;
using Testing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];
    
    public void Init()
    {
        if (false) HexDump.Run(Memory);
    }

    public void MemoryDump() => HexDump.Run(Memory);

    public void LoadByte(ushort address, byte data)
        => Memory[address] = data;
    
    public void LoadImage(ushort address, byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
            Memory[address + i] = data[i];
    }

    public byte Read(ushort address)
        => Memory[address];
    
    public void Write(ushort address, byte data)
        => Memory[address] = data;
}