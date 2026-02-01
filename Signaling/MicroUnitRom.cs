namespace intel8080.Signaling;
using Executing.Components;
using Executing.Computing;

public struct SignalSet()
{
    public MicroStep MicroStep = MicroStep.NONE;
    public Register Source = Register.NONE;
    public Register Operand = Register.NONE;
    public AluAction AluAction;
}

public enum MicroStep
{
    NONE, DECODE, HALT,
    REG_MOVE,
    RAM_READ, RAM_WRITE, 
    ALU_COMPUTE, 
    PAIR_INC, PAIR_DEC,
    IO_READ, IO_WRITE,
}

public struct AluAction()
{
    public Operation Operation = Operation.NONE;
    public Flag FlagMask = Flag.None;
    public bool UseCarry = false;
}

public enum Register
{
    PC_L, PC_H,
    SP_L, SP_H,
    HL_L, HL_H,
    WZ_L, WZ_H,
    
    C, B, E, D,
    A, IR ,TMP, PSW,
    NONE, RAM, 
}