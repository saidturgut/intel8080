namespace intel8080.Executing.Components;
using Computing;

public class Psw
{
    public static bool Sign;
    public static bool Zero;
    public static bool Auxiliary;
    public static bool Parity;
    public static bool Carry;

    public void Update(byte psw)
    {
        Sign = (psw & (byte)Flag.Sign) != 0;
        Zero = (psw & (byte)Flag.Zero) != 0;
        Auxiliary = (psw & (byte)Flag.Auxiliary) != 0;
        Parity = (psw & (byte)Flag.Parity) != 0;
        Carry = (psw & (byte)Flag.Carry) != 0;
    }

    public bool CheckCondition(Condition condition) => condition switch
    {
        Condition.NZ => !Zero,
        Condition.Z => Zero,
        Condition.NC => !Carry,
        Condition.C => Carry,
        Condition.PO => !Parity,
        Condition.PE => Parity,
        Condition.P => !Sign,
        Condition.M => Sign,
        _ => false
    };
}

public enum Condition
{
    NZ, Z, NC, C, PO, PE, P, M
}