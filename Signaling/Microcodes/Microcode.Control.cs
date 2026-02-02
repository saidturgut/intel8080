using intel8080.Executing.Components;
using intel8080.Executing.Computing;

namespace intel8080.Signaling.Microcodes;
using Signaling;

public partial class Microcode
{
    protected SignalSet[] JMP(bool permit) =>
    [
        ..LOAD_PAIR_IMM([Register.WZ_L, Register.WZ_H]),
        ..permit ? JUMP_TO_PAIR([Register.WZ_L, Register.WZ_H]) : NONE,
    ];

    protected SignalSet[] CALL(bool permit) =>
    [
        ..LOAD_PAIR_IMM([Register.WZ_L, Register.WZ_H]),
        ..PUSH(true),
        ..permit ? JUMP_TO_PAIR([Register.WZ_L, Register.WZ_H]) : NONE,
    ];

    protected SignalSet[] RST =>
    [
        ..PUSH(true),
        ALU_COMPUTE(new AluAction()
        {
            Operation = Operation.RST,
            FlagMask = Flag.None,
        }, Register.IR),
        ..JUMP_TO_PAIR([Register.TMP, Register.NONE]),
    ];

    protected SignalSet[] PUSH(bool pc) =>
    [
        PAIR_DEC(Register.SP_L),
        RAM_WRITE(Register.SP_L, pc ? Register.PC_H : EncodedPairs[zz_xxz_zzz][1]),
        PAIR_DEC(Register.SP_L),
        RAM_WRITE(Register.SP_L, pc ? Register.PC_L : EncodedPairs[zz_xxz_zzz][0]),
    ];

    protected SignalSet[] RET(bool permit) =>
    [
        ..permit ? POP(true) : IDLE,
    ];
    
    protected SignalSet[] POP(bool pc) =>
    [
        RAM_READ(Register.SP_L, pc ? Register.PC_L : EncodedPairsPop[zz_xxz_zzz][0]),
        PAIR_INC(Register.SP_L),
        RAM_READ(Register.SP_L, pc ? Register.PC_H : EncodedPairsPop[zz_xxz_zzz][1]),
        PAIR_INC(Register.SP_L),
    ];
}