namespace intel8080;

public class Disk
{
    private readonly byte[] diskImage = File.ReadAllBytes("cpm22-1.dsk");
    
    private byte currentTrack;
    private byte currentSector;
    private ushort dmaAddress;
    
    public void Init() { }

    public void Reset()
    {
        currentTrack = 0;
        currentSector = 1;
        dmaAddress = 0x80;
    }
    
    public void SetTrack(byte track)
        => currentTrack = track;

    public void SetSector(byte sector)
        => currentSector = sector;

    public void SetDma(ushort dma)
        => dmaAddress = dma;
    
    public byte Read(Ram ram)
    {
        int offset = Offset();

        if (offset < 0 || offset + 128 > diskImage.Length)
            throw new Exception("READ ONLY!!");
        
        for (byte i = 0; i < 128; i++)
        {
            ram.Write((ushort)(dmaAddress + i), diskImage[offset + i]);
        }

        return 0x00;
    }

    public byte Write(Ram ram)
    {
        int offset = Offset();
        
        for (byte i = 0; i < 128; i++)
        {
            diskImage[offset + i] = ram.Read((ushort)(dmaAddress + i));
        }
        
        return 0x00;
    }

    private int Offset()
        => (currentTrack * 26 + (currentSector - 1)) * 128;
}