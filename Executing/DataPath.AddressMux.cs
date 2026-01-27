namespace i8080_emulator.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    public void AddressDrive()
    {
        if(signals.AddressDriver is Register.NONE)
            return;
        
        byte lowLatch = Registers[(byte)signals.AddressDriver].Get();
        byte highLatch = Registers[(byte)signals.AddressDriver + 1].Get();

        AbusL.Set(lowLatch);
        AbusH.Set(highLatch);
        
        if (signals.IncAction is not IncAction.NONE)
        {
            Increment(lowLatch, highLatch);
        }
    }

    private void Increment(byte lowLatch,  byte highLatch)
    {
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