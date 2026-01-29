namespace i8080_emulator.Signaling.Multiplexer;
using Executing.Computing;

public partial class MicroUnitMux
{
    private static SignalSet IMM_TMP() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        IncAction = IncAction.INC,
    };
    
    private static SignalSet STORE_REG() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    private static SignalSet LOAD_REG() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataLatcher,
        DataLatcher = decoded.DataDriver,
    };
    
    private static SignalSet COMPUTE_ALU() => new()
    {
        AluAction = decoded.AluAction!.Value, 
    };
    private static SignalSet ZERO_CARRY() => new()
    {
        AluAction = new AluAction
        {
            Operation = Operation.OR, 
            FlagMask = PswFlag.Carry,
            LatchPermit = false,
        },
    };
    private static SignalSet ZERO_REG() => new()
    {
        DataLatcher = decoded.DataLatcher, 
    };
}