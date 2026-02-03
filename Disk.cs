namespace intel8080;

public class Disk
{
    private readonly byte[] diskImage = File.ReadAllBytes("cpm22-1.dsk");
    
    private byte currentDrive = 0;
    private byte currentTrack = 0;
    private byte currentSector = 1;
    private ushort dmaAddress = 0x80;
    private byte status = 0;
    
    public void Reset()
    {
        currentDrive = 0;
        currentTrack = 0;
        currentSector = 1;
        dmaAddress = 0;
        status = 0;
    }

    public void SetDrive(byte drive)
        => currentDrive = drive;
    public void SetTrack(byte track)
        => currentTrack = track;
    public void SetSector(byte sector)
        => currentSector = sector;
    public void SetDmaLow(byte low)
        => dmaAddress = (ushort)((dmaAddress & 0xFF00) | low);
    public void SetDmaHigh(byte high)
        => dmaAddress = (ushort)((dmaAddress & 0x00FF) | (high << 8));
    public byte GetStatus() 
        => status;
    
    public void ExecuteCommand(Ram ram, byte command)
    {
        int offset = Offset();

        status = 0;
        
        switch (command)
        {
            case 0:
            {
                for (byte i = 0; i < 128; i++)
                    ram.Write((ushort)(dmaAddress + i), diskImage[offset + i]);
                break;
            }
            case 1:
            {
                for (byte i = 0; i < 128; i++)
                    diskImage[offset + i] = ram.Read((ushort)(dmaAddress + i));
                break;
            }
            default:
                status = 0x1;
                break;
        }
    }
    
    private int Offset()
        => (currentTrack * 26 + (currentSector - 1)) * 128;
}