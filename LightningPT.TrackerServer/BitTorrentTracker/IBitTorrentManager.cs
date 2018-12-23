using System;
using System.Collections.Generic;

namespace LightningPT.TrackerServer.BitTorrentTracker
{
    public interface IBitTorrentManager
    {
        /// <summary>
        /// 为某个种子的可用 Peer 列表新增一个可用的 Peer 信息。
        /// </summary>
        /// <param name="infoHash">种子唯一 Hash Id。</param>
        /// <param name="peer">要增加的 Peer 信息。</param>
        Peer AddPeer(string infoHash, Peer peer);

        /// <summary>
        /// 获得当前种子的可用 Peer 列表。
        /// </summary>
        /// <param name="infoHash">种子唯一 Hash Id。</param>
        /// <returns>可用的客户端列表。</returns>
        IReadOnlyList<Peer> GetPeers(string infoHash);

        /// <summary>
        /// 删除某个种子的 Peer 列表中不可用的 Peer。
        /// </summary>
        /// <param name="infoHash">种子的唯一 Hash Id。</param>
        /// <param name="peer">要删除的 Peer 信息。</param>
        void DeletePeer(string infoHash, Peer peer);

        /// <summary>
        /// 清除某个种子的 Peer 列表中的僵尸 Peer (不活跃的 Peer 端)。
        /// </summary>
        /// <param name="infoHash">种子的唯一 Hash Id。</param>
        /// <param name="expiry">要删除的时间周期，比如说移除掉 1 个小时内没有响应的 Peer
        /// 信息。</param>
        void ClearZombiePeers(string infoHash, TimeSpan expiry);

        /// <summary>
        /// 根据 Peer 的请求参数来更新 Peer 列表信息。
        /// </summary>
        /// <param name="params">Peer 请求的参数。</param>
        void UpdateInfo(AnnounceRequestParameters @params);

        int GetComplete(string infoHash);

        int GetInComplete(string infoHash);
    }
}