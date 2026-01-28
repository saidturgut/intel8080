namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

// 10b AND 11b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded ALU(bool native) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[zz_zzz_xxx()],
        DataLatcher = Register.TMP,

        AluAction = new AluAction(
            EncodedCoreOperations[zz_xxx_zzz()],
            zz_xxx_zzz() != 0x7 ? Register.A : Register.NONE, // CMP, CMI
            FlagMasks[(byte)FlagMask.SZAPC], 
            zz_xxx_zzz() == 0x1 || zz_xxx_zzz() == 3), // ADC, SBB, ADI, SUI
        
        MicroCycles = [native ? MicroCycle.MOVE_LOAD : MicroCycle.MOVE_IMM, MicroCycle.ALU_EXECUTE],
    };

    protected static Decoded INR_DCR(bool inr) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[zz_xxx_zzz()],
        DataLatcher = Register.TMP,

        AluAction = new AluAction(
            inr ? Operation.INC : Operation.DEC,
            Register.TMP,
            FlagMasks[(byte)FlagMask.SZAP],
            false),

        MicroCycles = [MicroCycle.MOVE_LOAD, MicroCycle.ALU_EXECUTE, MicroCycle.MOVE_STORE],
    };

    protected static Decoded INX_DCX(bool inx) => new()
    {
        Pair = EncodedPairs[zz_xxz_zzz()],
        MicroCycles = [inx ? MicroCycle.PAIR_INC : MicroCycle.PAIR_DEC],
    };

    protected static Decoded BIT(byte type) => new()
    {
        AluAction = new AluAction(
            EncodedBitOperations[type],
            Register.A,
            type switch
            {
                0x4 => FlagMasks[(byte)FlagMask.SZAPC], // DAA
                0x5 => PswFlag.None, // CMA
                _ => PswFlag.Carry
            },
            true),
        MicroCycles = [MicroCycle.ALU_EXECUTE]
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