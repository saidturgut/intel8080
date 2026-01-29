using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    private static SignalSet IDLE() => new();
    private static SignalSet FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = Register.IR,
        State = State.FETCH,
    };
    private static SignalSet DECODE() => new() { State = State.DECODE };
    private static SignalSet HALT() => new() { State = State.HALT };

    private static SignalSet EXECUTE_ALU() => new()
        { AluAction = decoded.AluAction!.Value, };
    private static SignalSet CLEAR_CARRY() => new()
    {
        AluAction = new AluAction
        {
            Operation = Operation.OR, 
            FlagMask = PswFlag.Carry,
            LatchPermit = false,
        },
    };

    private static SignalSet MOVE_IMM() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        IncAction = IncAction.INC,
    };
    private static SignalSet MOVE_LOAD() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    private static SignalSet MOVE_STORE() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataLatcher,
        DataLatcher = decoded.DataDriver,
    };

    private static SignalSet MOVE_PAIR_IMM() => new()
    {
        AddressDriver = Register.PC_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    private static SignalSet MOVE_PAIR_LOAD() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = decoded.IncAction,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    private static SignalSet MOVE_PAIR_STORE() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = decoded.IncAction,
        DataDriver = decoded.Queue[queueIndex],
        DataLatcher = decoded.DataDriver,
        Index = true,
    };
    
    private static SignalSet MOVE_PAIR_TO_TMP() => new()
    {
        DataDriver = decoded.Queue[queueIndex],
        DataLatcher = Register.TMP,
        Index = true,
    };
    private static SignalSet MOVE_TMP_TO_PAIR() => new()
    {
        DataDriver = Register.TMP,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };

    private static SignalSet MOVE_CYCLE_LATCH() => new()
    {
        CycleLatch = decoded.CycleLatch,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    private static SignalSet MOVE_ZERO() => new()
        { DataLatcher = decoded.DataLatcher, };
    
    private static SignalSet INC_PAIR() => new()
    {
        AddressDriver = decoded.Queue[queueIndex],
        IncAction = IncAction.INC,
        Index = true,
    };
    private static SignalSet DEC_PAIR() => new()
    {
        AddressDriver = decoded.Queue[queueIndex],
        IncAction = IncAction.DEC,
        Index = true,
    };
}