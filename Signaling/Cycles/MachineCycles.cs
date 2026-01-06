using i8080_emulator.Executing;

namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;

public partial class ControlUnitROM
{
    private static SignalSet FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.IR,
        SideEffect = SideEffect.PC_INC,
    };

    private static SignalSet RAM_READ() => new ()
    {
        AddressDriver = Register.HL_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
    };

    private static SignalSet RAM_READ_IMM() => new ()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        SideEffect = SideEffect.PC_INC,
    };

    private static SignalSet RAM_WRITE() => new ()
    {
        AddressDriver = Register.HL_L,
        DataDriver = Register.TMP,
        DataLatcher = Register.RAM,
    };

    private static SignalSet INTERNAL_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    
    private static SignalSet TMP_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = Register.TMP,
    };
    
    private static SignalSet ALU_EXECUTE() => new ()
    {
        AluOperation = (ALUOperation)decoded.AluOperation!,
        DataLatcher = decoded.DataLatcher,
    };
}