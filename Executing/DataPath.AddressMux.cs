namespace i8080_emulator.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    public void AddressDrive()
    {
        if (signals.IncAction is not IncAction.NONE)
        {
            Increment();
            return;
        }
        
        if(signals.AddressDriver is Register.NONE)
            return;
        
        AbusL.Set(Registers[(byte)signals.AddressDriver].Get());
        AbusH.Set(Registers[((byte)signals.AddressDriver) + 1].Get());
    }

    private void Increment()
    {
        byte lowLatch = Registers[(byte)signals.AddressDriver].Get();
        byte highLatch = Registers[(byte)signals.AddressDriver].Get();

        switch (signals.IncAction)
        {
            case IncAction.INC:
            {
                Registers[(byte)signals.AddressDriver].Set((byte)(lowLatch + 1));
                if (lowLatch == 0xFF)
                {
                    Registers[(byte)signals.AddressDriver + 1].Set((byte)(highLatch + 1));
                }
                return;
            }
            case IncAction.DEC:
            {
                Registers[(byte)signals.AddressDriver].Set((byte)(lowLatch - 1));
                if (lowLatch == 0x00)
                {
                    Registers[(byte)signals.AddressDriver + 1].Set((byte)(highLatch - 1));
                }
                return;
            }
        }
    }
}