namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void ControlALU()
    {
        if(signals.AluOperation is not { })
            return;
        
        var nullable = signals.AluOperation!.Value;
        bool CarryFlag = (byte)(FLAGS & (byte)ALUFlags.Carry) == 1;
        
        if (nullable.Opcode == ALUOpcode.DAD)
        {
            DAD(nullable);
            return;
        }

        // STANDARD
        ALUInput input = StandardInput(nullable, CarryFlag);
        ALUOutput output = ALU.Compute(input);
        DBUS.Set(output.Result);
        UpdateFlags(nullable.FlagMask, output.Flags);
    }
    
    private ALUInput StandardInput(ALUOperation nullable, bool CarryFlag) => new ALUInput(
        nullable.Operation, // ALU OPERATION
        Registers[nullable.A].Get(), // A
        nullable.B != Register.NONE ? Registers[nullable.B].Get() : (byte)1, // B
        CarryFlag,
        nullable.UseCarry); // CARRY

    private void DAD(ALUOperation nullable)
    {
        ALUOutput dadOutput1 = ALU.Compute(new ALUInput
        (nullable.Operation, Registers[Register.HL_L].Get(), 
            Registers[nullable.A].Get(), false, false));
            
        Registers[Register.HL_L].Set(dadOutput1.Result);
            
        ALUOutput dadOutput2 = ALU.Compute(new ALUInput
        (nullable.Operation, Registers[Register.HL_H].Get(), 
            Registers[nullable.B].Get(), 
            (byte)(dadOutput1.Flags & (byte)ALUFlags.Carry) == 1, true));
            
        Registers[Register.HL_H].Set(dadOutput2.Result);
        UpdateFlags(2, dadOutput2.Flags);
    }

    private void UpdateFlags(byte flagMask, byte newFlags)
    {
        ALUFlags mask = ALUROM.FlagMasks[flagMask];

        FLAGS = (byte)((FLAGS & (byte)~mask) | (newFlags & (byte)mask));
    }
}