namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    protected static Decoded decoded;

    protected static byte pairIndex;

    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        EMPTY, FETCH, DECODE, HALT,
        MOVE_LOAD, MOVE_STORE, MOVE_IMM,
        MOVE_PAIR_IMM, MOVE_PAIR_LOAD, MOVE_PAIR_STORE,
        MOVE_PAIR_TMP_LOAD, MOVE_PAIR_TMP_STORE,
    ];
}

public enum MicroCycle
{
    EMPTY, FETCH, DECODE, HALT, 
    MOVE_LOAD, MOVE_STORE, MOVE_IMM, 
    MOVE_PAIR_IMM, MOVE_PAIR_LOAD, MOVE_PAIR_STORE,
    MOVE_PAIR_TMP_LOAD, MOVE_PAIR_TMP_STORE,
}