namespace i8080_emulator.Executing;
using Components;
using Computing;
using Signaling;

public partial class DataPath
{
    private readonly Tty Tty = new ();
    private readonly Disk Disk = new ();
    
    public void IoControl()
    {
        switch (signals.IoAction)
        {
            case IoAction.NONE:
            {
                return;
            }
            case IoAction.INPUT:
            {
                Dbus.Set(AbusL.Get() switch
                {
                    0x00 => Tty.ReadStatus(),
                    0x01 => Tty.ReadData(),
                    _=> throw new Exception("INVALID INPUT PORT"),
                });
                return;
            }
            case IoAction.OUTPUT:
            {
                switch (AbusL.Get())
                {
                    case 0x02: Tty.WriteData(Dbus.Get()); break;
                    default: throw new Exception("INVALID OUTPUT PORT");
                }
                return;
            }
        }
    }
}

public enum IoAction
{
    NONE, INPUT, OUTPUT,
}
