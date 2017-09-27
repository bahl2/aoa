namespace Assets.Scripts.Objetos
{
    public static class GameTags
    {
        public enum Tables
        {
            Usuarios,
            Exercitos,
            Construcoes
        };

        public enum Tags
        {

        };

        public static string ServerIP()
        {
            return "http://138.197.0.169";
        }

        public static string UrlExecQuery()
        {
            return ServerIP() + "/ExecQuery.php";
        }
    }
}
