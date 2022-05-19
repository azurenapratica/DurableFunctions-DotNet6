using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CargaImagensNASA.Data;

public class ImagemNASADocument
{
    public ObjectId _id { get; set; }
    public string Data { get; set; }
    public string DataUpload { get; set; }
    public string Titulo { get; set; }
    public string Detalhes { get; set; }
    public string UrlImagem { get; set; }
    public bool HD { get; set; }
    public string BlobName { get; set; }
    public string BlobContainer { get; set; }
    [BsonIgnoreIfNull]
    public string Copyright { get; set; }
    public string MediaType { get; set; }
    public string ServiceVersion { get; set; }        
}