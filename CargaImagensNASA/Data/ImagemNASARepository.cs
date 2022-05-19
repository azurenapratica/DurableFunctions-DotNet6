using System;
using MongoDB.Driver;

namespace CargaImagensNASA.Data;

public class ImagemNASARepository : IImagemNASARepository
{
    public ImagemNASARepository() { }

    public void SaveInfoImagem(ImagemNASADocument infoImagem)
    {
        var mongoClient = new MongoClient(
            Environment.GetEnvironmentVariable("MongoConnection"));
        var mongoDatabase = mongoClient.GetDatabase(
            Environment.GetEnvironmentVariable("MongoDatabase"));
        var mongoCollection = mongoDatabase
            .GetCollection<ImagemNASADocument>(
                Environment.GetEnvironmentVariable("MongoCollection"));
        mongoCollection.InsertOne(infoImagem);
    }
}