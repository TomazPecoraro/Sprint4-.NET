using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using System.IO;

public class FireInitializer
{
    public static void Initialize()
    {
        // Carregar o caminho do arquivo JSON
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Credential", "adoptimize-2a80a-firebase-adminsdk-85tpl-dac9da5f58.json");

        // Criar a aplicação Firebase
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile(jsonFilePath)
        });
    }
}
