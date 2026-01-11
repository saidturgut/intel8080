using i8080_emulator.External.Firmware;

namespace i8080_emulator;

public class ROM
{
    // 0x0000
    private static readonly byte[] VECTORS =
        [0xC3, 0x00, 0xE0,];

    private readonly BIOS BIOS = new (); 
    
    public void Boot(RAM RAM)
    {
        RAM.LoadArray(0x0000, VECTORS);

        BIOS.Load(RAM);
    }
}

