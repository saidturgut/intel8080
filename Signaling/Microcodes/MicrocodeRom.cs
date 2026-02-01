namespace intel8080.Signaling.Microcodes;
using Executing.Computing;
using Signaling;

public class MicrocodeRom
{
    protected static readonly Dictionary<byte, MicroStep> StateInstructions = new()
    {
        { 0x76, MicroStep.HALT }, // HALT

        { 0xF3, MicroStep.NONE }, // DI
        { 0xFB, MicroStep.NONE }, // EI
        
        { 0x00, MicroStep.NONE }, // NOP
        
        { 0x08, MicroStep.NONE }, // NOP
        { 0x10, MicroStep.NONE }, // NOP
        { 0x18, MicroStep.NONE }, // NOP
        { 0x20, MicroStep.NONE }, // NOP
        { 0x28, MicroStep.NONE }, // NOP
        { 0x30, MicroStep.NONE }, // NOP
        { 0x38, MicroStep.NONE }, // NOP
        { 0x40, MicroStep.NONE }, // NOP
        { 0x49, MicroStep.NONE }, // NOP
        { 0x52, MicroStep.NONE }, // NOP
        { 0x5B, MicroStep.NONE }, // NOP
        { 0x64, MicroStep.NONE }, // NOP
        { 0x6D, MicroStep.NONE }, // NOP
        { 0x7F, MicroStep.NONE }, // NOP
    };
    
    protected static SignalSet CHANGE_STATE(MicroStep state) => new()
        { MicroStep = state, };
    
    protected static SignalSet REG_MOVE(Register source, Register destination) => new() 
        {MicroStep = MicroStep.REG_MOVE, Source =  source, Operand = destination};
    
    protected static SignalSet RAM_READ(Register address, Register destination) => new() 
        {MicroStep = MicroStep.RAM_READ, Source =  address, Operand = destination};
    protected static SignalSet RAM_WRITE(Register address, Register data) => new() 
        {MicroStep = MicroStep.RAM_WRITE, Source =  address, Operand = data};

    protected static SignalSet ALU_COMPUTE(AluAction action, Register source) => new()
        { MicroStep = MicroStep.ALU_COMPUTE, AluAction = action, Source = source };

    protected static SignalSet PAIR_INC(Register source) => new()
        { MicroStep = MicroStep.PAIR_INC, Source = source };
    protected static SignalSet PAIR_DEC(Register source) => new() 
        {MicroStep = MicroStep.PAIR_DEC,  Source = source};

    protected static SignalSet IO_READ() => new()
        { MicroStep = MicroStep.IO_READ,};
    protected static SignalSet IO_WRITE() => new()
        { MicroStep = MicroStep.IO_WRITE,};

    protected static readonly Register[] EncodedRegisters =
    [
        Register.B, Register.C, Register.D, Register.E,
        Register.HL_H, Register.HL_L, Register.RAM, Register.A,
    ];

    protected static readonly Register[][] EncodedPairs =
    [
        [Register.C, Register.B],
        [Register.E, Register.D],
        [Register.HL_L, Register.HL_H],
        [Register.SP_L, Register.SP_H],
    ];
    
    protected static readonly Register[][] EncodedPairsPop =
    [
        [Register.C, Register.B],
        [Register.E, Register.D],
        [Register.HL_L, Register.HL_H],
        [Register.PSW, Register.A],
    ];
    
    protected static readonly Operation[] EncodedCoreOperations =
    [
        Operation.ADD, Operation.ADD,
        Operation.SUB, Operation.SUB,
        Operation.AND, Operation.XOR,
        Operation.OR, Operation.SUB,
        Operation.INC, Operation.DEC,
    ];
    
    protected static readonly Operation[] EncodedBitOperations =
    [
        Operation.RLC, Operation.RRC,
        Operation.RAL, Operation.RAR,
        Operation.DAA, Operation.CMA,
        Operation.STC, Operation.CMC,
    ];
    
    protected static readonly Flag[] FlagMasks =
    [
        Flag.None,
        Flag.Sign | Flag.Zero | Flag.Auxiliary | Flag.Parity,
        Flag.Sign | Flag.Zero | Flag.Auxiliary | Flag.Parity | Flag.Carry,
    ];
}

public enum RegisterPair
{
    BC, DC, HL, SP,
}

public enum AluOpcodes
{
    ADD, ADC, SUB, SBB, ANA, XRA, ORA, CMP,
}

public enum FlagMask
{
    NONE, SZAP, SZAPC, 
}
