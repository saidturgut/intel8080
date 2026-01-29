namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Multiplexer;
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
        
        MicroCycles = [native ? MicroCycle.STORE_REG : MicroCycle.IMM_TMP, MicroCycle.COMPUTE_ALU],
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
            
        MicroCycles = [MicroCycle.STORE_PAIR, MicroCycle.LOAD_REG, MicroCycle.COMPUTE_ALU, 
            MicroCycle.STORE_REG, MicroCycle.LOAD_PAIR],
    };

    protected static Decoded INX_DCX(bool inx) => new()
    {
        Queue = EncodedPairsSp[zz_xxz_zzz()],
        MicroCycles = [inx ? MicroCycle.INC_PAIR : MicroCycle.DEC_PAIR],
    };
    
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