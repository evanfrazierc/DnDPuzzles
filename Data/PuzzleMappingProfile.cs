using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DndPuzzles.Data.Entities;
using DndPuzzles.ViewModels;

namespace DndPuzzles.Data
{
  public class PuzzleMappingProfile : Profile
  {
    public PuzzleMappingProfile()
    {
      CreateMap<Order, OrderViewModel>()
        .ForMember(o => o.OrderId, ex => ex.MapFrom(i => i.Id))
        .ReverseMap();

      CreateMap<OrderItem, OrderItemViewModel>()
        .ReverseMap()
        .ForMember(m => m.Product, opt => opt.Ignore());
    }
  }
}
