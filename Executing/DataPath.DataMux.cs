namespace intel8080.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    private void RegisterMove()
        => Reg(signals.Operand).Set(Reg(signals.Source).Get());

    private void RamRead()
        => Reg(signals.Operand).Set(Ram.Read(Merge(signals.Source)));

    private void RamWrite()
    {
        Ram.Write(Merge(signals.Source), Reg(signals.Operand).Get()); 
        if(!DEBUG_MODE) return;
        Console.WriteLine($"RAM[{Merge(signals.Source)}]: {Reg(signals.Operand).Get()}");
    }
    
    private void Increment()
    {
        Reg(signals.Source).Set((byte)(Reg(signals.Source).Get() + 1));
        if (Reg(signals.Source).Get() == 0x00)
            Reg(signals.Source + 1).Set((byte)(Reg(signals.Source + 1).Get() + 1));
    }

    private void Decrement()
    {
        Reg(signals.Source).Set((byte)(Reg(signals.Source).Get() - 1));
        if (Reg(signals.Source).Get() == 0xFF)
            Reg(signals.Source + 1).Set((byte)(Reg(signals.Source + 1).Get() - 1));
    }
}