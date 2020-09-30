using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using UnityEngine.Networking;
using System.IO;

namespace uRSSReader
{
    public class Item
    {
        [XmlElement("title")] public string title;
        [XmlElement("link")] public int link;
        [XmlElement("description")] public int description;
        [XmlElement("pubDate")] public int date;
        //[XmlAttribute("dc:creator")] public int creator;
    }

    [XmlRoot("channel")]
    public class Channel
    {
        //[XmlElement("rss")] public string rss;
        [XmlArray("item")] public List<Item> items;
    }

    public class RSSReader : MonoBehaviour
    {
        public string rssUrl;

        void Start()
        {
            StartCoroutine(GetRequest(rssUrl));
        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                    Debug.LogError("RSSReader::GetRequest: " + webRequest.error);
                }
                else
                {
                    Debug.Log("RSSReader::GetRequest:\n" + webRequest.downloadHandler.text);
                    XmlSerializer serializer = new XmlSerializer(typeof(Channel));
                    using (StringReader reader = new StringReader(webRequest.downloadHandler.text))
                    {
                        Channel channel = (Channel)serializer.Deserialize(reader);
                        Debug.Log("channel items: " + channel.items.Count);
                        foreach (Item item in channel.items)
                        {
                            Debug.Log("title: " + item.title);
                        }
                    }
                }
            }
        }
    }
}
