namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;
using Executing;

public partial class ControlUnitROM
{
    // *** READ / WRITE AND TO TMP *** //
    private static SignalSet RAM_READ_TMP() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
    };
    private static SignalSet RAM_WRITE_TMP() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = Register.TMP,
        DataLatcher = Register.RAM,
    };
    
    // *** READ / WRITE AND EXECUTE *** //
    private static SignalSet RAM_READ_EXE() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = Register.RAM,
        DataLatcher = decoded.DataLatcher,
        SideEffect = decoded.SideEffect,
    };
    private static SignalSet RAM_WRITE_EXE() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver,
        DataLatcher = Register.RAM,
        SideEffect = decoded.SideEffect,
    };
    
    // *** IMMEDIATE MEMORY READ *** //
    private static SignalSet RAM_READ_IMM() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        SideEffect = SideEffect.PC_INC,
    };
    private static SignalSet RAM_READ_IMM_LOW() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.LatchPairs[0],
        SideEffect = SideEffect.PC_INC,
    };
    private static SignalSet RAM_READ_IMM_HIGH() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.LatchPairs[1],
        SideEffect = SideEffect.PC_INC,
    };
}