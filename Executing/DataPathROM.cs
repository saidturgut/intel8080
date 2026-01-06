namespace i8080_emulator.Executing;
using Signaling;

public class DataPathROM
{
    protected readonly Dictionary<Register, RegisterObject> Registers = new()
    {
        { Register.PC_H, new RegisterObject() }, { Register.PC_L, new RegisterObject() }, // PROGRAM COUNTER
        { Register.IR, new RegisterObject() }, // INSTRUCTION REGISTER
        { Register.SP_H, new RegisterObject() }, { Register.SP_L, new RegisterObject() }, // STACK POINTER
        { Register.A, new RegisterObject() }, // ACCUMULATOR
        { Register.B, new RegisterObject() }, { Register.C, new RegisterObject() }, // B & C PAIR
        { Register.D, new RegisterObject() }, { Register.E, new RegisterObject() }, // D & E PAIR
        { Register.TMP, new RegisterObject() }, // TEMP REGISTER
        { Register.HL_H, new RegisterObject() }, { Register.HL_L, new RegisterObject() }, // ADDRESS POINTER
    };
    
    protected Dictionary<Register, RegisterObject> DataDrivers = new();
    protected Dictionary<Register, RegisterObject> DataLatchers = new();
    
    private Dictionary<RP, RegisterObject[]> RegisterPairs = new();
    protected Dictionary<SideEffect, IncrementPair> PairIncrements = new();
    
    public virtual void Init()
    {
        DataDrivers = new()
        {
            { Register.TMP , Registers[Register.TMP] },
            { Register.A , Registers[Register.A] },
            { Register.B , Registers[Register.B] }, { Register.C , Registers[Register.C] },
            { Register.D , Registers[Register.D] }, { Register.E , Registers[Register.E] },
            { Register.HL_H , Registers[Register.HL_H] }, { Register.HL_L , Registers[Register.HL_L] },
        };
        
        DataLatchers = new()
        {
            { Register.TMP , Registers[Register.TMP] }, { Register.IR , Registers[Register.IR] },
            { Register.A , Registers[Register.A] },
            { Register.B , Registers[Register.B] }, { Register.C , Registers[Register.C] },
            { Register.D , Registers[Register.D] }, { Register.E , Registers[Register.E] },
            { Register.HL_H , Registers[Register.HL_H] }, { Register.HL_L , Registers[Register.HL_L] },
            
            { Register.SP_H , Registers[Register.SP_H] }, { Register.SP_L , Registers[Register.SP_L] },
        };
        
        RegisterPairs = new()
        {
            { RP.PC, [Registers[Register.PC_L], Registers[Register.PC_H]] },
            { RP.BC, [Registers[Register.C], Registers[Register.B]] },
            { RP.DE, [Registers[Register.E], Registers[Register.D]] },
            { RP.HL, [Registers[Register.HL_L], Registers[Register.HL_H]] }, 
            { RP.SP, [Registers[Register.SP_L], Registers[Register.SP_H]] },
        };
        
        PairIncrements = new()
        {
            { SideEffect.PC_INC, new IncrementPair { Pair = RegisterPairs[RP.PC] } },
            { SideEffect.BC_INC, new IncrementPair { Pair = RegisterPairs[RP.BC] } }, { SideEffect.BC_DCR, new IncrementPair { Pair = RegisterPairs[RP.BC], Decrement = true} },
            { SideEffect.DE_INC, new IncrementPair { Pair = RegisterPairs[RP.DE] } }, { SideEffect.DE_DCR, new IncrementPair { Pair = RegisterPairs[RP.DE], Decrement = true } },
            { SideEffect.HL_INC, new IncrementPair { Pair = RegisterPairs[RP.HL] } }, { SideEffect.HL_DCR, new IncrementPair { Pair = RegisterPairs[RP.HL], Decrement = true } },
            { SideEffect.SP_INC, new IncrementPair { Pair = RegisterPairs[RP.SP] } }, { SideEffect.SP_DCR, new IncrementPair { Pair = RegisterPairs[RP.SP], Decrement = true } },
        };
    }
}

public class RegisterObject
{
    private byte Value;

    public void Set(byte input)
    {
        Value = input;
    }

    public byte Get() => Value;
};

public enum Register
{
    NONE,
    PC_H, PC_L, 
    IR, 
    RAM,
    SP_H, SP_L,
    A, B, C, D, E, 
    TMP, HL_H, HL_L,
}

public enum RP
{
    NONE,
    PC, SP, BC, DE, HL
}

public class IncrementPair
{
    public RegisterObject[] Pair = [];
    public bool Decrement = false;
}
