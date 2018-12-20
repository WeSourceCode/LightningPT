namespace LightningPT.TrackerServer.AnnounceService.Dtos.InputDto
{
    public class GetTrackInfoInput
    {
        public string Info_Hash { get; set; }

        public string Peer_Id { get; set; }

        public long UpLoaded { get; set; }

        public long DownLoaded { get; set; }

        public string Event { get; set; }

        /// <summary>
        /// 用户唯一密钥。
        /// </summary>
        public string PassKey { get; set; }
    }
}