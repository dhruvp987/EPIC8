using System;
using System.IO;

class Program {
    static void Main(string[] args) {
        if (args.Length != 1) {
            Console.Error.WriteLine("usage: EPIC8 [CHIP-8 file]");
	    Environment.Exit(1);
        }

        if (!(File.Exists(args[0]))) {
            Console.Error.WriteLine($"EPIC8: error: file {args[0]} does not exist");
	    Environment.Exit(1);
        }

        var stream = File.Open(args[0], FileMode.Open);
        var progReader = new BinaryReader(stream);

        var phl = new Peripherals(
	    new Memory(),
	    new ConsoleDisplay(),
	    new EStack(),
	    new GPU(IDisplay.DISPLAY_WIDTH, IDisplay.DISPLAY_HEIGHT));
        var psr = new Processor(MemLoader.PROGRAM_LOAD_LOC);

        MemLoader.LoadBinary(progReader, phl.Mem, MemLoader.PROGRAM_LOAD_LOC);
        psr.Run(phl);
    }
}
