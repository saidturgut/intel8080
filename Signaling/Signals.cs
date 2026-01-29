namespace i8080_emulator.Signaling;
using Executing.Computing;
using Cycles;

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

public struct AluAction()
{
    public Operation Operation = Operation.NONE;
    public PswFlag FlagMask = PswFlag.None;
    public bool UseCarry = false;
    public bool LatchPermit = false;
}

public enum IncAction
{
    NONE, INC, DEC,
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