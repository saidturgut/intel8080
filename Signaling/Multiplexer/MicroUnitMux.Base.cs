using i8080_emulator.Executing;

namespace i8080_emulator.Signaling.Multiplexer;

public partial class MicroUnitMux
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
    
    private static SignalSet DECODE() => new() 
        { State = State.DECODE };
    
    private static SignalSet HALT() => new() 
        { State = State.HALT };

    private static SignalSet IO() => new()
    {
        AddressDriver = Register.TMP,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
        IoAction = decoded.IoAction,
    };
}