
namespace Localize.Company.Domain.Entities
{
    public class Organization : EntityBase
    {

        public string NomeEmpresarial { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Situacao { get; set; }
        public DateTime Abertura { get; set; }
        public string Tipo { get; set; }
        public string NaturezaLegal { get; set; }
        public string AtividadePrincipal { get; set; }
        public Endereco Endereco { get; set; }


        //Config One to Many
        public int UserId { get; set; }


        public void SetCnpj(string cnpj)
        {
            Cnpj = CleanCnpj(cnpj);
        }
        private static string CleanCnpj(string cnpj)
        {
            return new string(cnpj.Where(char.IsDigit).ToArray());
        }
    }
}
