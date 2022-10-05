using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DndPuzzles.Data.Entities;

namespace DndPuzzles.ViewModels
{
  public class OrderViewModel
  {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    [Required]
    [MinLength(4)]
    public string OrderNumber { get; set; }

    public ICollection<OrderItemViewModel> Items { get; set; }
  }
}
