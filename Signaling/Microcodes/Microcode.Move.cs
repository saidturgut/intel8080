namespace intel8080.Signaling.Microcodes;
using Signaling;

public partial class Microcode
{
    protected SignalSet[] MOV() =>
    [
        (decodedSource, decodedOperand) switch
        {
            (Register.RAM, _) => RAM_READ(Register.HL_L, decodedOperand),
            (_, Register.RAM) => RAM_WRITE(Register.HL_L, decodedSource),
            _ => REG_MOVE(decodedSource, decodedOperand),
        }
    ];

    protected SignalSet[] MVI() =>
    [
        ..EA_IMM,
        ..EA_WRITE(decodedOperand),
    ];

    protected SignalSet[] LXI =>
    [
        ..LOAD_PAIR_IMM(decodedPair),
    ];

    protected SignalSet[] LDA_STA(bool lda) =>
    [
        ..LOAD_PAIR_IMM([Register.WZ_L, Register.WZ_H]),
        ..LOAD_OR_STORE(lda, Register.WZ_L, Register.A),
    ];

    protected SignalSet[] LDAX_STAX(bool ldax) =>
    [
        ..LOAD_OR_STORE(ldax, decodedPair[0], Register.A),
    ];
    
    protected SignalSet[] LHLD_SHLD(bool lhld) =>
    [
        ..LOAD_PAIR_IMM([Register.WZ_L, Register.WZ_H]),
        ..LOAD_OR_STORE(lhld, Register.WZ_L, Register.HL_L),
        PAIR_INC(Register.WZ_L),
        ..LOAD_OR_STORE(lhld, Register.WZ_L, Register.HL_H),
    ];
    
    protected SignalSet[] XTHL =>
    [
        RAM_READ(Register.SP_L, Register.TMP),
        RAM_WRITE(Register.SP_L, Register.HL_L),
        REG_MOVE(Register.TMP, Register.HL_L),
        PAIR_INC(Register.SP_L),
        
        RAM_READ(Register.SP_L, Register.TMP),
        RAM_WRITE(Register.SP_L, Register.HL_H),
        REG_MOVE(Register.TMP, Register.HL_H),
        PAIR_DEC(Register.SP_L),
    ];
    protected SignalSet[] XCHG =>
    [
        ..REG_SWAP(Register.E, Register.HL_L),
        ..REG_SWAP(Register.D, Register.HL_H),
    ];
    protected SignalSet[] SPHL =>
    [
        REG_MOVE(Register.HL_L, Register.SP_L),
        REG_MOVE(Register.HL_H, Register.SP_H),
    ];
    protected SignalSet[] PCHL =>
    [
        ..JUMP_TO_PAIR([Register.HL_L, Register.HL_H]),
    ];
}