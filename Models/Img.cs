using System.Net;
using System.Text;

public class Img
{
    public Img(Uri url)
    {
        using (HttpClient client = new())
        {
            image = client.GetByteArrayAsync(url).Result;
            // OR 
            //client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");
        }
        id = new Random().Next().ToString();

    }
    public Img(byte[] im)
    {
        image = im;
        id = new Random().Next().ToString();
    }
    public Img(string im)
    {
        image = Encoding.ASCII.GetBytes(im);
        id = new Random().Next().ToString();
    }
    public Img() { }
    public string id { get; private set; }
    public byte[] image { get; set; }
}