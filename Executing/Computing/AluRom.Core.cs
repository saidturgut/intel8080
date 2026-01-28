namespace i8080_emulator.Executing.Computing;

public partial class AluRom
{
    private static AluOutput NONE(AluInput input) => new();

    private static AluOutput ADD(AluInput input)
    {
        AluOutput output = new();
        
        var result = input.A + input.B + input.C;
        output.Result = (byte)result;
     
        if ((input.A & 0xF) + (input.B & 0xF) + input.C > 0xF) 
            output.Flags |= (byte)PswFlag.Auxiliary;
        if (result > 0xFF) 
            output.Flags |= (byte)PswFlag.Carry;
        
        return output;
    }
    private static AluOutput SUB(AluInput input)
    {
        AluOutput output = new();
        input.C = (byte)(1 - input.C);
        
        var result = input.A + (~input.B & 0xFF) + input.C;
        output.Result = (byte)result;
     
        if ((input.A & 0x0F) - (input.B & 0x0F) - (1 - input.C) < 0) 
            output.Flags |= (byte)PswFlag.Auxiliary;
        if (result >= 0x100)
            output.Flags |= (byte)PswFlag.Carry;
        
        return output;
    }
    
    private static AluOutput AND(AluInput input) => new()
    {

    };

    private static AluOutput XOR(AluInput input) => new()
    {

    };
    
    private static AluOutput OR(AluInput input) => new()
    {

    };
}