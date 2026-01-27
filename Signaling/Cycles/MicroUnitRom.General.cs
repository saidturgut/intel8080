namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    private static SignalSet EMPTY() => new();
    private static SignalSet HALT() => new();
    private static SignalSet DECODE() => new();
    
    private static SignalSet FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.IR,
    };

    private static SignalSet ADDR_INC() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = IncAction.INC,
    };
    private static SignalSet ADDR_DEC() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = IncAction.DEC,
    };

    
    private static SignalSet REG_TO_REG() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
}