using BencodeNET.Objects;

namespace LightningPT.TrackerServer
{
    public static class LightningPTTrackerServerConsts
    {
        public static readonly BString PeerIdKey = new BString("peer id");
        public static readonly BString Port = new BString("port");
        public static readonly BString Ip = new BString("ip");

        public static readonly string FailureKey = "failure reason";
    }
}