using System;
using BencodeNET.Parsing;
using BencodeNET.Torrents;

namespace LightningPT.BitTorrentGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new BencodeParser();
            var torrent =
                parser.Parse<Torrent>(
                    "/Users/zony/Downloads/[GZtown].IMDb.Top.250.BluRay.1080p.x265.10bit.MNHD-FRDS.torrent");
            var t2 = new Torrent();
            
            Console.WriteLine("Hello World!");
        }
    }
}