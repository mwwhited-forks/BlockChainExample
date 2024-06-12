namespace BlockChainExample;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            BasicBlockchain.GenerateBasicBlockChain();
        }

        //To activate this in the debugger, right click 
        //the BlockchainExample project, select "properties"
        //Go to "Debug" and "Application Arguments". for older versions
        //
        //Or go to Debug > General > Open Debug Launch Profiles UI > Application Arguments
        //Paste the
        //code below inside the Application Arguments box minus the "//"
        //Ryan Sells Seymore .\images\CirclebackJack.bmp
        if (args.Length > 0)
        {
            BasicBlockchain.GenerateBasicNFTFromCommandLine(args);
        }
    }
}
