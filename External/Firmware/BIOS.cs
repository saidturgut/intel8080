namespace i8080_emulator.External.Firmware;

public partial class BIOS
{
    private const int offset = 0xE000;
    
    public void Load(RAM RAM)
    {
        RAM.LoadArray(0x0000 + offset, JUMP_TABLE);
        
        RAM.LoadArray(0x0100 + offset, BOOT);
        RAM.LoadArray(0x0105 + offset, WBOOT);
        
        RAM.LoadArray(0x0110 + offset, CONST);
        RAM.LoadArray(0x0118 + offset, CONIN);
        RAM.LoadArray(0x0125 + offset, CONOUT);

        RAM.LoadByte(0x012A + offset, 0xC9);
        RAM.LoadByte(0x012B + offset, 0xC9);
        RAM.LoadByte(0x012C + offset, 0xC9);
        RAM.LoadByte(0x012D + offset, 0xC9);
        
        RAM.LoadArray(0x0130 + offset, SELDSK);
        RAM.LoadByte(0x0134 + offset, 0xC9);
        RAM.LoadByte(0x0135 + offset, 0xC9);
        RAM.LoadArray(0x0136 + offset, SETDMA);
        RAM.LoadArray(0x013B + offset, READ);
        RAM.LoadArray(0x013E + offset, WRITE);
        RAM.LoadByte(0x0141 + offset, 0xC9);
        RAM.LoadArray(0x0144 + offset, SECTRAN);
        
        RAM.LoadArray(0x0150 + offset, DMAADDR);
    }
}
