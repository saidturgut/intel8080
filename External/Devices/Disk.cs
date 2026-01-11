namespace i8080_emulator.External.Devices;

public class Disk
{
    private byte[] DiskImage = [];

    private int currentTrack = 0;
    private int currentSector = 1;
    private ushort currentDMA = 0x0080;
    
    private const int TRACKS = 153;
    private const int SECTOR_PER_TRACK = 26;
    private const int SECTOR_SIZE = 128;
    
    public void Init()
    {
        DiskImage = File.ReadAllBytes("CPM14-24K-E000-DD.DSK");
    }
    
    public byte BIOS_SELDSK(ushort BC)
    {
        return 0;
    }
    
    public void BIOS_SETTRK(ushort BC)
    {
        currentTrack = BC;
    }
    
    public void BIOS_SETSEC(ushort BC)
    {
        currentSector = BC;
    }
    
    public void BIOS_SETDMA(ushort BC)
    {
        currentDMA = BC;
    }
    
    public byte BIOS_READ(RAM RAM)
    {
        return 0;
    }
    
    public byte BIOS_WRITE()
    {
        return 1;
    }
}