using BlockchainLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockchainOperationsUnitTests;

[TestClass]
public class BlockchainLowLevelTests
{
    [TestMethod]
    public void CreateGenesisBlockTest()
    {
        BlockchainHighLevel BC = new();
        Assert.AreEqual("GenesisBlock", BC.Blockchain.Last.Value.Data);
    }

    [TestMethod]
    public void CreateSecondBlockTest()
    {
        BlockchainHighLevel BC = new();
        BC.AddBlock("Second Test");
        //Get the data for the second Block
        Assert.AreEqual(BC.Blockchain.Last.Value.Data, "Second Test");
    }
}
