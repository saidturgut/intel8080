namespace i8080_emulator.Signaling;
using Executing;

public struct SignalSet()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public AluAction? AluAction = null;
    public IncAction IncAction = IncAction.NONE;
    public Register DataLatcher = Register.NONE;
}

public struct AluAction(Operation operation, FlagMask flagMask, bool carryIn)
{
    public Operation Operation = Operation.NONE;
    public FlagMask FlagMask = FlagMask.NONE;
    public bool UseCarry = false;
}

public enum IncAction
{
    NONE, INC, DEC,
}

public enum Operation
{
    NONE,
}

public enum FlagMask
{
    NONE, ALL,
}

public enum Register
{
    NONE = -1,
    PC_L, PC_H,
    SP_L, SP_H,
    HL_L, HL_H,
    WZ_L, WZ_H,
    
    A, B, C, D, E,
    IR ,TMP, PSW,
    RAM,
}