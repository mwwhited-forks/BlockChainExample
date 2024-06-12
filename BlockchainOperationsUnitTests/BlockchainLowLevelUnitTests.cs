using BlockchainLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockchainOperationsUnitTests;

[TestClass]
public class BlockchainLowLevelTests
{
    [TestMethod]
    public void CreateGenesisBlockTest()
    {
        var bc = new BlockchainHighLevel();
        Assert.AreEqual("GenesisBlock", bc.Blockchain.Last.Value.Data);
    }

    [TestMethod]
    public void CreateSecondBlockTest()
    {
        var bc = new BlockchainHighLevel();
        bc.AddBlock("Second Test");
        //Get the data for the second Block
        Assert.AreEqual(bc.Blockchain.Last.Value.Data, "Second Test");
    }
}
