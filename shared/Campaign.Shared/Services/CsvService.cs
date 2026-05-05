using Campaign.Shared.Exceptions;
using System.Net;
using System.Text;

namespace Campaign.Shared.Services
{
    public class CsvService
    {
        private readonly string _fileName;
        private readonly string _path = @$"C:\Users\matheusp\Desktop\Campanha_2026\CSVs";

        public CsvService(string directory, string fileName)
        {
            if(string.IsNullOrEmpty(directory))
                throw new CompaignException(HttpStatusCode.InternalServerError, "O diretório para salvar os arquivos CSVs não pode ser nulo ou vazio.");

            _fileName = fileName;
            _path = Path.Combine(_path, directory!);
        }

        public CsvService(string fileName)
        {
            _fileName = fileName;
        }

        public void Create(string lines)
        {
            if(!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            var path = Path.Combine(_path, _fileName);

            File.WriteAllText(path, lines, Encoding.UTF8);
        }
    }
}
