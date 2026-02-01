namespace intel8080.Executing;
using Components;
using Computing;
using Signaling;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    public readonly Psw Psw = new();
    
    private void AluCompute()
    {
        AluOutput output = Alu.Compute(new AluInput
        {
            A = Reg(signals.Source).Get(), // SOURCE
            B = Reg(Register.TMP).Get(), // OPERAND
            C = (byte)(Psw.Carry && signals.AluAction.UseCarry ? 1 : 0),
            Psw = Psw,
        }, signals.AluAction.Operation);
        
        Reg(Register.TMP).Set(output.Result);
        
        // SET PROCESS STATUS WORD
        byte newPsw = (byte)
            ((Reg(Register.PSW).Get() & (byte)~signals.AluAction.FlagMask) 
             | (output.Flags & (byte)signals.AluAction.FlagMask));
        
        Reg(Register.PSW).Set(newPsw);
    }
}