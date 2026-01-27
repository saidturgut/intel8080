namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    protected static Decoded decoded;
    
    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY, HALT, DECODE,
        FETCH, ADDR_INC,
        REG_TO_REG,
    ];
}

public enum MicroCycle
{
    EMPTY, HALT, DECODE,
    FETCH, ADDR_INC,
    REG_TO_REG,
}