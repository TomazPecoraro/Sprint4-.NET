using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

public class FireInitializer
{
    public static void Initialize()
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("AdOptimize.Firebase/adoptimize-2a80a-firebase-adminsdk-85tpl-dac9da5f58.json")
        });
    }
}
