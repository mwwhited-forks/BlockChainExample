using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlockchainLibrary;

namespace BlockchainOperationsUnitTests;

[TestClass]
public class AdvancedBlockchainHighLevelUnitTests
{
    [TestMethod]
    public void TestFindABlockWithADifficultyOf1Test() //0
    {
        int Difficulty = 1;
        var be = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        be.Add("Ryan Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 35BTC for a new shirt");
        Assert.AreEqual(0, be.Blockchain.Last.Value.BlockHash[0]);
    }

    [TestMethod]
    public void TestFindABlockWithADifficultyOf2Test() //00
    {
        int Difficulty = 2;
        var be = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        be.Add("Ryan Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 35BTC for a new shirt");
        Assert.AreEqual(0, be.Blockchain.Last.Value.BlockHash[1]);

    }

    /// <summary>
    /// IF you go over 3 zeros, be prepared to wait.
    /// </summary>
    [TestMethod]
    public void TestFindABlockWithADifficultyOf3Test() //000
    {
        int Difficulty = 3;
        var BE = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        BE.Add("Ryan Pays Seymore 10BTC for Lunch");
        BE.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        BE.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        BE.Add("POSH Ryan pays 35BTC for a new shirt");
        Assert.AreEqual(0, BE.Blockchain.Last.Value.BlockHash[2]);
    }

    [TestMethod]
    public void VerifyTransactionTest()
    {
        int Difficulty = 1;
        var be = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        be.Add("Ryan Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 35BTC for a new shirt");

        bool isValid = be.VerifyBlock(be.Blockchain.Last.Value, be.Blockchain.First.Value, null);
        Assert.IsTrue(isValid);
    }

    [TestMethod]
    public void VerifyLastBlockPass()
    {
        int Difficulty = 1;
        var be = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        be.Add("Ryan Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 35BTC for a new shirt");

        be.Add("Rayanne Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for Lunch");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 40BTC for a new pants");

        bool blockVerified = be.VerifyBlock(be.Blockchain.Last.Value, be.Blockchain.Last.Previous.Value, null);
        Assert.IsTrue(blockVerified);

    }

    [TestMethod]
    public void VerifySecondBlockTamperTest()
    {

        int Difficulty = 1;
        var be = new AdvancedBlockchainHighLevel("RyanMiner", Difficulty);
        be.Add("Ryan Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 35BTC for a new shirt");

        var  SecondBlockNode = be.Blockchain.Last;

        be.Add("Rayanne Pays Seymore 10BTC for Lunch");
        be.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        be.Add("Seymore Pays 5 BTC for Luncg");
        ///The transaction should trigger automatically after the 4th transaction
        be.Add("POSH Ryan pays 40BTC for a new pants");


        //Tamper with the Second Block and create a replacement block
        var blockToTamper = new Block(SecondBlockNode.Value.BlockHash, SecondBlockNode.Previous.Value.BlockHash, "Tampered");
        var TamperedBlockNode = new LinkedListNode<Block>(blockToTamper);

        be.Blockchain.AddBefore(be.Blockchain.Find(SecondBlockNode.Value), TamperedBlockNode);

        //Delete the true block
        be.Blockchain.Remove(be.Blockchain.FindLast(SecondBlockNode.Value));

        bool verifiedBlock = be.VerifyBlock(TamperedBlockNode.Value, TamperedBlockNode.Previous.Value, TamperedBlockNode.Next.Value);
        Assert.IsFalse(verifiedBlock);
    }

    [TestMethod]
    public void TestHashForVideo()
    {
        int Difficulty = 1;
        AdvancedBlockchainHighLevel BE = new("RyanMiner", Difficulty);
        BE.Add("Ryan Pays Seymore 10BTC for Lunch");
        BE.Add("Rayanne Pays Starbucks 8 BTC for Coffee");
        BE.Add("Seymore Pays 5 BTC for VPN subscription");
        ///The transaction should trigger automatically after the 4th transaction
        BE.Add("POSH Ryan pays 35BTC for a new shirt");

        bool isValid = BE.VerifyBlock(BE.Blockchain.Last.Value, BE.Blockchain.First.Value, null);
        Assert.IsTrue(isValid);
    }
}
