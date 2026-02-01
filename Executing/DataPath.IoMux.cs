using intel8080.Signaling;

namespace intel8080.Executing;

public partial class DataPath
{
    private readonly Tty Tty = new();
    
    private void Input()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 0: Reg(Register.A).Set(Tty.ReadStatus()); break;
            case 1: Reg(Register.A).Set(Tty.ReadData()); break;
            default: throw new Exception($"INVALID INPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
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