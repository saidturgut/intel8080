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

    protected static Decoded FIXED(byte ir) => new()
        { MicroCycles = [FixedOpcodes[ir]], };

    public static Decoded FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        MicroCycles =
        [
            MicroCycle.FETCH, MicroCycle.ADDR_INC,
            MicroCycle.DECODE
        ],
    };
}