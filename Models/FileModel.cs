
using System.ComponentModel.DataAnnotations.Schema;

public class FileModel
{
    public string id { get; set; }  
    public string name { get; set; }
    public byte[] data { get; set; }
    public string contentType { get; set; } 
    public long length { get; set; }
    public FileModel()
    {

    }
    public FileModel(IFormFile file)
    {
        id = new Random().Next().ToString();

        this.length = file.Length;
        var stream = new MemoryStream((int) length);
        file.CopyTo(stream);
        this.data = stream.ToArray();
        this.contentType = file.ContentType;
        this.name = file.FileName;
    }
    public FileModel(byte[] bytes,string name,string type)
    {
        id = new Random().Next().ToString();

        data = bytes;
        this.contentType = type;
        this.name = name;
        length= bytes.Length;
    }
}
