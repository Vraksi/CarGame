using System.Security.Cryptography.X509Certificates;

namespace API.Models
{
    public class Highscore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }

    }
}