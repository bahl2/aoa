
public static class GameTags
{
    public enum ECenas
    {
        Carrega,
        Fase1,
        MenuPrincipal,
        CutScene
    }

    public static string[] _Cenas = { "Carrega", "Fase1", "Menu Principal", "CutScene" };

    public static string ServerHost()
    {
        return "http://138.197.0.169";
    }

    public static string UrlExecQuery()
    {
        return ServerHost() + "/ExecQuery.php";
    }

    public static string ServerHostGoogleTradutor()
    {
        return "https://translate.googleapis.com/translate_a/single?client=gtx&sl=";
    }
}
