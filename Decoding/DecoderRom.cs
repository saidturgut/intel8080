namespace i8080_emulator.Decoding;
using Signaling.Cycles;
using Signaling;

public class DecoderRom
{
    protected static readonly Register[] EncodedRegisters =
    [
        Register.B, Register.C, Register.D, Register.E,
        Register.HL_L, Register.HL_H, Register.RAM, Register.A,
    ];
}