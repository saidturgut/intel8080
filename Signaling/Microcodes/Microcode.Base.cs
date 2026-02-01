namespace intel8080.Signaling.Microcodes;
using Signaling;

public partial class Microcode
{
    protected SignalSet[] IDLE =>
        [CHANGE_STATE(MicroStep.NONE)];

    public SignalSet[] FETCH =>
    [
        RAM_READ(Register.PC_L, Register.IR),
        PAIR_INC(Register.PC_L),
        CHANGE_STATE(MicroStep.DECODE),
    ];

    protected SignalSet[] IO(bool input) =>
    [
        ..EA_IMM,
        input ? IO_READ() : IO_WRITE(),
    ];
}