namespace intel8080.Executing;
using Signaling;

public partial class DataPath
{
    public readonly Tty Tty = new();
    
    private void Input()
    {
        Reg(Register.A).Set(Reg(Register.TMP).Get() switch
        {
            0 => Tty.ReadStatus(),
            1 => Tty.ReadData(),
            _ => throw new Exception($"INVALID INPUT PORT \"{Reg(Register.TMP).Get()}\"")
        });
    }
    
    private void Output()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 1: Tty.WriteData(Reg(Register.A).Get()); break; 
            default: throw new Exception($"INVALID OUTPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
    }
}