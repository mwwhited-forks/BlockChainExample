﻿using BlockchainLibrary;
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
        BlockchainHighLevel BE = new();
        Assert.AreEqual("GenesisBlock", BE.Blockchain.First.Value.Data);
    }

    [TestMethod]
    public void VerifyBlockSearch()
    {
        BlockchainHighLevel BE = new();
        BE.AddBlock("SecondTest");
        //Get the data for the second Block
        Block SecondBlock = BE.Blockchain.Last.Value;
        BE.AddBlock("ThirdTest");
        Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
        Assert.AreEqual("SecondTest", FoundBlock.Data);
    }

    [TestMethod]
    public void TamperWithSecondBlockAndTest()
    {
        BlockchainHighLevel BE = new();
        BE.AddBlock("SecondTest");
        //Get the data for the second Block
        Block SecondBlock = BE.Blockchain.Last.Value;
        BE.AddBlock("ThirdTest");

        //Get the second block to tamper with it
        Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
        LinkedListNode<Block> FoundBlockNode = new(FoundBlock);

        //Construct a tampered block
        Block TamperedBlock = new(FoundBlock.BlockHash, FoundBlockNode.Value.PreviousBlockHash, "Tampered Block");
        BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

        //Delete the true block
        BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

        ///Find the next node after the tampered Block
        Block FindTamperedBlock = BE.FindBlock(TamperedBlock.BlockHash);
        LinkedListNode<Block> FindNodeAfterTamperedBlock = new(FoundBlock);

        //Verify the tampered block
        bool BlockIsAuthentic = BE.VerifyBlock(TamperedBlock, BE.Blockchain.First.Value, FindNodeAfterTamperedBlock.Value);
        Assert.IsFalse(BlockIsAuthentic);

    }

    [TestMethod]
    public void VerifyNoTamperOnSecondBlockTest()
    {
        BlockchainHighLevel BE = new();
        BE.AddBlock("SecondTest");
        //Get the data for the second Block
        Block SecondBlock = BE.Blockchain.Last.Value;
        BE.AddBlock("ThirdTest");

        //Get the second block
        Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);

        //Verify the second block
        bool BlockIsAuthentic = BE.VerifyBlock(FoundBlock, BE.Blockchain.First.Value, BE.Blockchain.Last.Value);
        Assert.IsTrue(BlockIsAuthentic);
    }


    [TestMethod]
    public void VerifyLastNode()
    {
        BlockchainHighLevel BE = new();
        BE.AddBlock("SecondTest");

        BE.AddBlock("ThirdTest");

        bool Verified = BE.VerifyBlock(BE.Blockchain.Last.Value, BE.Blockchain.Last.Previous.Value, null);
        Assert.IsTrue(Verified);


    }



    [TestMethod]
    public void TamperSecondBlockAndFindDuringVerificationOfBlockChainTest()
    {
        BlockchainHighLevel BE = new();

        BE.AddBlock("SecondTest");
        //Get the data for the second Block
        Block SecondBlock = BE.Blockchain.Last.Value;

        BE.AddBlock("ThirdTest");

        //Get the second block to tamper with it
        Block FoundBlock = BE.FindBlock(SecondBlock.BlockHash);
        LinkedListNode<Block> FoundBlockNode = new(FoundBlock);

        //Construct a tampered block
        Block TamperedBlock = new(FoundBlock.BlockHash, FoundBlockNode.Value.PreviousBlockHash, "Tampered Block");
        BE.Blockchain.AddBefore(BE.Blockchain.Find(FoundBlock), TamperedBlock);

        //Delete the true block
        BE.Blockchain.Remove(BE.Blockchain.FindLast(FoundBlock));

        bool TamperedBlockResult = BE.VerifyBlock(TamperedBlock, BE.Blockchain.First.Value, BE.Blockchain.Last.Value);
        Assert.IsFalse(TamperedBlockResult);

    }

    [TestMethod]
    public void CreateSimpleBlockHashTest()
    {

        byte[] PerviousBlockHash = BitConverter.GetBytes(0);
        string TransactionData = "MyTransactionData";
        int nonce = 0;

        BlockchainHighLevel BE = new();
        BE.CreateASimpleBlockHash(PerviousBlockHash, TransactionData, nonce);
    }
}
