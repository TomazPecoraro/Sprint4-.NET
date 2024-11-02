using Google.Cloud.Firestore;
using System.Threading.Tasks;

public class FirestoreService : IFirestoreService
{
    private readonly FirestoreDb _db;

    public FirestoreService()
    {
        _db = FirestoreDb.Create("adoptimize-2a80a"); 
    }

    public async Task AddCampanha(Campanha campanha)
    {
        var campanhaRef = _db.Collection("Campanhas").Document(campanha.Id.ToString());
        await campanhaRef.SetAsync(campanha);
    }
}
