namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

// 00b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded BIT(byte type) => new()
    {
        AluAction = new AluAction
        {
            Operation = EncodedBitOperations[type],
            FlagMask = type switch
            {
                0x4 => FlagMasks[(byte)FlagMask.SZAPC], // DAA
                0x5 => PswFlag.None, // CMA
                _ => PswFlag.Carry
            },
            UseCarry = true,
        },

        MicroCycles = [MicroCycle.EXECUTE_ALU]
    };
    
    protected static Decoded DAD() => new()
    {
        Queue = [Register.WZ_L, EncodedPairsSp[zz_xxz_zzz()][0], Register.HL_L, Register.HL_L, 
            EncodedPairsSp[zz_xxz_zzz()][1], Register.HL_H, Register.HL_H, Register.WZ_L],

        DataDriver = Register.A,
        
        AluAction = new AluAction
        {
            Operation = Operation.ADD,
            FlagMask = PswFlag.Carry,
            UseCarry = true,
        },
        
        MicroCycles = 
        [
            MicroCycle.CLEAR_CARRY, MicroCycle.MOVE_PAIR_LOAD, // Z <- A, C = 0
            MicroCycle.MOVE_PAIR_TO_TMP, MicroCycle.MOVE_PAIR_STORE, // TMP <- LOW, A <- L
            MicroCycle.EXECUTE_ALU, MicroCycle.MOVE_PAIR_LOAD, // TMP <- TMP + A, L <- TMP
            MicroCycle.MOVE_PAIR_TO_TMP, MicroCycle.MOVE_PAIR_STORE, // TMP <- HIGH, A <- H
            MicroCycle.EXECUTE_ALU, MicroCycle.MOVE_PAIR_LOAD, // TMP <- TMP + A, H <- TMP
            MicroCycle.MOVE_PAIR_STORE, // A <- Z
        ],
    };
    
    private static readonly Operation[] EncodedBitOperations =
    [
        Operation.RLC, Operation.RRC,
        Operation.RAL, Operation.RAR,
        Operation.DAA, Operation.CMA,
        Operation.STC, Operation.CMC,
    ];
}