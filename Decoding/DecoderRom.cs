namespace i8080_emulator.Decoding;
using Signaling.Cycles;
using Signaling;

public class DecoderRom
{
    protected static byte opcode;
    
    protected static readonly Register[] EncodedRegisters =
    [
        Register.B, Register.C, Register.D, Register.E,
        Register.HL_L, Register.HL_H, Register.RAM, Register.A,
    ];

    protected static readonly Register[][] EncodedPairs =
    [
        [Register.C, Register.B],
        [Register.E, Register.D],
        [Register.HL_L, Register.HL_H],
        [Register.SP_L, Register.SP_H],
    ];

    protected static byte zz_zzz_xxx()
        => (byte)(opcode & 0x7);
    protected static byte zz_xxx_zzz() 
        => (byte)((opcode >> 3) & 0x7);
    protected static byte zz_xxz_zzz() 
        => (byte)((opcode >> 4) & 0x3);
    protected static byte zz_zzx_xxx() 
        => (byte)(opcode & 0xF);
    
}