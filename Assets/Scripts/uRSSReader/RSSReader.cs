using System.Collections;
using UnityEngine;
using CodeHollow.FeedReader;

namespace uRSSReader
{
    public class RSSReader : MonoBehaviour
    {
        public string rssUrl;

        void Start()
        {
            StartCoroutine(GetRequest(rssUrl));
        }

        IEnumerator GetRequest(string uri)
        {
            var feed = FeedReader.ReadAsync(uri).GetAwaiter().GetResult();
            Debug.Log("Feed Title: " + feed.Title);
            Debug.Log("Feed Description: " + feed.Description);
            Debug.Log("Feed Image: " + feed.ImageUrl);
            foreach (var item in feed.Items)
            {
                string desc = item.Description.Replace("&nbsp;", "");
                Debug.Log(item.Title + " - " + item.Link + "\n" + desc);
            }

            yield return null;
        }
    }
}
