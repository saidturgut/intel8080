namespace intel8080.Signaling;
using Executing.Components;
using Executing.Computing;

public class MicroUnit
{
    private readonly Decoder Decoder = new();

    private SignalSet[] Decoded = [];

    private byte timeState;
    
    public bool BOUNDARY;

    public string DEBUG_NAME;

    public void Init(Psw psw)
    {
        Decoder.Psw = psw;
        Decoded = Decoder.FETCH;
    }

    public SignalSet Emit()
    {
        DEBUG_NAME = Decoder.DEBUG_NAME;

        return Decoded[timeState];
    }
    
    public void Advance(byte ir)
    {
        //Console.WriteLine($"T STATE: {timeState}");

        if (timeState != Decoded.Length - 1)
        {
            timeState++;
        }
        else
        {
            switch (Decoded[timeState].MicroStep)
            {
                case MicroStep.HALT: break;
                case MicroStep.DECODE: Decoded = Decoder.Decode(ir); break;
                default: 
                    BOUNDARY = true;
                    Decoded = Decoder.FETCH; break;
            }
            
            timeState = 0;
        }
    }
}
