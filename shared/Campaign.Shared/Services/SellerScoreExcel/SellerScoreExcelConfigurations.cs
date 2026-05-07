namespace Campaign.Shared.Services.SellerScoreExcel
{
    public class SellerScoreExcelConfigurations
    {
        private static string _directory = "SellersScores";
        private static string _path = @$"{Directory.GetCurrentDirectory()}";
        private static string _fileName = $"{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.xlsx";

        public static string GetPath()
        {
            CreateDirectoryIfNotExists();
            return Path.Combine(_path, _directory, _fileName);
        }

        private static void CreateDirectoryIfNotExists()
        {
            if (!Directory.Exists(Path.Combine(_path, _directory)))
                Directory.CreateDirectory(Path.Combine(_path, _directory));
        }
    }
}
