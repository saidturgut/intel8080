namespace i8080_emulator.Executing.Computing;

public class ALU
{
    public ALUOutput Compute(ALUInput input)
    {
        ALUOutput output = new ALUOutput();
        int result = 0;
        
        switch (input.ALUOperation.Operation)
        {
            case Operation.ADD:
            {
                byte carry = (byte)(input is { CR: true, ALUOperation.UseCarry: true } ? 1 : 0);
                result = input.A + input.B + carry;
                
                if (((input.A & 0x0F) + (input.B & 0x0F) + carry) > 0x0F) output.Flags |= (byte)ALUFlags.AuxCarry;
                if (result > 0xFF) output.Flags |= (byte)ALUFlags.Carry;
                break;
            }
            case Operation.SUB:
            {
                byte carry;
                if (input.ALUOperation.UseCarry)
                    carry = (byte)(input.CR ? 0 : 1);
                else
                    carry = 1;
                
                result = input.A + (~input.B & 0xFF) + carry;
                
                if ((input.A & 0x0F) - (input.B & 0x0F) - (1 - carry) < 0) output.Flags |= (byte)ALUFlags.AuxCarry;
                if (result < 0x100) output.Flags |= (byte)ALUFlags.Carry;
                break;
            }
            case Operation.AND:
            {
                result = input.A & input.B;
                
                output.Flags |= (byte)ALUFlags.AuxCarry;
                break;
            }
            case Operation.XOR:
            {
                result = input.A ^ input.B;
                break;
            }
            case Operation.OR:
            {
                result = input.A | input.B;
                break;
            }
        }
        if ((result & 0x80) != 0) output.Flags |= (byte)ALUFlags.Sign;
        if (result == 0) output.Flags |= (byte)ALUFlags.Zero;
        if (EvenParity((byte)result)) output.Flags |= (byte)ALUFlags.Parity;
        
        output.Result = (byte)result;
        return output;
    }
    
    private bool EvenParity(byte result)
    {
        int ones = 0;
        for (int i = 0; i < 8; i++)
        {
            ones += (result >> i) & 1;
        }

        return (ones & 1) == 0;
    }
}