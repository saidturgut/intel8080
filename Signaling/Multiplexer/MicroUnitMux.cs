namespace i8080_emulator.Signaling.Multiplexer;
using Decoding;

public partial class MicroUnitMux
{
    protected static Decoded decoded;

    protected static byte queueIndex;
    
    protected static readonly Func<SignalSet>[] MicroCycles =
    [
        IDLE, FETCH, DECODE, HALT, IO,
    
        STORE_REG, LOAD_REG, IMM_TMP,
        COMPUTE_ALU, ZERO_CARRY, ZERO_REG,  

        IMM_PAIR, STORE_PAIR, LOAD_PAIR,
        STORE_PAIR_TMP, LOAD_PAIR_TMP, 
        LOAD_PAIR_ENCODE, INC_PAIR, DEC_PAIR,
    ];
}

public enum MicroCycle
{
    IDLE, FETCH, DECODE, HALT, IO,
    
    STORE_REG, LOAD_REG, IMM_TMP,
    COMPUTE_ALU, ZERO_CARRY, ZERO_REG,  

    IMM_PAIR, STORE_PAIR, LOAD_PAIR,
    STORE_PAIR_TMP, LOAD_PAIR_TMP, 
    LOAD_PAIR_ENCODE, INC_PAIR, DEC_PAIR,
}

public enum State
{
    FETCH, DECODE, EXECUTE, HALT
}
