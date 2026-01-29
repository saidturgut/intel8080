using i8080_emulator.Executing.Components;

namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    private readonly Psw Psw = new();
    
    public void AluAction()
    {
        if(signals.AluAction is null)
            return;

        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            A = Reg(Register.A).Get(), // SOURCE
            B = Reg(Register.TMP).Get(), // OPERAND
            C = (byte)(Psw.Carry && action.UseCarry ? 1 : 0),
            Psw = Psw,
        }, action.Operation);

        if (action.LatchPermit)
            Reg(Register.A).Set(output.Result); // DESTINATION

        // SET PROCESS STATUS WORD
        byte newPsw = (byte)
            ((Reg(Register.PSW).Get() & (byte)~action.FlagMask) | (output.Flags & (byte)action.FlagMask));
        
        Psw.Update(newPsw);
        
        Reg(Register.PSW).Set(newPsw);
        
    }
}