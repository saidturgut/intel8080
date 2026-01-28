namespace i8080_emulator.Signaling;
using Executing.Computing;
using Executing;

public struct SignalSet()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public AluAction? AluAction = null;
    public IncAction IncAction = IncAction.NONE;
    public Register DataLatcher = Register.NONE;
    
    public State State = State.EXECUTE;
    public bool Index = false;
}

public struct AluAction(Operation operation, PswFlag flagMask, bool useCarry)
{
    public Operation Operation = operation;
    public PswFlag FlagMask = flagMask;
    public bool UseCarry = useCarry;
}

public enum IncAction
{
    NONE, INC, DEC,
}

public enum Register
{
    NONE = -1,
    PC_L, PC_H,
    SP_L, SP_H,
    HL_L, HL_H,
    WZ_L, WZ_H,
    
    C, B, E, D,
    A, IR ,TMP, PSW,
    RAM,
}