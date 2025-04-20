using AutoMapper;
using Localize.Company.Domain.Entities;
using Localize.Company.Infrastructure.External.ReceitaWSApi.Entities;

namespace Localize.Company.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReceitaWS, Organization>()
                .ForMember(dest => dest.NomeFantasia, opt => opt.MapFrom(src => src.fantasia))
                .ForMember(dest => dest.NomeEmpresarial, opt => opt.MapFrom(src => src.nome))
                .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => src.cnpj))
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.status))
                .ForMember(dest => dest.Abertura, opt => opt.MapFrom(src => ParseDate(src.abertura)))
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.tipo))
                .ForMember(dest => dest.NaturezaLegal, opt => opt.MapFrom(src => src.natureza_juridica))
                .ForMember(dest => dest.AtividadePrincipal, opt => opt.MapFrom(src => src.atividade_principal.FirstOrDefault().text))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco
                {
                    Rua = src.logradouro,
                    Numero = src.numero,
                    Complemento = src.complemento,
                    Bairro = src.bairro,
                    Cidade = src.municipio,
                    Estado = src.uf,
                    Cep = src.cep
                }))
                .ReverseMap();
        }

        private static DateTime ParseDate(string data)
        {
            return DateTime.TryParse(data, out var result) ? result : DateTime.MinValue;
        }
    }
}
