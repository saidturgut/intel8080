namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;

public partial class DecoderMux : DecoderRom
{
    protected static readonly Dictionary<byte, MicroCycle> FixedOpcodes = new()
    {
        { 0x00, MicroCycle.EMPTY },
        { 0x76, MicroCycle.HALT },
    };
    
    protected static Decoded FIXED() => new()
        { MicroCycles = [FixedOpcodes[opcode]], };

    public static Decoded FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        MicroCycles = [MicroCycle.FETCH, MicroCycle.DECODE],
    };
}