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
                .ForPath(n=> n.ClientSurname, opt => opt.MapFrom(c=> c.Client.Surname))
                .ForPath(n=> n.ClientFirstName, opt => opt.MapFrom(c=> c.Client.FirstName))
                
                .ForPath(n=> n.ProductName, opt => opt.MapFrom(c=> c.Product.Name))
                .ForPath(n=> n.ProductCost, opt => opt.MapFrom(c=> c.Product.Cost))
                
                .ForPath(n=> n.ManagerSurname, opt => opt.MapFrom(c=> c.Manager.Surname))
                
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
                .ForPath(n=> n.ClientSurname, opt => opt.MapFrom(c=> c.Client.Surname))
                .ForPath(n=> n.ClientFirstName, opt => opt.MapFrom(c=> c.Client.FirstName))
                
                .ForPath(n=> n.ProductName, opt => opt.MapFrom(c=> c.Product.Name))
                .ForPath(n=> n.ProductCost, opt => opt.MapFrom(c=> c.Product.Cost))
                
                .ForPath(n=> n.ManagerSurname, opt => opt.MapFrom(c=> c.Manager.Surname))
                
                .ForMember(n=> n.Date, opt => opt.MapFrom(c=> c.Date))
                .ForMember(n=> n.Id, opt => opt.MapFrom(c=> c.Id))
            );
            var mapper = new Mapper(config);
            var saleView = mapper.Map<Sale, SaleViewModel>(sale);
            return saleView;
        }
        
        public Sale MapConfig(SaleViewModel saleView)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<SaleViewModel, Sale>()
                .ForPath(n=> n.Client.Surname, opt => opt.MapFrom(c=> c.ClientSurname))
                .ForPath(n=> n.Client.FirstName, opt => opt.MapFrom(c=> c.ClientFirstName))
                
                .ForPath(n=> n.Product.Name, opt => opt.MapFrom(c=> c.ProductName))
                .ForPath(n=> n.Product.Cost, opt => opt.MapFrom(c=> c.ProductCost))
                
                .ForPath(n=> n.Manager.Surname, opt => opt.MapFrom(c=> c.ManagerSurname))
                
                .ForMember(n=> n.Date, opt => opt.MapFrom(c=> c.Date))
                .ForMember(n=> n.Id, opt => opt.MapFrom(c=> c.Id))
            );
            var mapper = new Mapper(config);
            var sale = mapper.Map<SaleViewModel, Sale>(saleView);
            return sale;
        }
    }
}