using Sis_Controle_Viagens.Biblioteca;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Sis_Controle_Viagens.Models
{
    public class Block
    {
        [Key]
        [Display(Name = "#Bloco")]
        public int Height { get; set; }

        [Display(Name = "TimeStamp")]
        public long TimeStamp { get; set; }

        [Display(Name = "PrevHash")]
        public byte[] PrevHash { get; set; }

        [Display(Name = "Nonce")]
        public string Nonce { get; set; }

        public byte[] Hash { get; set; }
        [Display(Name = "Contract")]
        public string Contract { get; set; }

        [Display(Name = "Creator")]
        public string Creator { get; set; }
        
       

        public Block(byte[] prevHash, byte[] Hash, string contract, string creator, string Nonce = "")
        {
            
            PrevHash = prevHash;
            TimeStamp = DateTime.Now.Ticks;
            Contract = contract;
            Hash = this.GenerateHash();
            Creator = creator;
        }

        // generate hash of current block
        public byte[] GenerateHash()
        {
            var sha = SHA256.Create();
            byte[] timeStamp = BitConverter.GetBytes(TimeStamp);

            var contractHash = Contract.ConvertToBytes();

            byte[] headerBytes = new byte[timeStamp.Length + PrevHash.Length + contractHash.Length];

            Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
            Buffer.BlockCopy(PrevHash, 0, headerBytes, timeStamp.Length, PrevHash.Length);
            Buffer.BlockCopy(contractHash, 0, headerBytes, timeStamp.Length + PrevHash.Length, contractHash.Length);

            byte[] hash = sha.ComputeHash(headerBytes);

            return hash;
        }

    }
}
