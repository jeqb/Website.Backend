using System.Text;
using System.Reflection;

namespace Website.Backend.Infrastructure.Email
{
    /// <summary>
    /// Load HTML files from the Email/Static folder.
    /// This allows .html files and whatever else to live in the codebase
    /// without having to load them into a blob storage or something like that
    /// for retrieval.
    /// pros and cons to it.
    /// Not sure I'm a fan of reflection.
    /// </summary>
    public class StaticFileLoader
    {
        private readonly string _buildPath;

        private const string _staticFilePath = "/Email/Static/";

        public StaticFileLoader()
        {
            DirectoryInfo directoryInfo = new(Assembly.GetExecutingAssembly().Location);
            
            _buildPath = directoryInfo.Parent.FullName.ToString();
        }

        public string GetFileString(string fileName)
        {
            StringBuilder sb = new();

            sb.Append(_buildPath);
            sb.Append(_staticFilePath);
            sb.Append(fileName);

            string filePath = sb.ToString();

            string result = File.ReadAllText(filePath);

            return result;
        }
    }
}
