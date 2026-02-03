namespace intel8080.Signaling.Microcodes;
using Executing.Components;
using Signaling;

public partial class Microcode : MicrocodeRom
{
    protected byte opcode;
    
    protected byte zz_xxx_zzz;
    protected byte zz_zzz_xxx;
    protected byte zz_xxz_zzz;
    protected byte zz_zzx_xxx;
    
    protected Register decodedOperand;
    protected Register decodedSource;
    private Register[] decodedPair;
    protected Condition decodedCondition;

    protected void UpdateOpcode(byte ir)
    {
        opcode = ir;
        
        zz_xxx_zzz = (byte)((opcode >> 3) & 0x7);
        zz_zzz_xxx = (byte)(opcode & 0x7);
        zz_xxz_zzz = (byte)((opcode >> 4) & 0x3);
        zz_zzx_xxx = (byte)(opcode & 0xF);
        
        decodedOperand = EncodedRegisters[zz_xxx_zzz];
        decodedSource = EncodedRegisters[zz_zzz_xxx];
        decodedPair = EncodedPairs[zz_xxz_zzz];
        decodedCondition = (Condition)zz_xxx_zzz;
    }
    
    private static SignalSet[] NONE => [];

    private static SignalSet[] EA_IMM =>
    [   
        RAM_READ(Register.PC_L, Register.TMP),
        PAIR_INC(Register.PC_L),
    ];
    private static SignalSet[] EA_READ(Register source) =>
    [
        source is Register.RAM 
            ? RAM_READ(Register.HL_L, Register.TMP) : REG_MOVE(source, Register.TMP),
    ];
    private static SignalSet[] EA_WRITE(Register source) =>
    [
        source is Register.RAM 
            ? RAM_WRITE(Register.HL_L, Register.TMP) : REG_MOVE(Register.TMP, source),
    ];
    
    private static SignalSet[] LOAD_PAIR_IMM(Register[] pair) =>
    [
        RAM_READ(Register.PC_L, pair[0]),
        PAIR_INC(Register.PC_L),
        RAM_READ(Register.PC_L, pair[1]),
        PAIR_INC(Register.PC_L),
    ];

    private static SignalSet[] LOAD_OR_STORE(bool read, Register address, Register operand) =>
    [
        read ? RAM_READ(address, operand) : RAM_WRITE(address, operand),
    ];

    private static SignalSet[] REG_SWAP(Register first, Register second) =>
    [
        REG_MOVE(first, Register.TMP),
        REG_MOVE(second, first),
        REG_MOVE(Register.TMP, second),
    ];

    private static SignalSet[] JUMP_TO_PAIR(Register[] pair) =>
    [
        REG_MOVE(pair[0], Register.PC_L),
        REG_MOVE(pair[1], Register.PC_H),
    ];
}