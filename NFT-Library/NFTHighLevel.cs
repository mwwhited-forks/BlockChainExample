using System.IO;
using System.Security.Cryptography;

namespace NFT_Library;

public static class NFTHighLevel
{
    public static string MintNFT(string From, string Action, string To, string Filename) =>
        new NFT(From, Action, To, SHA1.HashData(File.ReadAllBytes(Filename))).TransactionString;
}
