using BlockchainLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BlockchainOperationsUnitTests;

[TestClass]
public class BlockchainHighLevelUnitTests
{
    [TestMethod]
    public void EngineTestCreateGenesisBlock()
    {
        var be = new BlockchainHighLevel();
        Assert.AreEqual("GenesisBlock", be.Blockchain.First.Value.Data);
    }

    [TestMethod]
    public void VerifyBlockSearch()
    {
        var be = new BlockchainHighLevel();
        be.AddBlock("SecondTest");
        //Get the data for the second Block
        var SecondBlock = be.Blockchain.Last.Value;
        be.AddBlock("ThirdTest");
        var FoundBlock = be.FindBlock(SecondBlock.BlockHash);
        Assert.AreEqual("SecondTest", FoundBlock.Data);
    }

    [TestMethod]
    public void TamperWithSecondBlockAndTest()
    {
        var be = new BlockchainHighLevel();
        be.AddBlock("SecondTest");
        //Get the data for the second Block
        var secondBlock = be.Blockchain.Last.Value;
        be.AddBlock("ThirdTest");

        //Get the second block to tamper with it
        var foundBlock = be.FindBlock(secondBlock.BlockHash);
        var foundBlockNode = new LinkedListNode<Block>(foundBlock);

        //Construct a tampered block
        var tamperedBlock = new Block(foundBlock.BlockHash, foundBlockNode.Value.PreviousBlockHash, "Tampered Block");
        be.Blockchain.AddBefore(be.Blockchain.Find(foundBlock), tamperedBlock);

        //Delete the true block
        be.Blockchain.Remove(be.Blockchain.FindLast(foundBlock));

        ///Find the next node after the tampered Block
        var findTamperedBlock = be.FindBlock(tamperedBlock.BlockHash);
        var findNodeAfterTamperedBlock = new LinkedListNode<Block>(foundBlock);

        //Verify the tampered block
        bool blockIsAuthentic = be.VerifyBlock(tamperedBlock, be.Blockchain.First.Value, findNodeAfterTamperedBlock.Value);
        Assert.IsFalse(blockIsAuthentic);

    }

    [TestMethod]
    public void VerifyNoTamperOnSecondBlockTest()
    {
        var be = new BlockchainHighLevel();
        be.AddBlock("SecondTest");
        //Get the data for the second Block
        var secondBlock = be.Blockchain.Last.Value;
        be.AddBlock("ThirdTest");

        //Get the second block
        var foundBlock = be.FindBlock(secondBlock.BlockHash);

        //Verify the second block
        var blockIsAuthentic = be.VerifyBlock(foundBlock, be.Blockchain.First.Value, be.Blockchain.Last.Value);
        Assert.IsTrue(blockIsAuthentic);
    }


    [TestMethod]
    public void VerifyLastNode()
    {
        var be = new BlockchainHighLevel();
        be.AddBlock("SecondTest");
        be.AddBlock("ThirdTest");

        bool verified = be.VerifyBlock(be.Blockchain.Last.Value, be.Blockchain.Last.Previous.Value, null);
        Assert.IsTrue(verified);
    }

    [TestMethod]
    public void TamperSecondBlockAndFindDuringVerificationOfBlockChainTest()
    {
        var be = new BlockchainHighLevel();

        be.AddBlock("SecondTest");
        //Get the data for the second Block
        var secondBlock = be.Blockchain.Last.Value;

        be.AddBlock("ThirdTest");

        //Get the second block to tamper with it
        var foundBlock = be.FindBlock(secondBlock.BlockHash);
        var foundBlockNode = new LinkedListNode<Block>(foundBlock);

        //Construct a tampered block
        var tamperedBlock = new Block(foundBlock.BlockHash, foundBlockNode.Value.PreviousBlockHash, "Tampered Block");
        be.Blockchain.AddBefore(be.Blockchain.Find(foundBlock), tamperedBlock);

        //Delete the true block
        be.Blockchain.Remove(be.Blockchain.FindLast(foundBlock));

        bool tamperedBlockResult = be.VerifyBlock(tamperedBlock, be.Blockchain.First.Value, be.Blockchain.Last.Value);
        Assert.IsFalse(tamperedBlockResult);
    }

    [TestMethod]
    public void CreateSimpleBlockHashTest()
    {
        var perviousBlockHash = BitConverter.GetBytes(0);
        var transactionData = "MyTransactionData";
        var nonce = 0;

        var be = new BlockchainHighLevel();
        be.CreateASimpleBlockHash(perviousBlockHash, transactionData, nonce);
    }
}
