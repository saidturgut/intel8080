namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    protected static Decoded decoded;

    protected static byte queueIndex;
    
    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        IDLE, FETCH, DECODE, HALT, 
        MOVE_LOAD, MOVE_STORE, MOVE_IMM, 
        MOVE_PAIR_IMM, MOVE_PAIR_LOAD, MOVE_PAIR_STORE,
        MOVE_PAIR_TO_TMP, MOVE_TMP_TO_PAIR, 
        MOVE_CYCLE_LATCH, MOVE_ZERO,
        EXECUTE_ALU, CLEAR_CARRY, INC_PAIR, DEC_PAIR
    ];
}

public enum MicroCycle
{
    IDLE, FETCH, DECODE, HALT, 
    MOVE_LOAD, MOVE_STORE, MOVE_IMM, 
    MOVE_PAIR_IMM, MOVE_PAIR_LOAD, MOVE_PAIR_STORE,
    MOVE_PAIR_TO_TMP, MOVE_TMP_TO_PAIR,
    MOVE_CYCLE_LATCH, MOVE_ZERO,
    EXECUTE_ALU, CLEAR_CARRY, INC_PAIR, DEC_PAIR
}

public enum State
{
    FETCH, DECODE, EXECUTE, HALT
}
