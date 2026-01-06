namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;
using Executing;

public partial class ControlUnitROM
{
    private static SignalSet STC() => new () { SideEffect = SideEffect.STC };
    private static SignalSet CMC() => new () { SideEffect = SideEffect.CMC };
    private static SignalSet INX_DCX() => new () { SideEffect = decoded.SideEffect };
    
    private static SignalSet LXI_LOW() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.RegisterPairs[0],
        SideEffect = SideEffect.PC_INC,
    };
    
    private static SignalSet LXI_HIGH() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.RegisterPairs[1],
        SideEffect = SideEffect.PC_INC,
    };
}