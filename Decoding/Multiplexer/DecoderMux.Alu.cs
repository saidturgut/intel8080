namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

// 10b AND 11b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded ALU(bool native, byte type) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[zz_zzz_xxx()],
        DataLatcher = Register.TMP,

        AluAction = new AluAction
        {
            Operation = EncodedCoreOperations[type],
            FlagMask = FlagMasks[(byte)FlagMask.SZAPC],
            UseCarry = type is 0x1 or 3, // ADC, SBB, ADI, SUI
            LatchPermit = type != 0x7,
        },
        
        MicroCycles = [native ? MicroCycle.MOVE_LOAD : MicroCycle.MOVE_IMM, MicroCycle.EXECUTE_ALU],
    };

    protected static Decoded DAD() => new()
    {
        Queue = [Register.WZ_L, EncodedPairs[zz_xxz_zzz()][0], Register.HL_L, Register.HL_L, 
            EncodedPairs[zz_xxz_zzz()][1], Register.HL_H, Register.HL_H, Register.WZ_L],

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
    
    protected static Decoded INR_DCR(bool inr) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = Register.A,
        DataLatcher = EncodedRegisters[zz_xxx_zzz()],

        Queue = [Register.WZ_L, Register.WZ_L],
        
        AluAction = new AluAction
        {
            Operation = inr ? Operation.INC : Operation.DEC,
            FlagMask = FlagMasks[(byte)FlagMask.SZAP],
            UseCarry = false,
        },
            
        MicroCycles = [MicroCycle.MOVE_PAIR_LOAD, MicroCycle.MOVE_STORE, MicroCycle.EXECUTE_ALU, 
            MicroCycle.MOVE_LOAD, MicroCycle.MOVE_PAIR_STORE],
    };

    protected static Decoded INX_DCX(bool inx) => new()
    {
        Queue = EncodedPairs[zz_xxz_zzz()],
        MicroCycles = [inx ? MicroCycle.INC_PAIR : MicroCycle.DEC_PAIR],
    };

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
    
    private static readonly Operation[] EncodedBitOperations =
    [
        Operation.RLC, Operation.RRC,
        Operation.RAL, Operation.RAR,
        Operation.DAA, Operation.CMA,
        Operation.STC, Operation.CMC,
    ];

    private static readonly Operation[] EncodedCoreOperations =
    [
        Operation.ADD, Operation.ADD,
        Operation.SUB, Operation.SUB,
        Operation.AND, Operation.XOR,
        Operation.OR, Operation.SUB,
        Operation.INC, Operation.DEC,
    ];
    
    public enum FlagMask
    {
        NONE, SZAP, SZAPC, 
    }
    
    private static readonly PswFlag[] FlagMasks =
    [
        PswFlag.None,
        PswFlag.Sign | PswFlag.Zero | PswFlag.Auxiliary | PswFlag.Parity,
        PswFlag.Sign | PswFlag.Zero | PswFlag.Auxiliary | PswFlag.Parity | PswFlag.Carry,
    ];
}