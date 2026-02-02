namespace intel8080.Executing;
using Signaling;

public partial class DataPath
{
    private readonly Tty Tty = new();
    private readonly Disk Disk = new();
    
    private string DEBUG_NAME;
    
    private void Input()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 2: Reg(Register.A).Set(Tty.ReadStatus()); DEBUG_NAME = "CONST"; break;
            case 3: Reg(Register.A).Set(Tty.ReadData()); DEBUG_NAME = "CONIN"; break;
            case 7: Reg(Register.A).Set(0x1A); DEBUG_NAME = "READER"; break;
            
            case 9: PairSet(Register.HL_L, Rom.DPHB << 8); PairSet(Register.E, Rom.DPHB << 8); DEBUG_NAME = "SELDSK"; break; // SELDSK
            
            case 13: Reg(Register.A).Set(Disk.Read(Ram)); DEBUG_NAME = "READ"; break; // READ
            case 15: Reg(Register.A).Set(0x0); DEBUG_NAME = "LISTST"; break;
            
            case 16: PairSet(Register.HL_L, (ushort)(PairGet(Register.C) + 1)); DEBUG_NAME = "SECTRAN"; break;
            default: throw new Exception($"INVALID INPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
        DEBUG_NAME = $"BIOS: {DEBUG_NAME}";
    }

    private void Output()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 0: Boot(); DEBUG_NAME = "BOOT"; break;
            case 1: Tty.Reset(); Disk.Reset(); DEBUG_NAME = "WBOOT"; break;
            case 4: Tty.WriteData(Reg(Register.A).Get()); DEBUG_NAME = "CONOUT"; break;
            case 5: DEBUG_NAME = "LIST"; break;
            case 6: DEBUG_NAME = "PUNCH"; break;
            case 8: DEBUG_NAME = "HOME"; break;
            case 10: Disk.SetTrack(Reg(Register.C).Get()); DEBUG_NAME = "SETTRK"; break;
            case 11: Disk.SetSector(Reg(Register.C).Get()); DEBUG_NAME = "SETSEC"; break;
            case 12: Disk.SetDma(PairGet(Register.HL_L)); DEBUG_NAME = "SETDMA"; break;
            case 14: Reg(Register.A).Set(Disk.Write(Ram)); DEBUG_NAME = "WRITE"; break;
            default: throw new Exception($"INVALID OUTPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
        DEBUG_NAME = $"BIOS: {DEBUG_NAME}";
    }

    private void Boot()
    {
        ushort baseAddress = Rom.CCPB << 8;
        int sectorsRemaining = 50;

        byte track = 0;
        byte sector = 2;

        while (sectorsRemaining --> 0)
        {
            Disk.SetTrack(track);
            Disk.SetSector(sector);
            Disk.SetDma(baseAddress);
            Disk.Read(Ram);

            baseAddress += 128;

            sector++;
            if (sector == 27)
            {
                sector = 1;
                track++;
            }
            
            if(baseAddress >= 0xFA00)
                break;
        }
        //MemoryDump();
    }
}