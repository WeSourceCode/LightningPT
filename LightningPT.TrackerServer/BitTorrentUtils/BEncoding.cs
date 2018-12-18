using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Abp;
using LightningPT.Core.Extensions;

namespace LightningPT.TrackerServer.BitTorrentUtils
{
    /// <summary>
    /// B 编码相关的序列化与反序列化工具类
    /// </summary>
    [Obsolete("已经弃用，请使用 BencodeNET 库所提供的方法。")]
    public static class BEncoding
    {
        // 字典定义
        private const byte DictionaryStart = (byte) 'd';
        private const byte DictionaryEnd = (byte) 'e';
        
        // 集合定义
        private const byte ListStart = (byte) 'l';
        private const byte ListEnd = (byte) 'e';
        
        // 整形定义
        private const byte NumberStart = (byte) 'i';
        private const byte NumberEnd = (byte) 'e';

        private const byte ByteArrayDivider = (byte) ':';

        #region 编码(序列化)操作
        
        /// <summary>
        /// 将对象序列化为 B 编码的字节流数据。
        /// </summary>
        /// <param name="obj">需要序列化的对象</param>
        /// <returns>采用 B 编码之后的字节流数据。</returns>
        public static byte[] Encode(object obj)
        {
            byte[] resultBytes = null;
            
            using (var buffer = new MemoryStream())
            {
                EncodingObjByType(buffer,obj);
                resultBytes = buffer.ToArray();
            }

            return resultBytes;
        }

        /// <summary>
        /// 将对象的数据序列化为字节流数据，并写出到指定路径。
        /// </summary>
        /// <param name="obj">需要序列化的对象。</param>
        /// <param name="filePath">要输出的文件路径。</param>
        public static void EncodeToFile(object obj, string filePath)
        {
            File.WriteAllBytes(filePath,Encode(obj));
        }

        private static void EncodingObjByType(MemoryStream buffer,object obj)
        {
            switch (obj)
            {
                case string objStr:
                    EncodeString(buffer,objStr);
                    break;
                case byte[] objBytes:
                    EncodeByteArray(buffer,objBytes);
                    break;
                case long objLong:
                    EncodeNumber(buffer,objLong);
                    break;
                case IList<object> objList:
                    EncodeList(buffer,objList);
                    break;
                case IDictionary<string,object> objDict:
                    EncodeDictionary(buffer,objDict);
                    break;
                default:
                    throw new AbpException($"不能将类型 {obj.GetType()} 进行 B 编码处理。");
            }
        }

        private static void EncodeString(MemoryStream buffer, string input)
        {
            EncodeByteArray(buffer,Encoding.UTF8.GetBytes(input));
        }

        private static void EncodeNumber(MemoryStream buffer, long input)
        {
            buffer.AppendByte(NumberStart);
            buffer.AppendBytes(Encoding.UTF8.GetBytes(Convert.ToString(input)));
            buffer.AppendByte(NumberEnd);
        }

        private static void EncodeList(MemoryStream buffer, IList<object> input)
        {
            buffer.AppendByte(ListStart);
            
            foreach (var item in input)
            {
                
            }
            
            buffer.AppendByte(ListEnd);
        }

        private static void EncodeDictionary(MemoryStream buffer, IDictionary<string, object> input)
        {
            buffer.AppendByte(DictionaryStart);

            // 此处需要按照字节进行排序，而不是根据字符串来进行排序。
            var sortedKeys = input.Keys.OrderBy(x => BitConverter.ToString(Encoding.UTF8.GetBytes(x)));
            foreach (var key in sortedKeys)
            {
                EncodeString(buffer,key);
                EncodingObjByType(buffer,input[key]);
            }
            
            buffer.AppendByte(DictionaryEnd);
        }

        private static void EncodeByteArray(MemoryStream buffer, byte[] body)
        {
            buffer.AppendBytes(Encoding.UTF8.GetBytes(Convert.ToString(body.Length)));
            buffer.AppendByte(ByteArrayDivider);
            buffer.AppendBytes(body);
        }
        
