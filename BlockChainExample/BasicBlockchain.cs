using BlockchainLibrary;
using NFT_Library;
using System;

namespace BlockChainExample;

public static class BasicBlockchain
{
    public static void GenerateBasicBlockChain()
    {
        //Create a blockchain and add some transactions
        BlockchainHighLevel BE = new();
        BE.AddBlock("Ryan Pays PoshRyan 10 BTC for lunch");
        BE.AddBlock("Seymore pays 20 BTC to Moo2You for Milkshake.");

        int itemNumber = 0;
        foreach (var item in BE.Blockchain)
        {

            Console.WriteLine("---------------------------");
            Console.WriteLine("Block Height: " + itemNumber);
            Console.WriteLine("Block Hash: " + item.HashR);
            Console.WriteLine("Previous Hash: " + item.PreviousHashR);
            Console.WriteLine("Transaction Data: " + item.Data);
            Console.WriteLine("---------------------------");
            Console.WriteLine("");
            itemNumber++;
        }
    }

    public static void GenerateBasicNFTFromCommandLine(string[] args)
    {
        var BE = new BlockchainHighLevel();

        var From = args[0];
        var Action = args[1];
        var To = args[2];
        var Filename = args[3];

        var NFT = NFTHighLevel.MintNFT(From, Action, To, Filename);
        BE.AddBlock(NFT);

        int itemNumber = 0;
        foreach (var item in BE.Blockchain)
        {

            Console.WriteLine("---------------------------");
            Console.WriteLine("Hash: " + item.HashR);
            Console.WriteLine("Previous Hash: " + item.PreviousHashR);
            Console.WriteLine("Data: " + item.Data);
            Console.WriteLine("---------------------------");
            Console.WriteLine("");
            itemNumber++;
        }
    }
}
