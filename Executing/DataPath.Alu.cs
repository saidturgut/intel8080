namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    public void AluAction()
    {
        if(signals.AluAction is null)
            return;

        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            A = Dbus.Get(),
            B = Registers[(byte)Register.TMP].Get(),
            C = (byte)(action.UseCarry && GetFlag(PswFlag.Carry) ? 1 : 0),
        });
        
        Registers[(byte)Register.A].Set(output.Result);
    }
}