        #endregion
        
        #region 解码(反序列化)操作

        /// <summary>
        /// 从字节流当中按 B 编码的规则反序列化成对象。
        /// </summary>
        /// <param name="bytes">源字节数据</param>
        /// <returns>反序列化成功的对象</returns>
        public static object Decode(byte[] bytes)
        {
            object resultObj = null;
            
            using (var enumerator = ((IEnumerable<byte>) bytes).GetEnumerator())
            {
                enumerator.MoveNext();
                resultObj = DecodeNextObject(enumerator);
            }

            return resultObj;
        }

        /// <summary>
        /// 从文件当中按 B 编码的规则反序列化成对象。
        /// </summary>
        /// <param name="filePath">需要反序列化的文件</param>
        /// <returns>反序列化成功的对象</returns>
        public static object DecodeFromFile(string filePath)
        {
            return Decode(File.ReadAllBytes(filePath));
        }

        private static object DecodeNextObject(IEnumerator<byte> enumerator)
        {
            switch (enumerator.Current)
            {
                case DictionaryStart:
                    return DecodeDictionary(enumerator);
                case ListStart:
                    return DecodeList(enumerator);
                case NumberStart:
                    return DecodeNumber(enumerator);
                default:
                    return DecodeByteArray(enumerator);
            }
        }

        private static Dictionary<string, object> DecodeDictionary(IEnumerator<byte> enumerator)
        {
            var resultDictionary = new Dictionary<string,object>();
            var keys = new List<string>();
            
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == DictionaryEnd) break;

                // 所有键都应该是采用 UTF-8 进行编码的。
                var key = Encoding.UTF8.GetString(DecodeByteArray(enumerator));
                enumerator.MoveNext();
                var value = DecodeNextObject(enumerator);
                
                keys.Add(key);
                if(!resultDictionary.TryAdd(key,value)) throw new BEncodingException($"反序列化字典对象失败，有重复的字典键 {key} 。");
            }

            var sortedKeys = keys.OrderBy(x => BitConverter.ToString(Encoding.UTF8.GetBytes(x)));
            if (!keys.SequenceEqual(sortedKeys))
                throw new BEncodingException("无法加载字典，因为其键在序列化的时候没有进行有效的排序，无法进行反序列化操作。");

            return resultDictionary;
        }

        private static List<object> DecodeList(IEnumerator<byte> enumerator)
        {
            var resultList = new List<object>();

            while (enumerator.MoveNext())
            {
                if(enumerator.Current == ListEnd) break;
                
                resultList.Add(DecodeNextObject(enumerator));
            }

            return resultList;
        }

        private static byte[] DecodeByteArray(IEnumerator<byte> enumerator)
        {
            var lengthBytes = new List<byte>();

            do
            {
                if (enumerator.Current == ByteArrayDivider) break;

                lengthBytes.Add(enumerator.Current);
            } while (enumerator.MoveNext());

            // 获取到剩余字节数组的长度。
            var lengthStr = Encoding.UTF8.GetString(lengthBytes.ToArray());
            if (!int.TryParse(lengthStr, out int length)) throw new BEncodingException("无法获取到正确的长度信息。");

            // 如果长度正确，从迭代器当中读取剩余字节
            var resultBytes = new byte[length];

            for (int index = 0; index < length; index++)
            {
                enumerator.MoveNext();
                resultBytes[index] = enumerator.Current;
            }

            return resultBytes;
        }

        private static long DecodeNumber(IEnumerator<byte> enumerator)
        {
            var numberBytes = new List<byte>();

            while (enumerator.MoveNext())
            {
                if(enumerator.Current == NumberEnd) break;
                numberBytes.Add(enumerator.Current);
            }

            var numberStr = Encoding.UTF8.GetString(numberBytes.ToArray());
            if (!long.TryParse(numberStr, out long resultNumber)) throw new BEncodingException("无法反序列化数值类型，转换错误。");

            return resultNumber;
        }
        
        #endregion
    }
}