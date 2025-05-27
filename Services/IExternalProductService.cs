using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerShop.Models;

public interface IExternalProductService
{
    Task<IEnumerable<ExternalProduct>> GetExternalProductsAsync();
}