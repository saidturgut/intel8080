namespace intel8080.Executing.Components;

public class Reg
{
    private byte value;
    
    public void Set(byte input) 
        => value = input;

    public byte Get()
        => value;
}