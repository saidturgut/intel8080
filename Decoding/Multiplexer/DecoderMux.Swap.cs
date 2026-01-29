namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Multiplexer;
using Signaling;

// 11b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded XCHG() => new()
    {
        Queue = [Register.HL_L, Register.WZ_L, Register.E, Register.HL_L, Register.WZ_L, Register.E,
            Register.HL_H, Register.WZ_L, Register.D, Register.HL_H, Register.WZ_L, Register.D],
        MicroCycles = [..SwapPairs, ..SwapPairs, ..SwapPairs],
    };

    protected static Decoded XTHL() => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        Queue = [Register.HL_L, Register.HL_L, Register.SP_L, Register.HL_H, Register.HL_H],
        
        MicroCycles = 
        [
            MicroCycle.STORE_REG, MicroCycle.LOAD_PAIR, MicroCycle.LOAD_PAIR_TMP,
            MicroCycle.INC_PAIR, 
            MicroCycle.STORE_REG, MicroCycle.LOAD_PAIR, MicroCycle.LOAD_PAIR_TMP
        ]
    };

    protected static Decoded SPHL() => new()
    {
        Queue = [Register.HL_L, Register.SP_L, Register.HL_H, Register.SP_H],
        MicroCycles = [..SwapPairs]
    };

    protected static Decoded PCHL() => new()
    {
        Queue = [Register.HL_L, Register.PC_L, Register.HL_H, Register.PC_H],
        MicroCycles = [..SwapPairs]
    };

    private static readonly MicroCycle[] SwapPairs =
    [
        MicroCycle.STORE_PAIR_TMP, MicroCycle.LOAD_PAIR_TMP,
        MicroCycle.STORE_PAIR_TMP, MicroCycle.LOAD_PAIR_TMP,
    ];
}