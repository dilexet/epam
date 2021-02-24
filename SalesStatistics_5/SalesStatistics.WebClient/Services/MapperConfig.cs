using System.Collections.Generic;
using AutoMapper;
using SalesStatistics.DataAccessLayer.Models;
using SalesStatistics.WebClient.Models.ViewModels;

namespace SalesStatistics.WebClient.Services
{
    public class MapperConfig
    {
        public IEnumerable<SaleViewModel> MapConfig(IEnumerable<Sale> sales)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Sale, SaleViewModel>()
                .ForPath(n=> n.Client.Surname, opt => opt.MapFrom(c=> c.Client.Surname))
                .ForPath(n=> n.Client.FirstName, opt => opt.MapFrom(c=> c.Client.FirstName))
                
                .ForPath(n=> n.Product.Name, opt => opt.MapFrom(c=> c.Product.Name))
                .ForPath(n=> n.Product.Cost, opt => opt.MapFrom(c=> c.Product.Cost))
                
                .ForPath(n=> n.Manager.Surname, opt => opt.MapFrom(c=> c.Manager.Surname))
                
                .ForMember(n=> n.Date, opt => opt.MapFrom(c=> c.Date))
                .ForMember(n=> n.Id, opt => opt.MapFrom(c=> c.Id))
            );
            var mapper = new Mapper(config);
            var salesView = mapper.Map<IEnumerable<Sale>, List<SaleViewModel>>(sales);
            return salesView;
        }
        
        public SaleViewModel MapConfig(Sale sale)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Sale, SaleViewModel>()
                .ForPath(n=> n.Client.Surname, opt => opt.MapFrom(c=> c.Client.Surname))
                .ForPath(n=> n.Client.FirstName, opt => opt.MapFrom(c=> c.Client.FirstName))
                
                .ForPath(n=> n.Product.Name, opt => opt.MapFrom(c=> c.Product.Name))
                .ForPath(n=> n.Product.Cost, opt => opt.MapFrom(c=> c.Product.Cost))
                
                .ForPath(n=> n.Manager.Surname, opt => opt.MapFrom(c=> c.Manager.Surname))
                
                .ForMember(n=> n.Date, opt => opt.MapFrom(c=> c.Date))
                .ForMember(n=> n.Id, opt => opt.MapFrom(c=> c.Id))
            );
            var mapper = new Mapper(config);
            var saleView = mapper.Map<Sale, SaleViewModel>(sale);
            return saleView;
        }
    }
}