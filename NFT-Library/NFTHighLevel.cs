using System.IO;
using System.Security.Cryptography;

namespace NFT_Library;

public static class NFTHighLevel
{
    public static string MintNFT(string from, string action, string to, string filename) =>
        new NFT(from, action, to, SHA1.HashData(File.ReadAllBytes(filename))).TransactionString;
}
