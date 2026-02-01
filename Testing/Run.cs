namespace intel8080.Testing;

internal static class Run
{
    private static readonly Cpu Cpu = new ();
    
    private static void Main() => Cpu.PowerOn();
}