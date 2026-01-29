namespace i8080_emulator.Signaling.Multiplexer;

public partial class MicroUnitMux
{
    private static SignalSet IMM_PAIR() => new()
    {
        AddressDriver = Register.PC_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    
    private static SignalSet STORE_PAIR() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = decoded.IncAction,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    private static SignalSet LOAD_PAIR() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = decoded.IncAction,
        DataDriver = decoded.Queue[queueIndex],
        DataLatcher = decoded.DataDriver,
        Index = true,
    };
    
    private static SignalSet STORE_PAIR_TMP() => new()
    {
        DataDriver = decoded.Queue[queueIndex],
        DataLatcher = Register.TMP,
        Index = true,
    };
    private static SignalSet LOAD_PAIR_TMP() => new()
    {
        DataDriver = Register.TMP,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    
    private static SignalSet LOAD_PAIR_ENCODE() => new()
    {
        EncodeLatch = decoded.EncodeLatch,
        DataLatcher = decoded.Queue[queueIndex],
        Index = true,
    };
    
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